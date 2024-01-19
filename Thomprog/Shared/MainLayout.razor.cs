using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Thomprog.Components.ThemeManager;

namespace Thomprog.Shared;
public partial class MainLayout
{

    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    bool _drawerOpen = true;

    private bool _themeDrawerOpen;
    private MudTheme _currentTheme = new LightTheme();
    public bool UseDarkmode { get; set; } = false;

    private ClientPreference? _themePreference;
    private readonly ILocalStorageService _localStorage;

    protected override async Task OnInitializedAsync()
    {

        //CHeck if user is logged in
        await DatabaseService.authRefresh();
        // var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();



        PreferenceService.OnChange += SetTheme;
        //await PreferenceService.GetPreference();
        //await RightsService.GetRights();
        SetCurrentTheme(@PreferenceService.themePreference);
        RightsService.OnChange += StateHasChanged;


    }

    protected MudThemeProvider? themeProvider = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            //UseDarkmode = await themeProvider!.GetSystemPreference();
            StateHasChanged();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    async void DarkTheme()
    {
        //_isDarkMode = !_isDarkMode;


        await PreferenceService.SetDarkMode(@PreferenceService.themePreference.isDarkMode);

        //StateHasChanged();

    }

    async void SetTheme()
    {

        await ThemePreferenceChanged(@PreferenceService.themePreference);

    }
    private void OnClickLogin()
    {

        NavigationManager.NavigateTo($"/login");
    }

    private void OnClickLogout()
    {

        ((PocketBaseAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsLoggedOut();



        NavigationManager.NavigateTo($"/", forceLoad: true);
        Snackbar.Add("Logout successful");
    }

    private void Profile()
    {
        Navigation.NavigateTo("/account");
    }

    private void SetCurrentTheme(ClientPreference themePreference)
    {

        _currentTheme = themePreference.isDarkMode ? new DarkTheme() : new LightTheme();
        _currentTheme.Palette.Primary = themePreference.primaryColor;
        _currentTheme.Palette.Secondary = themePreference.secondaryColor;
        _currentTheme.LayoutProperties.DefaultBorderRadius = $"{themePreference.borderRadius}px";
        _currentTheme.LayoutProperties.DefaultBorderRadius = $"{themePreference.borderRadius}px";

    }
    public async Task ThemePreferenceChanged(ClientPreference themePreference)
    {
        await RightsService.GetRights();
        SetCurrentTheme(themePreference);
        StateHasChanged();
        //await ClientPreferences.SetPreference(themePreference);
    }

}

