using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using pocketbase_csharp_sdk.Models;
using System.Text.Json;
using pocketbase_csharp_sdk.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Http;


namespace pocketbase_csharp_sdk.Services;
                      
public abstract class RealtimeServiceBase : IRealtimeServiceBase
{

    

    public HttpClient _httpcleint;
    public string newLineChar { get; set; }

    public readonly Dictionary<string, List<Action<RealtimeEventArgs>>> subscriptions = new();
    public string _cleintId = "";
    public bool cancelled;
    public string baseUrl { get; set; }

    public RealtimeServiceBase(HttpClient httpClient, string baseUrl)
    {
        this._httpcleint = httpClient;
        this.baseUrl = baseUrl;
        newLineChar = (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\n" : Environment.NewLine);
    }

    public void Dispose()
    {
        //Todo Disposable
        //implement proper cancellation tockens
        cancelled = true;
    }

    /// <summary>
    /// Manages subscriptions based on topic
    /// </summary>
    /// <param name="topic">topic to subscribe to</param>
    /// <param name="callback">callback to be run when given topic has been altered</param>
    public void Subscribe(string topic, Action<RealtimeEventArgs> callback, string collectioName)
    {
        if (string.IsNullOrWhiteSpace(topic))
        {
            throw new Exception("topic needs to be set cannot be empty");
        }

        topic = topic == "*" ? collectioName.ToLower() : topic.ToLower();

        if (subscriptions.Count == 0) //init new subscriptions and establish connections
        {
            //Todo here for each topic need call related callbacks
            subscriptions.Add(topic, new List<Action<RealtimeEventArgs>>());
            subscriptions[topic].Add(callback);
            Dowork();

        }
        else if (subscriptions.TryGetValue(topic, out List<Action<RealtimeEventArgs>>? value))
        {
            value.Add(callback); //Add new callback to existing topic
        }
        else
        {
            subscriptions.Add(topic, new List<Action<RealtimeEventArgs>>());
            subscriptions[topic].Add(callback);
            AddRemoveTopics();
        }

    }

    /// <summary>
    /// unsubscribe from the given topic
    /// </summary>
    /// <param name="topic">Topic name or , can use * to remove all subscriptions</param>
    public void UnSubscribe(string topic)
    {
        if (topic == "*")
        {
            subscriptions.Clear();
            AddRemoveTopics();
            Dispose();
        }
        else
        {
            subscriptions.Remove(topic);
        }
    }

    /// <summary>
    /// Communicate with the server to subscribe or unsubscribe to topics
    /// </summary>
    /// <returns></returns>
    public async void AddRemoveTopics()
    {

        await _httpcleint.PostAsJsonAsync(baseUrl + "api/realtime", new
        {
            clientId = _cleintId,
            subscriptions = subscriptions.Keys.ToArray()
        });
    }

    public void ProcessCallBacks(RealtimeEventArgs args)
    {
        subscriptions[args.@event].ForEach((clb) => clb.Invoke(args));
    }

    public virtual void Dowork()
    {
        try
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "stream")
                    {
                        RequestUri = new Uri(baseUrl + "api/realtime"),                
                     
                    };
                    request.SetBrowserResponseStreamingEnabled(true);
                    
                    //https://www.tpeczek.com/2021/12/aspnet-core-6-and-iasyncenumerable.html



                    await ReadSSEStream(request);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    Debug.WriteLine(ex.Message);
                }
                return Task.CompletedTask;
            });
            ;
        }
        catch (Exception ex)
        {

            Debug.WriteLine(ex.Message);
            Console.WriteLine(ex.Message);
        }
    }


    public async Task ReadSSEStream(HttpRequestMessage request)
    {

        using var client = new HttpClient();

        
        var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
       
        
        bool prevNewLine = false;
        var _responseContent = "";

        Stream? stream = null;
        byte[] bytes = new byte[1];
        var newlineChar = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\n" : Environment.NewLine;
        
        
        while (!cancelled)
        {
            try
            {
                
               
               
                stream = await response.Content.ReadAsStreamAsync();
               


                if (stream.CanRead)
                {
                    await stream.ReadAsync(bytes);
                    string? letter = Encoding.UTF8.GetString(bytes);
                    _responseContent += letter;
                   


                    if (letter == newlineChar && prevNewLine == true)
                    {
                        _responseContent = _responseContent[..^2];

                        ProcessEvents(_responseContent);
                        _responseContent = string.Empty;
                        prevNewLine = false;
                        await Task.Delay(TimeSpan.FromMilliseconds(100));
                    }
                    else if (letter == newlineChar && prevNewLine == false)
                    {
                        prevNewLine = true;
                    }
                    else
                    {
                        prevNewLine = false;
                    }
                }
                else
                {
                    break;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Retrying in 5 seconds");
                await Task.Delay(TimeSpan.FromSeconds(5));
            }

        }

        if (stream is not null)
        {
            stream.Close();
            await stream.DisposeAsync();
        }

        client.CancelPendingRequests();
    }

    /// <summary>
    /// After reading stream the returened data is processed and required operations are done
    /// </summary>
    /// <param name="data">Data read from the stream</param>
    public void ProcessEvents(string data)
    {
        
        if (data == "")
            return;

        var val = data.Split(newLineChar);
        if (string.IsNullOrWhiteSpace(_cleintId)) _cleintId = val[0].Split(":")[1];

        var _event = val[1].Split(":")[1];
        var Data = val[2][(val[2].IndexOf(":") + 1)..];

        var args = new RealtimeEventArgs()
        {
            id = _cleintId,
            @event = _event,
            data = JsonSerializer.Deserialize<Data>(Data) ?? new()
        };

        if (!string.IsNullOrWhiteSpace(_event) && _event == "PB_CONNECT")
        {
            AddRemoveTopics();
        }
        else
        {
            ProcessCallBacks(args);
        }
    }
}