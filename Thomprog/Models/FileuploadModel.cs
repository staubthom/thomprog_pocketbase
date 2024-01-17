using pocketbase_csharp_sdk.Models;

namespace Thomprog.Models
{

    public class FileuploadModel : BaseModel
    {

        public string? file { get; set; }

        public string? name { get; set; }

        public string? userid { get; set; }
        public string? virtualFolder { get; set; }
        public string? filetype { get; set; }

        public bool? no_edit { get; set; }

    }

}

