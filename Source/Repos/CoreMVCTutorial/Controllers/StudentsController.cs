using Microsoft.AspNetCore.Mvc;

namespace CoreMVCTutorial.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Details() 
        {
            return View();
        }


        public IActionResult DisplayList()
        {
            return View("List");
        }


        public IActionResult DisplayDetail()
        {
            return View("Views/Students/Details.cshtml");
        }

        public string SayMyName()
        {
            return "Mohsin Raza";
        }

        public string Fetch(int num)
        {
            return $"{num} this value is given by you";
        }
    }
}
