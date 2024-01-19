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

        _DatabaseService = DatabaseService;
        _logger = logger;
        _dialogService = dialogService;

        _localStorage = localStorage;
    }

    public Thomprog.Components.ThemeManager.ClientPreference? themePreference = new();
    public event Action OnChange;
    public bool isDarkMode { get; private set; } = false;

    //Get the Preferences from the Database
    public async Task<bool> GetPreference()
    {

        var userid = await _DatabaseService.GetUserId();  // _localStorage.GetItemAsync<string>("Id");    



        if (userid != null)
        {
            var preference = await _DatabaseService.GetRecordByUserId<Thomprog.Components.ThemeManager.ClientPreference>("preferences", userid);


            if ((preference != null))
            {
                themePreference = preference.FirstOrDefault();
                await _localStorage.SetItemAsync("preferences", themePreference);

            }
        }
        else
        {

            themePreference.isDarkMode = false;
            themePreference.borderRadius = 5;
            themePreference.primaryColor = "#3eaf7c";
            themePreference.secondaryColor = "#2196F3";
            themePreference.languageCode = "en-US";

        }
        NotifyStateChanged();
        return true;

    }

    public async Task SetDarkMode(bool _isDarkMode)
    {
        this.themePreference.isDarkMode = !_isDarkMode;
        await SavePreference();
    }

    public async Task SetLanguage(string Language)
    {

        this.themePreference.languageCode = Language;
        await SavePreference();

    }
    public async Task SetThemeBorderRadius(int radius)
    {
        this.themePreference.borderRadius = radius;
        await SavePreference();
    }

    public async Task SetThemePrimaryColor(string color)
    {
        this.themePreference.primaryColor = color;
        await SavePreference();
    }

    public async Task SetThemeSecundaryColor(string color)
    {
        this.themePreference.secondaryColor = color;
        await SavePreference();
    }

    //Save to DB
    public async Task SavePreference()
    {


        if (themePreference.user_id != "")
        {
            await _DatabaseService.Update("preferences", themePreference);

            await _localStorage.SetItemAsync("preferences", themePreference);

        }
        NotifyStateChanged();

    }
    private void NotifyStateChanged() => OnChange?.Invoke();

}
