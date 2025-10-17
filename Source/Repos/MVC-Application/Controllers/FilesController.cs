using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MVC_Application.Models.FilesModels;

namespace MVC_Application.Controllers
{
    public class FilesController : Controller
    {
        private readonly string _filesDirectory;
        private readonly string _metadataFile;
        private readonly Dictionary<string, string> _allowedMimeTypes = new Dictionary<string, string>
        {
            { ".txt", "text/plain" },
            { ".pdf", "application/pdf" },
            { ".doc", "application/msword" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { ".xls", "application/vnd.ms-excel" },
            { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
            { ".gif", "image/gif" },
            { ".zip", "application/zip" },
            { ".rar", "application/x-rar-compressed" }
        };

        public FilesController()
        {
            _filesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            _metadataFile = Path.Combine(_filesDirectory, "metadata.json");

            if (!Directory.Exists(_filesDirectory))
                Directory.CreateDirectory(_filesDirectory);
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Get all files
        [HttpGet]
        public JsonResult GetAllFiles()
        {
            try
            {
                var files = GetFilesMetadata();
                return Json(new { success = true, files });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Create new text file
        [HttpPost]
        public JsonResult CreateFile([FromBody] FileCreateRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                    return Json(new { success = false, message = "File name is required." });

                // Validate extension
                if (string.IsNullOrEmpty(request.Extension))
                    request.Extension = ".txt";

                if (!_allowedMimeTypes.ContainsKey(request.Extension.ToLower()))
                    return Json(new { success = false, message = "File type not allowed." });

                var fileExtension = request.Extension.StartsWith('.') ? request.Extension : "." + request.Extension;
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(_filesDirectory, fileName);

                // Create file with content (for text files) or empty for other types
                if (fileExtension == ".txt" && !string.IsNullOrEmpty(request.Content))
                {
                    System.IO.File.WriteAllText(filePath, request.Content);
                }
                else
                {
                    // Create empty file for non-text files when creating new
                    System.IO.File.WriteAllBytes(filePath, new byte[0]);
                }

                var fileInfo = new FileInfo(filePath);

                // Add to metadata
                var files = GetFilesMetadata();
                var fileMetadata = new FileMetadata
                {
                    Id = Guid.NewGuid().ToString(),
                    OriginalName = request.Name,
                    StoredName = fileName,
                    Extension = fileExtension,
                    CreatedDate = DateTime.Now,
                    Size = fileInfo.Length,
                    MimeType = _allowedMimeTypes[fileExtension.ToLower()]
                };

                files.Add(fileMetadata);
                SaveFilesMetadata(files);

                return Json(new { success = true, message = "File created successfully!", file = fileMetadata });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        // POST: Upload existing file
        [HttpPost]
        public async Task<JsonResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Json(new { success = false, message = "Please select a file." });

                // Check file extension
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (string.IsNullOrEmpty(extension) || !_allowedMimeTypes.ContainsKey(extension))
                    return Json(new { success = false, message = "File type not allowed." });

                // Check file size (10MB limit)
                if (file.Length > 10 * 1024 * 1024)
                    return Json(new { success = false, message = "File size cannot exceed 10MB." });

                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(_filesDirectory, fileName);

                // Save the uploaded file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Add to metadata
                var files = GetFilesMetadata();
                var fileMetadata = new FileMetadata
                {
                    Id = Guid.NewGuid().ToString(),
                    OriginalName = Path.GetFileNameWithoutExtension(file.FileName),
                    StoredName = fileName,
                    Extension = extension,
                    CreatedDate = DateTime.Now,
                    Size = file.Length,
                    MimeType = _allowedMimeTypes[extension]
                };

                files.Add(fileMetadata);
                SaveFilesMetadata(files);

                return Json(new { success = true, message = "File uploaded successfully!", file = fileMetadata });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Upload failed: {ex.Message}" });
            }
        }

        // POST: Update file (only for text files)
        [HttpPost]
        public JsonResult UpdateFile([FromBody] FileUpdateRequest request)
        {
            try
            {
                var files = GetFilesMetadata();
                var file = files.FirstOrDefault(f => f.Id == request.Id);

                if (file == null)
                    return Json(new { success = false, message = "File not found." });

                // Only allow updating text files
                if (file.Extension?.ToLower() != ".txt")
                    return Json(new { success = false, message = "Only text files can be edited." });

                var filePath = Path.Combine(_filesDirectory, file.StoredName);

                // Update file content
                System.IO.File.WriteAllText(filePath, request.Content ?? string.Empty);

                // Update metadata
                file.OriginalName = request.Name;
                file.Size = new FileInfo(filePath).Length;

                SaveFilesMetadata(files);

                return Json(new { success = true, message = "File updated successfully!", file = file });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        // GET: Read file content (only for text files)
        [HttpGet]
        public JsonResult ReadFile(string id)
        {
            try
            {
                var files = GetFilesMetadata();
                var file = files.FirstOrDefault(f => f.Id == id);

                if (file == null)
                    return Json(new { success = false, message = "File not found." });

                var filePath = Path.Combine(_filesDirectory, file.StoredName);

                if (!System.IO.File.Exists(filePath))
                    return Json(new { success = false, message = "File not found." });

                string content = string.Empty;

                // Only read content for text files
                if (file.Extension?.ToLower() == ".txt")
                {
                    content = System.IO.File.ReadAllText(filePath);
                }

                return Json(new
                {
                    success = true,
                    content,
                    file,
                    canEdit = file.Extension?.ToLower() == ".txt"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        // POST: Delete file
        [HttpPost]
        public JsonResult DeleteFile([FromBody] FileDeleteRequest request)
        {
            try
            {
                var files = GetFilesMetadata();
                var file = files.FirstOrDefault(f => f.Id == request.Id);

                if (file == null)
                    return Json(new { success = false, message = "File not found." });

                // Delete physical file
                var filePath = Path.Combine(_filesDirectory, file.StoredName);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                // Remove from metadata
                files.Remove(file);
                SaveFilesMetadata(files);

                return Json(new { success = true, message = "File deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        // GET: Download file
        [HttpGet]
        public IActionResult DownloadFile(string id)
        {
            try
            {
                var files = GetFilesMetadata();
                var file = files.FirstOrDefault(f => f.Id == id);

                if (file == null)
                    return NotFound();

                var filePath = Path.Combine(_filesDirectory, file.StoredName);
                if (!System.IO.File.Exists(filePath))
                    return NotFound();

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var mimeType = file.MimeType ?? "application/octet-stream";

                return File(fileBytes, mimeType, $"{file.OriginalName}{file.Extension}");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET: Get allowed file types
        [HttpGet]
        public JsonResult GetAllowedFileTypes()
        {
            var allowedTypes = _allowedMimeTypes.Keys.Select(ext => new {
                extension = ext,
                mimeType = _allowedMimeTypes[ext]
            }).ToList();

            return Json(new { success = true, fileTypes = allowedTypes });
        }

        private List<FileMetadata> GetFilesMetadata()
        {
            if (!System.IO.File.Exists(_metadataFile))
                return new List<FileMetadata>();

            var json = System.IO.File.ReadAllText(_metadataFile);
            return JsonSerializer.Deserialize<List<FileMetadata>>(json) ?? new List<FileMetadata>();
        }

        private void SaveFilesMetadata(List<FileMetadata> files)
        {
            var json = JsonSerializer.Serialize(files, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(_metadataFile, json);
        }
    }
}