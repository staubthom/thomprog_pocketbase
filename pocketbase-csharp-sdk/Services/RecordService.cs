using FluentResults;
using pocketbase_csharp_sdk.Services.Interfaces;
using pocketbase_csharp_sdk.Enum;
using pocketbase_csharp_sdk.Services.Base;
using pocketbase_csharp_sdk.Models;

namespace pocketbase_csharp_sdk.Services
{
    public class RecordService : BaseSubCrudService
    {

        protected override string BasePath(string? path = null)
        {
            var encoded = UrlEncode(path);
            return $"/api/collections/{encoded}/records";
        }

        private readonly PocketBase _client;
        readonly string _collectionName;
        private IRealtimeServiceBase _realtimeService;



        public RecordService(PocketBase client, string collectionName, IRealtimeServiceBase realtimeService) : base(client, collectionName)
        {
            this._collectionName = collectionName;
            this._client = client;
            this._realtimeService = realtimeService;
        }

        private Uri GetFileUrl(string recordId, string fileName, IDictionary<string, object?>? query = null)
        {
            var url = $"api/files/{UrlEncode(_collectionName)}/{UrlEncode(recordId)}/{fileName}";
            return _client.BuildUrl(url, query);
        }

        public Task<Result<Stream>> DownloadFileAsync(string recordId, string fileName, ThumbFormat? thumbFormat = null, CancellationToken cancellationToken = default)
        {
            var url = $"api/files/{UrlEncode(_collectionName)}/{UrlEncode(recordId)}/{fileName}";

            //TODO find out how the specify the actual resolution to resize
            var query = new Dictionary<string, object?>()
            {
                { "thumb", ThumbFormatHelper.GetNameForQuery(thumbFormat) }
            };

            return _client.GetStreamAsync(url, query, cancellationToken);
        }




        public void Subscribe(string topic, Action<RealtimeEventArgs> callbackFun, string token)
        {
           

            _realtimeService.Subscribe(topic, callbackFun, _collectionName, token);
        }


        /// <summary>
        /// Unsubscribe from the current collection
        /// </summary>
        public void UnSubscribe(string topic)
        {
            _realtimeService.UnSubscribe(topic);
        }
        /// <summary>
        /// unsubscribe all listeners from the specified topic
        /// </summary>
        /// <param name="topic">the topic to unsubscribe from</param>


    }
}
