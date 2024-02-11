
using pocketbase_csharp_sdk.Models;
using System.Text.Json.Nodes;

namespace Thomprog.Models
{
    
    public class  ChatConversationModel : BaseModel
    {
        



        public string? name { get; set; }
        public string? avatar { get; set; }
        public string? color { get; set; }
         public string? description { get; set; }
        public string? group_id { get; set; }
        public string? user1_id { get; set; }
        public string? message_id { get; set; }
        public bool? is_read { get; set; }
        public string? user2_id { get; set; }

        //public Expand? Expand { get; set; }        
        public Expand expand { get; set; }






    }

    public class UserId
    {
        public string avatar { get; set; }
        public string collectionId { get; set; }
        public string collectionName { get; set; }
        public string created { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string id { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string updated { get; set; }
        public string user_id { get; set; }
        public string website { get; set; }
    }

    public class Expand
    {
        public UserId user1_id { get; set; }
        public UserId user2_id { get; set; }
    }
    
    public class  ChatMessageRecipient : BaseModel
    {
           

        public string? recipient_id { get; set; }
        public string? recipient_group_id { get; set; }
        public bool is_read { get; set; }
        public string? message_id { get; set; }
        
       
    }

    

     public class ChatMessageModel : BaseModel
    {
        public string? subject { get; set; }
        public string? message_body { get; set; }
        public string? parent_message_id { get; set; }
        public string? expire_date { get; set; }
        public string? sender_id { get; set; }
        public bool is_reminder { get; set; }       
        public string? next_reminder_date { get; set; }  
        public int? reminder_frequency_id { get; set; }
        public string? chatconversation_id { get; set; }

    }
    
}
