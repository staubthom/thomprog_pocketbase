namespace pocketbase_csharp_sdk.Models.Files
{

    /// <summary>
    /// simple Interface needed for uploading files to PocketBase
    /// </summary>
    public interface IFileAsync
    {
        public string? FieldName { get; set; }
        public string? FileName { get; set; }
        Task<Stream?> GetStreamAsync();

    }
}
