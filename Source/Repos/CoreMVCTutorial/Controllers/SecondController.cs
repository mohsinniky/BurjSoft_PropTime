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
        [Route("Details")]
        public IActionResult Details()
        {
            return View();
        }
    }
}
