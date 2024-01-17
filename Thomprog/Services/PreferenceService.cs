using MudBlazor;
using Microsoft.JSInterop;
using Blazored.LocalStorage;

namespace Thomprog.Services;

public class PreferenceService
{

    IJSRuntime JSRuntime { get; set; }

    private readonly DatabaseService _DatabaseService;
    private readonly ILocalStorageService _localStorage;
    private readonly ILogger<DatabaseService> _logger;
    private readonly IDialogService _dialogService;
   

    public PreferenceService(         
        ILocalStorageService localStorage,
        ILogger<DatabaseService> logger,
        IDialogService dialogService,        
        DatabaseService DatabaseService
       

        )
    {

        Console.WriteLine("PreverenceService");

       _DatabaseService = DatabaseService;       
       _logger = logger;
       _dialogService = dialogService;
    
        _localStorage = localStorage;
    }

    public Thomprog.Components.ThemeManager.ClientPreference? themePreference  = new();
    public event Action OnChange;
    public bool isDarkMode { get; private set; } = false;

    //Get the Preferences from the Database
    public async Task<bool> GetPreference()
    {
        //await localStorage.SetItemAsync("Name", "John Smith");
        //Name = "ID: " + await localStorage.GetItemAsync<string>("ID") + "Name : " + await localStorage.GetItemAsync<string>("Name");
        // await localStorage.ClearAsync(); 

        var userid = await _DatabaseService.GetUserId();//           _localStorage.GetItemAsync<string>("Id");

        //var userid = await JSRuntime.InvokeAsync<string>("localStorageFunctions.getItem", "Id");
        // Verarbeite den gelesenen Wert weiter
        Console.WriteLine(userid);

        // _util.Log(userid);
        if (userid != null)
        {
            var preference = await _DatabaseService.GetRecordByUserId<Thomprog.Components.ThemeManager.ClientPreference>("preferences", userid);
            //List<Thomprog.Components.ThemeManager.ClientPreference> preference = result;
            //_util.Log(preference);

           if ((preference != null) )
            {
                themePreference = preference.FirstOrDefault();
                await _localStorage.SetItemAsync("preferences", themePreference);

            }
        }   
        else
            {

                    themePreference.isDarkMode = false;
                    themePreference.borderRadius= 5;
                    themePreference.primaryColor = "#3eaf7c";
                    themePreference.secondaryColor = "#2196F3";
                    themePreference.languageCode = "en-US";

            } 
        NotifyStateChanged();  
        return  true;

    }  

public async Task SetDarkMode(bool _isDarkMode)
    {
        this.themePreference.isDarkMode = !_isDarkMode;        
        await SavePreference(); 
    }

    public async Task SetLanguage(string Language)
    {
        //ClientPreference myPreferences  = await _localStorage.GetItemAsync<ClientPreference>("thomprog");
        this.themePreference.languageCode = Language;        
        await SavePreference();      

    }
    public async Task SetThemeBorderRadius(int radius){       
        this.themePreference.borderRadius = radius;        
        await SavePreference();  
     }

    public async Task SetThemePrimaryColor(string color){       
        this.themePreference.primaryColor = color;        
        await SavePreference();  
     }

    public async Task SetThemeSecundaryColor(string color){
        this.themePreference.secondaryColor = color;        
        await SavePreference();  
     }

     //Save to DB
    public async Task SavePreference()
    {
        //_util.Log("SavePreference");
       // _util.Log(themePreference);     

        if (themePreference.user_id !="")
        {
            await _DatabaseService.Update("preferences", themePreference);
            //string output = JsonConvert.SerializeObject(themePreference);                
            await _localStorage.SetItemAsync("preferences", themePreference);

        }
        NotifyStateChanged();

    }
    private void NotifyStateChanged() => OnChange?.Invoke();

}
