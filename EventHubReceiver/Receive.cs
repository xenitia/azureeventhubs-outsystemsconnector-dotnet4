using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using System.Collections.Concurrent;
using System.Diagnostics;
using Azure.Messaging.EventHubs.Primitives;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using System.Threading;

namespace AzureEventHubs.OutSystemsConnector.DotNet4
{
    internal class EventHubReceiver
    {

        static SecretClient client = new SecretClient(vaultUri: new Uri("https://eventhubsnskeyvault.vault.azure.net/"), credential: new DefaultAzureCredential());

        static KeyVaultSecret secret;
        static String eventHubsConnectionString;
        static String storageConnectionString;

        static String blobContainerName = "eventhubblob";

        static String eventHubName = "eventhubtopic1";
        static String consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
        static int maximumBatchSize = 1000;


        async static Task Main(string[] args)
        {

            secret = client.GetSecret("eventHubsConnectionString");
            eventHubsConnectionString = secret.Value;
            secret = client.GetSecret("storageConnectionString");
            storageConnectionString = secret.Value;

            BlobContainerClient storageClient = new BlobContainerClient(storageConnectionString, blobContainerName);

            var processor = new MFCustomEventHubProcessor(
                storageClient,
                maximumBatchSize,
                consumerGroup,
                eventHubsConnectionString,
                eventHubName);

            CancellationTokenSource cancellationSource = new CancellationTokenSource();
            cancellationSource.CancelAfter(TimeSpan.FromSeconds(30));

            // Starting the processor does not block when starting; delay
            // until the cancellation token is signaled.

            try
            {
                await processor.StartProcessingAsync(cancellationSource.Token);
                await Task.Delay(Timeout.Infinite, cancellationSource.Token);
            }
            catch (TaskCanceledException)
            {
                // This is expected if the cancellation token is
                // signaled.
            }
            finally
            {
                // Stopping may take up to the length of time defined
                // as the TryTimeout configured for the processor;
                // By default, this is 60 seconds.

                await processor.StopProcessingAsync();
            }
        }
    }
}