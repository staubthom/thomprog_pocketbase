using System.Net.Http;
using pocketbase_csharp_sdk.Services.Interfaces;

namespace pocketbase_csharp_sdk.Services;

internal class RealtimeService : RealtimeServiceBase, IRealtimeServiceBase
{
    public RealtimeService(HttpClient httpClient, string baseUrl) : base(httpClient, baseUrl)
    {
    }
}
