using Microsoft.AspNetCore.Mvc;
using MVC_Application.Models;
using MVC_Application.ViewModels;
using System.Diagnostics;

namespace MVC_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult DataView()
        {
            var teachers = new List<Teacher>
            {
                new Teacher { Id = 1, TeacherName = "T_Name_First", TeacherCity = "T_City_First" },
                new Teacher { Id = 2, TeacherName = "T_Name_Second", TeacherCity = "T_City_Second" }
            };

            var students = new List<Student>
            {
                new Student { Id = 1, StudentName = "S_Name_First", StudentCity = "S_City_First" },
                new Student { Id = 2, StudentName = "S_Name_Second", StudentCity = "S_City_Second" }
            };

            var viewModel = new SchoolViewModel
            {
                Teachers = teachers,
                Students = students
            };

            return View(viewModel);
        }

        public IActionResult DTOExample()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, StudentName = "S_Name_First", StudentCity = "S_City_First" },
                new Student { Id = 2, StudentName = "S_Name_Second", StudentCity = "S_City_Second" }
            };
            var dtoStudents = students.Select(s => new DTO_Student
            {
                Name = s.StudentName
            }).ToList();

            return View(dtoStudents);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
