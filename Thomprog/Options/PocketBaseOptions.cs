namespace Thomprog.Options;

public class PocketBaseOptions
{
    public const string Position = "PocketBase";

    public string BaseUrl { get; set; } = "http://127.0.0.1:8090/";  //Achtung auch in wwwroot appsettings.json wechseln
    public long maxFileSize { get; set; } = 20000000; // Allows Max File Size of 20 Mb.
}
