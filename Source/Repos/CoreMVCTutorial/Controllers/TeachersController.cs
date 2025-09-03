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

        //Without Parameters
        public IActionResult GetServertime()
        {
            return Content(DateTime.Now.ToString());
        }


        //With Parameters
        [HttpGet]
        public IActionResult GetGreeting(string name) 
        {
            var message = $"Hello {name}";
            return Json(new { greeting = message });

        }
        //With Parameters
        [HttpPost]
        public IActionResult MultiplyTwoNums(int num1, int num2)
        {
            int result = num1 * num2;
            return Json(new { product = result });
        }

    }
}
