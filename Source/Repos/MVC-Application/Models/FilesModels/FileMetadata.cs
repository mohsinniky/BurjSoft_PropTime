namespace MVC_Application.Models.FilesModels
{
    public class FileMetadata
    {
        public string Id { get; set; }
        public string OriginalName { get; set; }
        public string StoredName { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedDate { get; set; }
        public long Size { get; set; }
    }
}
