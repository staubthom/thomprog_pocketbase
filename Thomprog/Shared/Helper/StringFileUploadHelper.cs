

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using pocketbase_csharp_sdk.Models.Files;

namespace Thomprog.Shared.Helper
{

    public class StringStreamFileWrapper : IFile
    {
        public string? FieldName { get; set; }
        public string? FileName { get; set; }
        private readonly Stream _browserFile;

        public StringStreamFileWrapper(Stream browserFile,  string fieldname , string filename)
        {
            _browserFile = browserFile;
            FieldName = fieldname; 
            FileName = filename;
        }

        public Stream? GetStream()
        {

            return _browserFile; // Allows Max File Size of 20 Mb.
        }
    }

    public class StringFileUploadHelper
    {
        public static IEnumerable<IFile> ConvertToIFileEnumerable(Stream file, string fieldname, string filename)
        {
            var fileWrapper = new StringStreamFileWrapper(file, fieldname, filename);
            return new List<IFile> { fileWrapper };
        }
    }
}
