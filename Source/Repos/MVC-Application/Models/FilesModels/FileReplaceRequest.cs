namespace MVC_Application.Models.FilesModels
{
    public class FileReplaceRequest
    {
        public string? Id { get; set; }
        public IFormFile? File { get; set; }
    }
}
