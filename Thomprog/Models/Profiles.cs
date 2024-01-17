using pocketbase_csharp_sdk.Models;

namespace Thomprog.Models
{

    public class Profiles : BaseModel
    {

        public string? email { get; set; }

        public string? first_name { get; set; }

        public string? last_name { get; set; }

        public string? phone { get; set; }

        public string? avatar { get; set; }

        public string? website { get; set; }

        public string? language { get; set; }
        public string? user_id { get; set; }

    }

}

