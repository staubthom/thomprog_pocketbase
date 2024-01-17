using pocketbase_csharp_sdk.Models;

namespace Thomprog.Models
{
    public class Rights : BaseModel

    {

        public string? email { get; set; }

        public bool canViewProfile { get; set; }

        public bool canViewAdministrationGroup { get; set; }

        public bool canManageUsers { get; set; }

        public bool canManageRoles { get; set; }
        public string? user_id { get; set; }

    }

}

