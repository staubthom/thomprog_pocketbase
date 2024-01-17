using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;
using Newtonsoft.Json.Linq;

public static class WebAssemblyHostExtension
{

    public async static Task SetDefaultCulture(this WebAssemblyHost host)
    {
        //Console.WriteLine("SetDefaultCulture");

        //await JSRuntime.InvokeVoidAsync("localStorageFunctions.setItem", "key", data);
        //var storedValue = await JSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", "key");

        var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
        //Console.WriteLine("SetDefaultCulture1");

        var preferences = await jsInterop.InvokeAsync<string>("localStorageFunctions.getItem", "preferences");

        CultureInfo culture;
        if (preferences != null)
        {
            dynamic data = JObject.Parse(preferences);
            string CurrentLanguage = data.languageCode;
            //var CurrentLanguage = preferences.languageCode;

            //dynamic data = JObject.Parse(result);
            //string cu = data.LanguageCode;

            culture = new CultureInfo(CurrentLanguage);
        } 
        else
        {
            culture = new CultureInfo("en-US");
            //culture = new CultureInfo(LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US");
        }

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}

//https://code-maze.com/localization-in-blazor-webassembly-applications/amp/