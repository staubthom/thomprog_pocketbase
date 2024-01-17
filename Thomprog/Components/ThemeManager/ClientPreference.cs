

using pocketbase_csharp_sdk.Models;

namespace Thomprog.Components.ThemeManager;

public class ClientPreference : BaseModel
{
    public bool isDarkMode { get; set; }
    public bool isRTL { get; set; }
    public bool isDrawerOpen { get; set; }
    public string primaryColor { get; set; } = CustomColors.Light.Primary;
    public string secondaryColor { get; set; } = CustomColors.Light.Secondary;
    public int borderRadius { get; set; } = 5;
    public string languageCode { get; set; } = "en-US";    
    public string user_id { get; set; } = "";

}

