using pocketbase_csharp_sdk.Models;

namespace Thomprog.Models
{
    public class TodoModel : BaseModel
    {
        public string? name { get; set; }
        public bool softDeletet { get; set; }
        public string? description { get; set; }
        public string? color { get; set; }
        public string? avatar { get; set; }

    }
}
