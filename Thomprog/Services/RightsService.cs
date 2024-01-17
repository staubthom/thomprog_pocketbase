using MudBlazor;

using Blazored.LocalStorage;
using Thomprog.Models;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Components;


namespace Thomprog.Services;

public class RightsService
{ 

    [Inject]

    public MudThemeProvider MudThemeProvider { get; set; }

    private readonly DatabaseService _DatabaseService;
    private readonly ILocalStorageService _localStorage;
    private readonly ILogger<DatabaseService> _logger;
    private readonly IDialogService _dialogService;
   

    public RightsService (
        ILocalStorageService localStorage,
        ILogger<DatabaseService> logger,
        IDialogService dialogService,
        DatabaseService DatabaseService
       

        )
    {

        Console.WriteLine("RightsService");

        _DatabaseService = DatabaseService;
        _logger = logger;
        _dialogService = dialogService;
    
        _localStorage = localStorage;
        _dialogService = dialogService;
    }

    public event Action OnChange;
    public bool canViewProfile { get; private set; } = false;
    public bool canViewAdministrationGroup { get; private set; } = false;
    public bool canManageUsers { get; private set; } = false;
    public bool canManageRoles { get; private set; } = false;

    //Get the Preferences from the Database
    public async Task<bool> GetRights()
    {  
       Console.WriteLine("RightService GetRights");
       // var userid = await _localStorage.GetItemAsync<string>("Id");  //Rechteproblem? 

        var userid = await _localStorage.GetItemAsync<string>("Id"); // await _DatabaseService.GetUserId();//   
        if (userid is not null)
          {
             Console.WriteLine("RightService GetUserID() is not null)"); 
            await _DatabaseService.authRefresh();
            var rights = await _DatabaseService.GetRecordByUserId<Rights>("rights", userid); 
            Console.WriteLine(rights);
            foreach (var item in rights)
            {

                    SetcanViewProfile(item.canViewProfile);
                    SetcanViewAdministrationGroup(item.canViewAdministrationGroup);
                    SetcanManageUsers(item.canManageUsers);
                    SetcanManageRoles(item.canManageRoles);

                string output = JsonConvert.SerializeObject(item);
                Console.WriteLine(output); 
                //await _localStorage.SetItemAsStringAsync("thomprog",output );

                }

                Console.WriteLine("RightService User"); 
                return true;
            }
            else
            {
                 Console.WriteLine("RightService No User"); 
                return false;
            }

    }

    public  void SetcanViewProfile(bool _canViewProfile)
    {       

       canViewProfile = _canViewProfile;        
        NotifyStateChanged();    

    }

    public  void SetcanViewAdministrationGroup(bool _canViewAdministrationGroup)
    {       

       canViewAdministrationGroup = _canViewAdministrationGroup;        
        NotifyStateChanged();    

    }

    public  void SetcanManageUsers(bool _canManageUsers)
    {       

       canManageUsers = _canManageUsers;        
        NotifyStateChanged();    

    }

 public  void SetcanManageRoles(bool _canManageRoles)
    {       

       canManageRoles = _canManageRoles;        
        NotifyStateChanged();    

    }

    private void NotifyStateChanged() => OnChange?.Invoke();

    //Rechte werden gleich beim Bearbeiten gespeicher und nicht über diesen Service

}
