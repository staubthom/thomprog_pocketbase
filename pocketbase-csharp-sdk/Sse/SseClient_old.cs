using pocketbase_csharp_sdk.Models;
using System.Text;

namespace pocketbase_csharp_sdk.Sse
{
    public class SseClient
    {
        const string BasePath = "/api/realtime";

        private readonly PocketBase client;
        private CancellationTokenSource? tokenSource = null;
        private Task? eventListenerTask = null;

        public string? Id { get; private set; }
        public bool IsConnected { get; private set; } = false;

        public Func<SseMessage, Task> CallbackAsync { get; private set; }

        public SseClient(PocketBase client, Func<SseMessage, Task> callbackAsync)
        {
            this.client = client;
            CallbackAsync = callbackAsync;
        }
        ~SseClient()
        {
            Disconnect();
        }

        public async Task EnsureIsConnectedAsync()
        {
            if (!IsConnected)
                await ConnectAsync();
        }

        public async Task ConnectAsync()
        {
            Disconnect();
            tokenSource = new CancellationTokenSource();
            try
            {
                eventListenerTask = ConnectEventStreamAsync(tokenSource.Token);

                while (!IsConnected)
                    await Task.Delay(250);
            }
            catch { throw; }
        }

        public void Disconnect()
        {
            tokenSource?.Cancel();
            tokenSource?.Dispose();
            tokenSource = null;

            try { eventListenerTask?.Dispose(); }
            catch { }
            eventListenerTask = null;

            IsConnected = false;
            Id = null;
        }

        private async Task ConnectEventStreamAsync(CancellationToken token)
        {
           
             using var client = new HttpClient();
              var request = new HttpRequestMessage(HttpMethod.Get, "stream")
                    {
                        RequestUri = new Uri("http://127.0.0.1:8090/api/realtime")
                    };

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            Console.WriteLine(response);
           
          
            try
            {

                //var response = await httpClient.GetAsync(client.BuildUrl(BasePath).ToString(),
                //                                         HttpCompletionOption.ResponseHeadersRead,
                //                                         token);
               

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Unable to connect the stream");

                var isTextEventStream = response.Content.Headers.ContentType?.MediaType == "text/event-stream";

                if (!isTextEventStream)
                    throw new InvalidOperationException("Invalid resource content type");

                //TODO this never completes
                Console.WriteLine(response);

                Stream? stream = null;
                //var stream =await response.Content.ReadAsStreamAsync();
                var buffer = new byte[8];
                //Console.WriteLine("Connected SseClient Zeile 89");    

                while (!token.IsCancellationRequested)
                {
                     Console.WriteLine("Connected SseClient Zeile 94");
                    stream = await response.Content.ReadAsStreamAsync();
                    
                     Console.WriteLine("Connected SseClient Zeile 96");
                    if (stream.CanRead)
                    {


                        Console.WriteLine("Connected SseClient Zeile 101");
                        var readCount = await stream.ReadAsync(buffer, token);

                        Console.WriteLine(readCount);
                        if (readCount > 0)
                        {
                            var data = Encoding.UTF8.GetString(buffer, 0, readCount);
                            var sseMessage = await SseMessage.FromReceivedMessageAsync(data);
                            if (sseMessage != null)
                            {
                                if (sseMessage.Id != null && sseMessage.Event == "PB_CONNECT")
                                {
                                    Id = sseMessage.Id;
                                    IsConnected = true;
                                    Console.WriteLine(sseMessage.Id);
                                }
                                await CallbackAsync(sseMessage);
                            }
                        }
                        await Task.Delay(125, token);
                    }
                }
            }
            finally
            {
                client.Dispose();
                IsConnected = false;
                Id = null;
            }
        }
    }
}
