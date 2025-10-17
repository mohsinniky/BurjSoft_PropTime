namespace MVC_Application.Models.FilesModels
{
    public class FileCreateRequest
    {
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string? Extension { get; set; } = ".txt";
    }
}
