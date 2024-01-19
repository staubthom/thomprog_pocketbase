using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;
using Newtonsoft.Json.Linq;

public static class WebAssemblyHostExtension
{

    public async static Task SetDefaultCulture(this WebAssemblyHost host)
    {


        var jsInterop = host.Services.GetRequiredService<IJSRuntime>();

        var preferences = await jsInterop.InvokeAsync<string>("localStorageFunctions.getItem", "preferences");

        CultureInfo culture;
        if (preferences != null)
        {
            dynamic data = JObject.Parse(preferences);
            string CurrentLanguage = data.languageCode;

            culture = new CultureInfo(CurrentLanguage);
        }
        else
        {
            culture = new CultureInfo("en-US");

        }

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}

