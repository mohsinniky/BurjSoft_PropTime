using Microsoft.AspNetCore.Mvc;

namespace CoreMVCTutorial.Controllers
{
    public class SecondController : Controller
    {
        public IActionResult SecondControlMethod() 
        { 
            return View(); 
        }
        //Attribute Based Routing
    }
}
