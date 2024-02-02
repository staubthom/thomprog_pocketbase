using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


using static System.Runtime.InteropServices.JavaScript.JSType;

namespace pocketbase_csharp_sdk.Models
{
    public class RealtimeEventArgs : EventArgs
    {
        public string id { get; set; } = string.Empty;
        public string @event { get; set; } = string.Empty;//this rule violation not fixed as it conflicts with event keyword
                                                          //public Dictionary<string, object> data { get; set; } = new();

        public Data data { get; set; } = new();

        public override string ToString()
        {
            return JsonSerializer.Serialize(this.MemberwiseClone());
        }
    }


    public class Data
    {
        public string action { get; set; } = "";
        public Dictionary<string, object> record { get; set; } = new();
    }

    /// <summary>
    /// Stuct contains Actions done on topic
    /// </summary>
    public struct RealtimeActions
    {
        public const string Create = "create";
        public const string Update = "update";
        public const string Delete = "delete";

    }
}
