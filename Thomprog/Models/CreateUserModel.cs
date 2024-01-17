using pocketbase_csharp_sdk.Models;
namespace Thomprog.Models
{
    public class CreateUserModel : BaseModel
    {

        public string? username { get; set; }
        public string? Email { get; set; }
        public string? password { get; set; }
        public string? passwordConfirm { get; set; }
        public string? oldPassword { get; set; }
    }
}
