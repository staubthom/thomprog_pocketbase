using Blazored.LocalStorage;
using Thomprog;
using Thomprog.Options;
using Thomprog.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor;
using MudBlazor.Services;
using pocketbase_csharp_sdk;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Make the loaded config available via dependency injection
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var pbConfigurationSection = builder.Configuration.GetSection(PocketBaseOptions.Position);

builder.Services.Configure<PocketBaseOptions>(pbConfigurationSection);

var pbOptions = pbConfigurationSection.Get<PocketBaseOptions>();

//Authentication
builder.Services.AddScoped<AuthenticationStateProvider, PocketBaseAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddSingleton<PocketBase>(sp =>
{
    return new PocketBase(pbOptions.BaseUrl);
});
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    config.SnackbarConfiguration.HideTransitionDuration = 100;
    config.SnackbarConfiguration.ShowTransitionDuration = 100;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;

});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//Services
builder.Services.AddScoped<DatabaseService>();
builder.Services.AddScoped<PocketBaseService>();



//builder.Services.AddScoped<StorageService>();
builder.Services.AddScoped<PreferenceService>();
builder.Services.AddScoped<RightsService>();




var host = builder.Build();
await host.SetDefaultCulture();
await host.RunAsync();

//await builder.Build().RunAsync();

