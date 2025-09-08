using Microsoft.AspNetCore.Mvc;

namespace CoreMVCTutorial.Areas.AreaPractice.Controllers
{
    //[Area("AreaPractice")]
    public class HomeController : Controller
    {
        [Area("AreaPractice")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
