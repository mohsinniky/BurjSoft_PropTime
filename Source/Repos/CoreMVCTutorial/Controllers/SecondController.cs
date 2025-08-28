using Microsoft.AspNetCore.Mvc;

namespace CoreMVCTutorial.Controllers
{
    [Route("Second")]
    public class SecondController : Controller
    {
        public IActionResult SecondControlMethod()
        {
            return View();
        }
        //Attribute Based Routing

        // Adding Route
        [Route("Details/{age:int?}/{name?}")]
        public IActionResult Details(int age = 19, string name = "Mohsin")
        {
            // Using ViewBag
            ViewBag.Age = age;  
            ViewBag.Name = name;
            return View();
        }
    }
}
