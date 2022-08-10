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
            msgOut = msgIn + "XENITIA5";
        }

        //async static public Task<String> Send2(string msgIn)
        //{
        //    EHSend.Init();
        //    EHSend.Send(msgIn);
        //    //await EHSend.Send(msgIn);
        //    return msgIn + "XENITIA4";
        //    //msgOut = msgIn + "XENITIA4";
        //    //await EHSend.Send(msgOut); //.Wait();
        //}

        static public void Send3(string BusinessEvent, string msgIn, out string msgOut)
        {
            EHSend.Init();
            EHSend.Send(BusinessEvent, msgIn);
            msgOut = msgIn + "XENITIA5";
        }

        static public void SendMultiple(List<MFEventData> eventList, out string msgOut)
        {
            EHSend.Init();
            EHSend.SendMultiple(eventList);
            msgOut = eventList.First().BusinessEvent + eventList.First().EventMessage + "XENITIA5";
        }

        async static public void SendMultipleAsync(List<MFEventData> eventList, int batchsize)
        {
            EHSend.Init();
            await EHSend.SendMultipleAsync(eventList, batchsize);
        }
    }

    public class MFEventData
    {
        public string BusinessEvent = "DefaultBusinessEvent";
        public string EventMessage = "XENITIA";
    }

    static internal class EHSend
    {
        //static SecretClient client = new SecretClient(vaultUri: new Uri("https://eventhubsnskeyvault.vault.azure.net/"), credential: new DefaultAzureCredential());
        static String eventHubsConnectionString;
        static String eventHubName = "eventhubtopic1";
        static int defaultBatchSize = 10; //1, 10, 1000, 5000
        static string ComputerName = System.Net.Dns.GetHostName();

        static bool isInitialised = false;

        //async static Task Main(string[] args)
        static void Main(string[] args)
        {
            //KeyVaultSecret secret = client.GetSecret("eventHubsConnectionString");
            //eventHubsConnectionString = secret.Value;

            eventHubsConnectionString = "Endpoint=sb://eventhubabc.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YVZ/vp4y/KZjdPHnazN3eOZGO6I8Mlu/G+Ws60VUK9w=";
            ComputerName = System.Net.Dns.GetHostName();
            Send();

            //await Send();
            //await Send("WOOHOO");
            //await Send(args[0]);
        }

        public static void Init()
        {
            if (!isInitialised)
            {
                eventHubsConnectionString = "Endpoint=sb://eventhubabc.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YVZ/vp4y/KZjdPHnazN3eOZGO6I8Mlu/G+Ws60VUK9w=";
                ComputerName = System.Net.Dns.GetHostName();
                isInitialised = true;
            }
        }

        //public async static Task Send(string message = "XENITIA4")
        public static void Send(string BusinessEvent = "DefaultBusinessEvent", string message = "XENITIA")
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
                    //EventDataBatch eventBatch = await producer.CreateBatchAsync(batchOptions);
                    EventDataBatch eventBatch = producer.CreateBatchAsync(batchOptions).Result;
                    var eventData = new EventData();

                    for (var index = 0; index < defaultBatchSize; ++index)
                    {
                        //eventData = new EventData($"PartitionId:{firstPartition} Host:{ComputerName}Event #{counter}:{index} Id:{Guid.NewGuid()} Timestamp:{DateTime.Now.ToString()}");
                        eventData = new EventData($"PartitionKey:{batchOptions.PartitionKey} ** InputData {message} ** Host:{ComputerName}Event #{counter}:{index} Id:{Guid.NewGuid()} Timestamp:{DateTime.Now.ToString()}");
                        eventData.MessageId = Guid.NewGuid().ToString();
                        eventData.CorrelationId = Guid.NewGuid().ToString();
                        eventData.Properties.Add("BusinessEvent", BusinessEvent);

                        Console.WriteLine($"PRE: Adding to Micro Batch Payload: Event to partitionKey: {batchOptions.PartitionKey} with length {eventData.EventBody.ToArray().Length}, eventBody = {eventData.EventBody}.");

                        if (!eventBatch.TryAdd(eventData))
                        {
                            //throw new Exception($"The event at Host:{ComputerName} { index } could not be added.");
                            break;
                        }
                        //Thread.Sleep(10);

                    }
                    //Thread.Sleep(3000);
                    //await producer.SendAsync(eventBatch);
                    producer.SendAsync(eventBatch).Wait();
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
                //await producer.CloseAsync();
                producer.CloseAsync().Wait();
            }
        }

        //public static void SendMultiple(List<MFEventData> eventList)
        async public static Task SendMultipleAsync(List<MFEventData> eventList, int batchsize)
        {
            var producer = new EventHubProducerClient(eventHubsConnectionString, eventHubName);

            try
            {
                var batchOptions = new CreateBatchOptions
                {
                    PartitionKey = "ABCDE"
                };


                for (int iEl = 0; iEl < eventList.Count;)
                {
                    EventDataBatch eventBatch = await producer.CreateBatchAsync(batchOptions);
                    var eventData = new EventData();

                    var batchindex = 0;

                    while (iEl < eventList.Count && batchindex < batchsize)
                    {
                        MFEventData el = eventList[iEl];

                        eventData = new EventData($"{el.EventMessage}");
                        eventData.MessageId = Guid.NewGuid().ToString();
                        eventData.CorrelationId = Guid.NewGuid().ToString();
                        eventData.Properties.Add("BusinessEvent", el.BusinessEvent);
                        eventData.Properties.Add("Hostname", ComputerName);
                        eventData.Properties.Add("SentTimestamp", DateTime.Now.ToString());
                        eventData.Properties.Add("MCPartitionKey", batchOptions.PartitionKey);
                        eventData.Properties.Add("MCEventNumber", $"MicrobatchEvent #{batchindex}");

                        Console.WriteLine($"PRE: Adding to Micro Batch Payload: Event to partitionKey: {batchOptions.PartitionKey} with length {eventData.EventBody.ToArray().Length}, eventBody = {eventData.EventBody}.");

                        if (!eventBatch.TryAdd(eventData))
                        {
                            //throw new Exception($"The event at Host:{ComputerName} { index } could not be added.");
                            break;
                        }
                        batchindex++;
                        iEl++;
                    }
                    await producer.SendAsync(eventBatch);
                    Console.WriteLine($"POST: Sent Micro Batch Payload."); // Event to partitionKey: {batchOptions.PartitionKey} with length {eventData.EventBody.ToArray().Length}, eventBody = {eventData.EventBody}.");
                }

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

        public static void SendMultiple(List<MFEventData> eventList)
        {
            var producer = new EventHubProducerClient(eventHubsConnectionString, eventHubName);

            try
            {
                var batchOptions = new CreateBatchOptions
                {
                    PartitionKey = "ABCDE"
                };

                foreach (MFEventData el in eventList)
                {
                    EventDataBatch eventBatch = producer.CreateBatchAsync(batchOptions).Result;
                    var eventData = new EventData();

                    for (var index = 0; index < defaultBatchSize; ++index)
                    {
                        //eventData = new EventData($"PartitionId:{firstPartition} Host:{ComputerName}Event #{counter}:{index} Id:{Guid.NewGuid()} Timestamp:{DateTime.Now.ToString()}");
                        eventData = new EventData($"{el.EventMessage}");
                        eventData.MessageId = Guid.NewGuid().ToString();
                        eventData.CorrelationId = Guid.NewGuid().ToString();
                        eventData.Properties.Add("BusinessEvent", el.BusinessEvent);
                        eventData.Properties.Add("Hostname", ComputerName);
                        eventData.Properties.Add("SentTimestamp", DateTime.Now.ToString());
                        eventData.Properties.Add("MCPartitionKey", batchOptions.PartitionKey);
                        eventData.Properties.Add("MCEventNumber", $"MicrobatchEvent #{index}");

                        Console.WriteLine($"PRE: Adding to Micro Batch Payload: Event to partitionKey: {batchOptions.PartitionKey} with length {eventData.EventBody.ToArray().Length}, eventBody = {eventData.EventBody}.");

                        if (!eventBatch.TryAdd(eventData))
                        {
                            //throw new Exception($"The event at Host:{ComputerName} { index } could not be added.");
                            break;
                        }

                    }
                    producer.SendAsync(eventBatch).Wait();
                    Console.WriteLine($"POST: Sent Micro Batch Payload."); // Event to partitionKey: {batchOptions.PartitionKey} with length {eventData.EventBody.ToArray().Length}, eventBody = {eventData.EventBody}.");
                }
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
                producer.CloseAsync().Wait();
            }
        }
    }

}
