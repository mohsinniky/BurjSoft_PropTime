namespace MVC_Application.Models.FilesModels
{
    public class FileUpdateRequest
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public string? Extension { get; set; }
    }
}
