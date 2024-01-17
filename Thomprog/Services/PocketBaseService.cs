using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components;
using MudBlazor;
using pocketbase_csharp_sdk;
using System.Collections;
using pocketbase_csharp_sdk.Models;
using pocketbase_csharp_sdk.Models.Files;

namespace Thomprog.Services
{

    public class PocketBaseService
        {

        [Inject]
        public PocketBase PocketBase { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<DatabaseService> _logger;
        private readonly IDialogService _dialogService;
        
        private readonly PocketBase _PocketBase;

        public PocketBaseService(           

           ILocalStorageService localStorage,
           ILogger<DatabaseService> logger,
           IDialogService dialogService,
         
           PocketBase PocketBase)
        {
            logger.LogInformation("------------------- CONSTRUCTOR Pocketbaseservices -------------------");

            _localStorage = localStorage;
            _logger = logger;
            _dialogService = dialogService;
           
            _PocketBase = PocketBase;

        }

        private static Dictionary<Type, CollectionBase> RegisteredCollections { get; } = new Dictionary<Type, CollectionBase>();

        //----------------------------USER LOGIN---------------------------------------------------------------------------
        public async Task<FluentResults.Result<pocketbase_csharp_sdk.Models.Auth.UserAuthModel>> Login(string username, string password)
        {
            Console.WriteLine("Login in PocketbaseService");            
            return  await _PocketBase.User.AuthenticateWithPasswordAsync(username!, password!);

        }


         public async Task<FluentResults.Result<pocketbase_csharp_sdk.Models.Auth.UserAuthModel>>Refresh ()
        {
            Console.WriteLine("Login Refersh in PocketbaseService");            
            return  await _PocketBase.User.RefreshAsync();

        }

        public async Task<Thomprog.Models.UserModel>GetUserModel( )
        {
          var result = await _PocketBase.User.ListAsync();
          var user = result.Value.Items.FirstOrDefault();

            var userModel = new Models.UserModel
            {
                avatar = user.Avatar,
                email = user.Email,
                userid = user.Id,
                username = user.Username,

            };

            //_util.Log(result.Value.Items.FirstOrDefault());
            return userModel;

        }

        //----------------------------CRUD-------------------------------------------------------------------------------

        public async Task<IEnumerable<T>> GetCollection<T> (string tablename, string? filter = null) where T : BaseModel, new()
        {

            var currentUserResult = await _PocketBase.Collection(tablename).GetFullListAsync<T>( filter:filter);
            if (currentUserResult.IsSuccess)
            {
                //Console.WriteLine("currentUserResult");
                //_util.Log(currentUserResult);
                //Console.WriteLine("currentUserResult.Value");
                //_util.Log(currentUserResult.Value);   
                return (IEnumerable<T>)currentUserResult.Value;
            }
            else
            {
                return Enumerable.Empty<T>();
            }

            //return (T)Convert.ChangeType(modeledResponse, typeof(T));

        }

        public async Task<IEnumerable<T>> GetRecordById<T>(string tablename, string id) where T : BaseModel, new()
        {
            //Console.WriteLine("PocketBaseService GetRecordById");
            var currentUserResult = await _PocketBase.Collection(tablename).GetOneAsync<T>(id);
            if (currentUserResult.IsSuccess)
            {
                //Console.WriteLine("currentUserResult");
                //_util.Log(currentUserResult);
                //_util.Log(currentUserResult);

                //Console.WriteLine("currentUserResult.Value");
                //_util.Log(currentUserResult.Value);            
                return (IEnumerable<T>)Enumerable.Repeat(currentUserResult.ValueOrDefault,1);
                //return (T)Convert.ChangeType(modeledResponse, typeof(T));
            }
            else
            {
                return Enumerable.Empty<T>();
            }

        }

        public async Task<IEnumerable<T>> GetRecordByUserId<T>(string tablename, string userid, string? filter = null) where T : BaseModel, new()
        {
            //Console.WriteLine("PocketBaseService GetRecordByUserId");
            // Möchlich wäre filter: $"todo_id.id='{Id}'"
            string filtertext = "user_id='"+userid+"'";
            var currentUserResult = await _PocketBase.Collection(tablename).GetFullListAsync<T>(filter: filtertext);
            if (currentUserResult.IsSuccess)
            {
                // Console.WriteLine("modeledResponse");
                //_util.Log(currentUserResult);
                //_util.Log(currentUserResult.ValueOrDefault);
                //Console.WriteLine("currentUserResult End");
                //Console.WriteLine("currentUserResult.Value");
                //_util.Log(currentUserResult.Value);            
                return currentUserResult.Value;
                //return (T)Convert.ChangeType(currentUserResult, typeof(T));
            }
            else
            {
                return Enumerable.Empty<T>();
            }

        }

        public async Task<IEnumerable<T>> Insert<T>(string tablename,T item, IEnumerable<IFile>? files = null) where T : BaseModel, new()
        {
            //Console.WriteLine("PocketBaseService Insert");            
            var currentUserResult  = await  _PocketBase.Collection(tablename).CreateAsync<T>(item, files: files);
            if (currentUserResult.IsSuccess)
            {
                //Console.WriteLine("currentUserResult");
                //_util.Log(currentUserResult);
                //_util.Log(currentUserResult.Value);
                //Console.WriteLine("currentUserResult.Value");
                //_util.Log(currentUserResulte.Value);            
                return (IEnumerable<T>)Enumerable.Repeat(currentUserResult.Value, 1);
                //return (T)Convert.ChangeType(currentUserResult, typeof(T));
            }
            else
            {
                return Enumerable.Empty<T>();
            }

        }

        public async Task<IEnumerable<T>> Update<T>(string tablename, T item) where T : BaseModel, new()
        {
            //Console.WriteLine("PocketBaseService Update");
            //_util.Log(item);
            //_util.Log(typeof(T));
            var currentUserResult = await _PocketBase.Collection(tablename).UpdateAsync<T>(item);
            if (currentUserResult.IsSuccess)
            {
                //await _PocketBase.Collection(tablename).UpdateAsync<T>(item);
                //Console.WriteLine("currentUserResult");
                //_util.Log(modeledResponse);
                //_util.Log(currentUserResult.Value);

                //Console.WriteLine("currentUserResult.Value");
                //_util.Log(currentUserResult.Value);            
                return (IEnumerable<T>)Enumerable.Repeat(currentUserResult.Value, 1);
                //return (T)Convert.ChangeType(currentUserResult, typeof(T));
            }
            else
            {
                return Enumerable.Empty<T>();
            }
        }

        public async Task<IEnumerable<T>> UpdateAsyncWithFile<T>(string tablename, T item, IEnumerable<IFile>? files = null) where T : BaseModel, new()
        {
            //Console.WriteLine("PocketBaseService UpdateWithFile");
           
            //_util.Log(typeof(T));
            var currentUserResult = await _PocketBase.Collection(tablename).UpdateAsyncWithFile<T>(item, files: files);

            if (currentUserResult.IsSuccess)
            {
                //await _PocketBase.Collection(tablename).UpdateAsync<T>(item);
                //Console.WriteLine("currentUserResult");
                //_util.Log(modeledResponse);
                //_util.Log(currentUserResult.Value);

                //Console.WriteLine("currentUserResult.Value");
                //_util.Log(currentUserResult.Value);            
                return (IEnumerable<T>)Enumerable.Repeat(currentUserResult.Value, 1);
                //return (T)Convert.ChangeType(currentUserResult, typeof(T));
            }
            else
            {
                return Enumerable.Empty<T>();
            }
        }

        public async Task<Stream> DownloadFileAsync(string tablename, string fileid, string file) 
        {
            //Console.WriteLine("PocketBaseService DownloadFileAsync");
            //_util.Log(item);
            //_util.Log(typeof(T));
            var currentResult = await _PocketBase.Collection(tablename).DownloadFileAsync(fileid, file);            

            if (currentResult.IsSuccess)
            {
                //await _PocketBase.Collection(tablename).UpdateAsync<T>(item);
                //Console.WriteLine("currentUserResult");
                //_util.Log(modeledResponse);
                //_util.Log(currentUserResult.Value);

                //Console.WriteLine("currentUserResult.Value");
                //_util.Log(currentUserResult.Value);            
                return currentResult.Value;
                //return (T)Convert.ChangeType(currentUserResult, typeof(T));
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetRecordWhereRecordContains<T>(string tablename, string filtertext) where T : BaseModel, new()
        {

            //Console.WriteLine("PocketBaseService GetRecordWhereRecordContains");           
            var currentUserResult = await _PocketBase.Collection(tablename).GetFullListAsync<T>(filter: filtertext);

            if (currentUserResult.IsSuccess)
            {
                //Console.WriteLine("currentUserResult");
                //_util.Log(currentUserResult);
                // _util.Log(currentUserResult.ValueOrDefault);
                //Console.WriteLine("currentUserResult End");
                //Console.WriteLine("currentUserResult.Value");
                //_util.Log(currentUserResult.Value);            
                return (IEnumerable<T>)Enumerable.Repeat(currentUserResult.ValueOrDefault, 1);
                //return (T)Convert.ChangeType(currentUserResult, typeof(T));
            }
            else
            {
                return Enumerable.Empty<T>();
            }
        }

        public async Task<IEnumerable<T>> Delete<T>(string tablename, T item) where T : BaseModel, new()
        {
            //Console.WriteLine("PocketBaseService Delete");
            //_util.Log(item);
            //_util.Log(typeof(T));
            //var currentUserResult = await _PocketBase.Collections.DeleteAsync(item.Id);
            var currentUserResult = await _PocketBase.Collection(tablename).DeleteAsync<T>(item);
            if (currentUserResult.IsSuccess)
            {
                //Console.WriteLine("currentUserResult");
                // _util.Log(currentUserResult);
                //_util.Log(currentUserResult.Value);
                //Console.WriteLine("currentUserResult.Value");
                //_util.Log(currentUserResult.Value);            
                return (IEnumerable<T>)currentUserResult;
                //return (T)Convert.ChangeType(currentUserResult, typeof(T));
            }
            else
            {
                return Enumerable.Empty<T>();
            }
        }


        public async Task<int> GetCollectionCount<T>(string tablename, string? filter = null) where T : BaseModel, new()
        {

            var currentUserResult = await _PocketBase.Collection(tablename).GetFullListAsync<T>(filter: filter);
            if (currentUserResult.IsSuccess)
            {
                
                var count = currentUserResult.Value.Count<T>();

                    //Console.WriteLine("currentUserResult");
                //_util.Log(currentUserResult);
                //Console.WriteLine("currentUserResult.Value");
                //_util.Log(currentUserResult.Value);   
                return count;
            }
            else
            {
                return 0;
            }
           

            //return (T)Convert.ChangeType(modeledResponse, typeof(T));

        }

        /*  internal static async Task<bool> HttpDeleteAsync(this PocketBase pocketBase, string url)
        {
            try { await pocketBase.SendAsync(url, HttpMethod.Delete); }
            catch { return false; }
            return true;
        }
        internal static bool HttpDelete(this PocketBase pocketBase, string url)
        {
            try { pocketBase.Send(url, HttpMethod.Delete); }
            catch { return false; }
            return true;
        }
        */

    }

}
