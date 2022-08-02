using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using Azure.Identity;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
//using Azure.Security.KeyVault.Secrets;

namespace ClassLibraryOSTest
{
    public class Sender
    {
        static public void Send(string msgIn, out string msgOut)
        {
            EHSend.Init();
            EHSend.Send(msgIn);
            msgOut = msgIn + "XENITIA3";

            Task.Delay(5000).Wait();
          
        }
    }



    static internal class EHSend
    {
        //static SecretClient client = new SecretClient(vaultUri: new Uri("https://eventhubsnskeyvault.vault.azure.net/"), credential: new DefaultAzureCredential());

        static String eventHubsConnectionString;
        static String eventHubName = "eventhubtopic1";
        static int batchSize = 1; //1, 10, 1000, 5000
        static string ComputerName = System.Net.Dns.GetHostName();

        async static Task Main(string[] args)
        {
            //KeyVaultSecret secret = client.GetSecret("eventHubsConnectionString");
            //eventHubsConnectionString = secret.Value;

            eventHubsConnectionString = "eventhubabc.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YVZ/vp4y/KZjdPHnazN3eOZGO6I8Mlu/G+Ws60VUK9w=";
            ComputerName = System.Net.Dns.GetHostName();

            await Send();
            await Send("WOOHOO");
            //await Send(args[0]);
        }

        public static void Init()
        {
            eventHubsConnectionString = "eventhubabc.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YVZ/vp4y/KZjdPHnazN3eOZGO6I8Mlu/G+Ws60VUK9w=";
            ComputerName = System.Net.Dns.GetHostName();

        }

        public async static Task Send(string message = "XENITIA2")
        {
            var producer = new EventHubProducerClient(eventHubsConnectionString, eventHubName);

            try
            {
                var batchOptions = new CreateBatchOptions
                {
                    PartitionKey = "ABCDE"
                };

                //string firstPartition = (await producer.GetPartitionIdsAsync()).First();

                //var batchOptions = new CreateBatchOptions
                //{
                //    PartitionId = firstPartition
                //};

                //for (var counter = 0; counter < int.MaxValue; ++counter)
                for (var counter = 0; counter < 1; ++counter)

                {
                    //producer = new EventHubProducerClient(connectionString, eventHubName);
                    EventDataBatch eventBatch = await producer.CreateBatchAsync(batchOptions);
                    var eventData = new EventData();

                    for (var index = 0; index < batchSize; ++index)
                    {
                        //eventData = new EventData($"PartitionId:{firstPartition} Host:{ComputerName}Event #{counter}:{index} Id:{Guid.NewGuid()} Timestamp:{DateTime.Now.ToString()}");
                        eventData = new EventData($"PartitionKey:{batchOptions.PartitionKey} ** InputData {message} ** Host:{ComputerName}Event #{counter}:{index} Id:{Guid.NewGuid()} Timestamp:{DateTime.Now.ToString()}");
                        eventData.MessageId = Guid.NewGuid().ToString();
                        eventData.CorrelationId = Guid.NewGuid().ToString();

                        Console.WriteLine($"PRE: Adding to Micro Batch Payload: Event to partitionKey: {batchOptions.PartitionKey} with length {eventData.EventBody.ToArray().Length}, eventBody = {eventData.EventBody}.");

                        if (!eventBatch.TryAdd(eventData))
                        {
                            //throw new Exception($"The event at Host:{ComputerName} { index } could not be added.");
                            break;
                        }
                        //Thread.Sleep(10);

                    }
                    //Thread.Sleep(3000);
                    await producer.SendAsync(eventBatch);
                    //Thread.Sleep(3000);
                    Console.WriteLine($"POST: Sent Micro Batch Payload."); // Event to partitionKey: {batchOptions.PartitionKey} with length {eventData.EventBody.ToArray().Length}, eventBody = {eventData.EventBody}.");

                    //await producer.CloseAsync();
                    //Thread.Sleep(1000);
                }
                //await producer.SendAsync(eventBatch);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Transient failures will be automatically retried as part of the
                // operation. If this block is invoked, then the exception was either
                // fatal or all retries were exhausted without a successful publish.
            }
            finally
            {
                await producer.CloseAsync();
            }
        }
    }

}
