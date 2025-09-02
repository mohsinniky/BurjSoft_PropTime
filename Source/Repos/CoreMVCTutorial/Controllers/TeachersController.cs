using CoreMVCTutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVCTutorial.Controllers
{
    public class TeachersController : Controller
    {
        private static List<Teacher> teachers = new List<Teacher>();

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Courses = new List<string> { "B.Tech", "M.Tech", "MBA", "BBA" };
            ViewBag.Hobbies = new List<string> { "Reading", "Traveling", "Music", "Sports", "Photography" };
            ViewBag.Skills = new List<string> { "C#", "Python", "SQL", "Machine Learning", "Physics", "Research", "Data Analysis" };

            // Ensure model is initialized with empty lists to avoid null reference
            var model = new Teacher
            {
                Hobbies = new List<string>(),
                Skills = new List<string>()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.TeacherId = teachers.Count + 1;
                teachers.Add(teacher);
                return RedirectToAction("Index","Home");
            }
            ViewBag.Courses = new List<string> { "B.Tech", "M.Tech", "MBA", "BBA" };
            ViewBag.Hobbies = new List<string> { "Reading", "Traveling", "Music", "Sports", "Photography" };
            ViewBag.Skills = new List<string> { "C#", "Python", "SQL", "Machine Learning", "Physics", "Research", "Data Analysis" };
            return View(teacher);
        }
    }
}
