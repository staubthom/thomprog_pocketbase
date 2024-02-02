using pocketbase_csharp_sdk.Models;

namespace pocketbase_csharp_sdk.Services.Interfaces;

public interface IRealtimeServiceBase
{
    void Dispose();
    void Subscribe(string topic, Action<RealtimeEventArgs> callback, string collectioName);
    void UnSubscribe(string topic);
}