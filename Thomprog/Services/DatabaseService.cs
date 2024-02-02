using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using pocketbase_csharp_sdk;
using pocketbase_csharp_sdk.Models;
using pocketbase_csharp_sdk.Models.Files;
using Thomprog.Components.ThemeManager;
using Thomprog.Models;
using static MudBlazor.CategoryTypes;
using Thomprog.Options;

namespace Thomprog.Services
{
    public class DatabaseService
    {

        [Inject]
        public PocketBase PocketBase { get; set; } = null!;

        [Inject]
        PocketBaseAuthenticationStateProvider PocketBaseAuthenticationStateProvider { get; set; } = null!;

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        private readonly PocketBaseService _client;
        private readonly AuthenticationStateProvider _customAuthStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<DatabaseService> _logger;
        private readonly IDialogService _dialogService;
        

        private readonly PocketBase _PocketBase;

        public DatabaseService(
           PocketBaseService client,
           AuthenticationStateProvider customAuthStateProvider,
           ILocalStorageService localStorage,
           ILogger<DatabaseService> logger,
           IDialogService dialogService,           
           PocketBase PocketBase)
        {
            //logger.LogInformation("------------------- CONSTRUCTOR Databaseservices -------------------");
            _customAuthStateProvider = customAuthStateProvider;
            _localStorage = localStorage;
            _logger = logger;
            _dialogService = dialogService;
            _client = client;
            _PocketBase = PocketBase;
            


        }
        //-------------------------USER--------------------------------------------------------

