

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using pocketbase_csharp_sdk.Models.Files;

namespace Thomprog.Shared.Helper
{

    public class BrowserFileWrapper : IFile
    {
        public string? FieldName { get; set; }
        public string? FileName { get; set; }
        private readonly IBrowserFile _browserFile;

        public BrowserFileWrapper(IBrowserFile browserFile,  string fieldname)
        {
            _browserFile = browserFile;
            FieldName = fieldname; 
            FileName = browserFile.Name;
        }

        public Stream? GetStream()
        {

            return _browserFile.OpenReadStream(20000000); // Allows Max File Size of 20 Mb.
        }
    }

    public class FileUploadHelper
    {
        public static IEnumerable<IFile> ConvertToIFileEnumerable(IBrowserFile file, string fieldname)
        {
            var fileWrapper = new BrowserFileWrapper(file, fieldname);
            return new List<IFile> { fileWrapper };
        }
    }
}
