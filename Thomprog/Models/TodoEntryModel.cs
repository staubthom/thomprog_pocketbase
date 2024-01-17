using pocketbase_csharp_sdk.Models;

namespace Thomprog.Models
{
    public class TodoEntryModel : BaseModel
    {
        public string? name { get; set; }
        public bool isDone { get; set; }
        public string? todoId { get; set; }
    }
}