        public async Task<bool> Login(string username, string password)
        {
            var login = false;
            try
            {
                var result = await _client.Login(username!, password!);
                if (result.IsSuccess)
                {

                    SetUserId(result.ValueOrDefault.Model.Id);
                    SetUserToken(result.ValueOrDefault.Token);
                    login = true;
                    var claims = PocketBaseAuthenticationStateProvider.ParseClaimsFromJwt(result.Value.Token);
                    ((PocketBaseAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated(claims);

                    await GetUserModel();
                    return login;
                }

            }
            catch (Exception ex)
            {

            }
            return login;
        }


        public async Task<bool> authRefresh()
        {
            var login = false;
            try
            {
                var result = await _client.Refresh();
                if (result.IsSuccess)
                {

                    SetUserId(result.ValueOrDefault.Model.Id);
                    SetUserToken(result.ValueOrDefault.Token);

                    login = true;
                    var claims = PocketBaseAuthenticationStateProvider.ParseClaimsFromJwt(result.Value.Token);
                    ((PocketBaseAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated(claims);

                    await GetUserModel();
                    return login;
                }

            }
            catch (Exception ex)
            {

            }
            return login;
        }


        public async Task<bool> CreateUser(string username, string password, string email)
        {

            CreateUserModel newUserModel = new();
            newUserModel.username = username;
            newUserModel.password = password;
            newUserModel.passwordConfirm = password;
            newUserModel.Email = email;

            var result = await Insert<CreateUserModel>("users", newUserModel);
            var id = result.FirstOrDefault<CreateUserModel>().Id;
            if (id != null)
            {
                //-----Create Table Profiles
                Profiles profiles = new Profiles();
                profiles.email = email;
                profiles.user_id = id;
                await Insert<Profiles>("profiles", profiles);

                //-----Create Table Preferences
                ClientPreference clientPreference = new ClientPreference();
                clientPreference.user_id = id;
                clientPreference.isDarkMode = false;
                clientPreference.isDrawerOpen = false;
                clientPreference.primaryColor = "#607D8B";
                clientPreference.secondaryColor = "#00BCD4";
                clientPreference.languageCode = "en-US";
                clientPreference.borderRadius = 5;
                await Insert<ClientPreference>("preferences", clientPreference);

                //-----Create Table Rights
                Thomprog.Models.Rights rights = new Thomprog.Models.Rights();
                rights.user_id = id;
                rights.canViewProfile = true;
                rights.canManageRoles = false;
                rights.canViewAdministrationGroup = false;
                rights.canManageUsers = false;
                await Insert<Thomprog.Models.Rights>("rights", rights);

            }

            return true;

        }

        public string userid { get; set; }
        public string token { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }

        public async Task GetUserModel()
        {
            Models.UserModel result = await _client.GetUserModel();
            avatar = result.avatar;
            email = result.email;
            userid = result.userid;
            username = result.username;

        }

        public async Task<string> GetEmail()
        {
            await GetUserModel();
            return email;
        }

        public async Task<string> GetAvatar()
        {
           
           
            userid = await _localStorage.GetItemAsync<string>("Id");
            string ImageUri = "";
            var profiles_ienum = await GetRecordByUserId<Profiles>("profiles", userid);
            List<Profiles> profiles = profiles_ienum.ToList();
            foreach (var item in profiles)
            {               
                var avatar = item.avatar;            
                if (!string.IsNullOrEmpty(avatar))
                {
                    ImageUri  = "api/files/" + item.CollectionId + "/" + item.Id + "/" + item.avatar;
                }
                else
                {
                    ImageUri = "";
                }    
            }  
            return ImageUri;
        }
        public async Task<string> GetUsername()
        {
            await GetUserModel();
            return username;
        }

        public async Task<string> GetFirstName()
        {
            return "Fritz";
        }
        public async Task<string> GetLastName()
        {
            return "Meyer";
        }
        public async Task<string> GetUserId()
        {
            userid = await _localStorage.GetItemAsync<string>("Id");
            return userid;
        }

        public async Task<string> GetUserToken()
        {
            return token;
        }

        public async void SetUserId(string uuid)
        {
            userid = uuid;
            await _localStorage.SetItemAsync<string>("Id", uuid);
        }
        public async void SetUserToken(string _token)
        {
            token = _token;
            await _localStorage.SetItemAsync<string>("token", _token);
        }

        //--------------------------CRUD--------------------------------------------------

        public async Task<IEnumerable<T>> GetCollection<T>(string tablename, string? filter = null) where T : BaseModel, new()
        {

            var modeledResponse = await _client.GetCollection<T>(tablename, filter: filter);
            return modeledResponse;
        }

        public async Task<IEnumerable<T>> GetRecordById<T>(string tablename, string id) where T : BaseModel, new()
        {

            var modeledResponse = await _client.GetRecordById<T>(tablename, id);
            return modeledResponse;
        }

        public async Task<IEnumerable<T>> GetRecordByUserId<T>(string tablename, string userid) where T : BaseModel, new()
        {

            var modeledResponse = await _client.GetRecordByUserId<T>(tablename, userid);
            return modeledResponse;
        }

        public async Task<IEnumerable<T>> Insert<T>(string tablename, T item, IEnumerable<IFile>? files = null) where T : BaseModel, new()
        {

            var modeledResponse = await _client.Insert(tablename, item, files);
            return modeledResponse;

        }

        public async Task<IEnumerable<T>> Update<T>(string tablename, T item) where T : BaseModel, new()
        {

            var modeledResponse = await _client.Update(tablename, item);
            return modeledResponse;
        }

        public async Task<IEnumerable<T>> UpdateAsyncWithFile<T>(string tablename, T item, IEnumerable<IFile>? files = null) where T : BaseModel, new()
        {

            var modeledResponse = await _client.UpdateAsyncWithFile<T>(tablename, item, files);
            return modeledResponse;
        }

        public async Task<IEnumerable<T>> GetRecordWhereRecordContains<T>(string tablename, string search) where T : BaseModel, new()
        {

            var modeledResponse = await _client.GetRecordWhereRecordContains<T>(tablename, search);

            return modeledResponse;

        }

        public async Task<Stream> DownloadFileAsync(string tablename, string fileid, string file)
        {

            return await _client.DownloadFileAsync(tablename, fileid, file);

        }

        public async Task<IEnumerable<T>> Delete<T>(string tablename, T item) where T : BaseModel, new()
        {

            var modeledResponse = await _client.Delete(tablename, item);
            return modeledResponse;

        }

        public async Task<int> GetCollectionCount<T>(string tablename, string? filter = null) where T : BaseModel, new()
        {

            var modeledResponse = await _client.GetCollectionCount<T>(tablename, filter);
            return modeledResponse;
        }


        




        public async Task Realtime(string tablename, string topic, Action<RealtimeEventArgs> callbackFun) 
        {
            await authRefresh();
             _client.Subscribe(tablename,topic, callbackFun);
        }

       

    }
}
