using Microsoft.AspNetCore.Mvc;

namespace CoreMVCTutorial.Controllers
{
    public class FirstController : Controller
    {
        public IActionResult FirstControlMethod()
        {
            return View();
        }

        //Convention Based Routing
        /* This is written in Program.cs
         *  app.MapControllerRoute(
         *       name: "default", // A name for this route rule
         *       pattern: "{controller=Home}/{action=Index}/{id?}" // The URL pattern
         *  );
         */

        //The Below is attribute Based Routing Example
        //[Route("First/ParsingArgument/{num:int}")]
        public IActionResult ParsingArgument(int num)
        {
            //Using ViewBag
            //ViewBag.Number = num;

            //Using ViewData
            ViewData["Number"] = num;
            return View();
        }

    }
}
