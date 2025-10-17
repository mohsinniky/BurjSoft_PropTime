using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MVC_Application.Models.FilesModels;

namespace MVC_Application.Controllers
{
    public class FilesController : Controller
    {
        private readonly string _filesDirectory;
        private readonly string _metadataFile;

        public FilesController()
        {
            _filesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TextFiles");
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

                var fileName = $"{Guid.NewGuid()}.txt";
                var filePath = Path.Combine(_filesDirectory, fileName);

                // Create file with content
                System.IO.File.WriteAllText(filePath, request.Content ?? "Empty file");

                var fileInfo = new FileInfo(filePath);

                // Add to metadata
                var files = GetFilesMetadata();
                var fileMetadata = new FileMetadata
                {
                    Id = Guid.NewGuid().ToString(),
                    OriginalName = request.Name,
                    StoredName = fileName,
                    CreatedDate = DateTime.Now,
                    Size = fileInfo.Length
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

        // POST: Upload existing text file
        [HttpPost]
        public async Task<JsonResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Json(new { success = false, message = "Please select a file." });

                // Check if it's a text file
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (extension != ".txt")
                    return Json(new { success = false, message = "Only .txt files are allowed." });

                var fileName = $"{Guid.NewGuid()}.txt";
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
                    CreatedDate = DateTime.Now,
                    Size = file.Length
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

        // POST: Update file content
        [HttpPost]
        public JsonResult UpdateFile([FromBody] FileUpdateRequest request)
        {
            try
            {
                var files = GetFilesMetadata();
                var file = files.FirstOrDefault(f => f.Id == request.Id);

                if (file == null)
                    return Json(new { success = false, message = "File not found." });

                var filePath = Path.Combine(_filesDirectory, file.StoredName);

                // Update file content
                System.IO.File.WriteAllText(filePath, request.Content);

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

        // GET: Read file content
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

                var content = System.IO.File.ReadAllText(filePath);

                return Json(new
                {
                    success = true,
                    content = content,
                    file = file
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
                return File(fileBytes, "text/plain", $"{file.OriginalName}.txt");
            }
            catch (Exception)
            {
                return NotFound();
            }
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