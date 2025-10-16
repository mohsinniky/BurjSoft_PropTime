using Microsoft.AspNetCore.Mvc;
using MVC_Application.DTOs;
using MVC_Application.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MVC_Application.Controllers
{
    public class SerializationsController : Controller
    {
        private readonly string _basePath;
        private readonly string _serializationPath;
        public SerializationsController()
        {
            _basePath = Path.Combine(Directory.GetCurrentDirectory(), "FileOperations");
            _serializationPath = Path.Combine(Directory.GetCurrentDirectory(), "SerializationData");

            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
            if (!Directory.Exists(_serializationPath))
            {
                Directory.CreateDirectory(_serializationPath);
            }

        }
        public IActionResult FileOperations()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFile(string fileName, string content)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                ViewBag.Message = "File name is Required.";
                return View("FileOperations");
            }

            var filePath = Path.Combine(_basePath, fileName);

            try
            {
                await System.IO.File.WriteAllTextAsync(filePath, content ?? "Content Was Empty");
                ViewBag.Message = $"File '{fileName}' created successfully at '{filePath}'.";
                ViewBag.FilePath = filePath;
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error creating file: {ex.Message}";
            }
            return View("FileOperations");
        }

        [HttpPost]
        public async Task<IActionResult> ReadFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                ViewBag.Message = "File name is Required that is being read.";
                return View("FileOperations");
            }
            var filePath = Path.Combine(_basePath, fileName);


            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    var content = await System.IO.File.ReadAllTextAsync(filePath);
                    ViewBag.Message = $"File '{fileName}' read successfully.";
                    ViewBag.FileContent = content;
                }
                else
                {
                    ViewBag.Message = $"File '{fileName}' does not exist.";
                }
            }

            catch (Exception ex)
            {
                ViewBag.Message = $"Error reading file: {ex.Message}";
            }
            return View("FileOperations");
        }
        public IActionResult ListFiles()
        {
            try
            {
                var files = Directory.GetFiles(_basePath);
                ViewBag.Files = files.Select(f => Path.GetFileName(f)).ToList();
                ViewBag.Message = $"Found {files.Length} files";
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error listing files: {ex.Message}";
            }
            return View("FileOperations");
        }


        // <---------------- Serialization TO XML and JSON -------------------------->

        public IActionResult SerializationDemo()
        {
            return View();
        }

        // JSON Serialization AJAX Methods
        [HttpPost]
        public async Task<JsonResult> SerializeToJson([FromBody] PersonDTO request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Email))
                {
                    return Json(new { success = false, message = "Name and Email are required" });
                }

                var person = new Person(
                    id: new Random().Next(1, 1000),
                    name: request.Name,
                    email: request.Email,
                    birthDate: DateTime.Parse(request.BirthDate)
                );

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                string jsonString = JsonSerializer.Serialize(person, options);
                string fileName = $"Person_{person.Id}.json";
                string filePath = Path.Combine(_serializationPath, fileName);

                await System.IO.File.WriteAllTextAsync(filePath, jsonString);

                return Json(new
                {
                    success = true,
                    message = $"Person serialized to JSON successfully!",
                    fileName = fileName,
                    jsonContent = jsonString,
                    person = person
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error during serialization: {ex.Message}" });
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeserializeFromJson([FromBody] FileRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.FileName))
                {
                    return Json(new { success = false, message = "File name is required!" });
                }

                string filePath = Path.Combine(_serializationPath, request.FileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return Json(new { success = false, message = $"File '{request.FileName}' not found!" });
                }

                string jsonString = await System.IO.File.ReadAllTextAsync(filePath);

                // Add JsonSerializerOptions to handle camelCase during deserialization
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                var person = JsonSerializer.Deserialize<Person>(jsonString, options);

                return Json(new
                {
                    success = true,
                    message = $"Person deserialized from JSON successfully!",
                    jsonContent = jsonString,
                    person
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error during deserialization: {ex.Message}" });
            }
        }

        // XML Serialization AJAX Methods
        [HttpPost]
        public JsonResult SerializeToXml([FromBody] PersonDTO request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Email))
                {
                    return Json(new { success = false, message = "Name and Email are required!" });
                }

                var person = new Person(
                    id: new Random().Next(1, 1000),
                    name: request.Name,
                    email: request.Email,
                    birthDate: DateTime.Parse(request.BirthDate)
                );

                var xmlSerializer = new XmlSerializer(typeof(Person));
                string fileName = $"person_{person.Id}.xml";
                string filePath = Path.Combine(_serializationPath, fileName);

                using (var writer = new StringWriter())
                {
                    xmlSerializer.Serialize(writer, person);
                    string xmlString = writer.ToString();
                    System.IO.File.WriteAllText(filePath, xmlString);

                    return Json(new
                    {
                        success = true,
                        message = $"Person serialized to XML successfully!",
                        fileName = fileName,
                        xmlContent = xmlString,
                        person = person
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error during XML serialization: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult DeserializeFromXml([FromBody] FileRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.FileName))
                {
                    return Json(new { success = false, message = "File name is required!" });
                }

                string filePath = Path.Combine(_serializationPath, request.FileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return Json(new { success = false, message = $"File '{request.FileName}' not found!" });
                }

                string xmlString = System.IO.File.ReadAllText(filePath);
                var xmlSerializer = new XmlSerializer(typeof(Person));

                using (var reader = new StringReader(xmlString))
                {
                    var person = (Person)xmlSerializer.Deserialize(reader);

                    return Json(new
                    {
                        success = true,
                        message = $"Person deserialized from XML successfully!",
                        xmlContent = xmlString,
                        person = person
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error during XML deserialization: {ex.Message}" });
            }
        }

        // AJAX Method to List Files
        [HttpGet]
        public JsonResult ListSerializedFiles()
        {
            try
            {
                var jsonFiles = Directory.GetFiles(_serializationPath, "*.json")
                    .Select(f => Path.GetFileName(f))
                    .ToArray();

                var xmlFiles = Directory.GetFiles(_serializationPath, "*.xml")
                    .Select(f => Path.GetFileName(f))
                    .ToArray();

                return Json(new
                {
                    success = true,
                    jsonFiles = jsonFiles,
                    xmlFiles = xmlFiles,
                    message = $"Found {jsonFiles.Length} JSON files and {xmlFiles.Length} XML files"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error listing files: {ex.Message}" });
            }
        }
    }
}
