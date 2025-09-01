using CoreMVCTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreMVCTutorial.Controllers
{
    public class PracticeController : Controller
    {
        public IActionResult RazorSyntax()
        {
            return View();
        }

        public IActionResult ExtensionMethod()
        {
            return View();
        }
        //public IActionResult StronglyTypedMethod()
        //{
        //    var model = new PracticeModel
        //    {
        //        UserName = "John Doe",
        //        Description = "Default description text",
        //        IsActive = true,
        //        Gender = "Male",
        //        Category = "2",
        //        UserId = 123,

        //        // Populate dropdown options
        //        Categories = new List<SelectListItem>
        //        {
        //            new SelectListItem { Value = "1", Text = "Category 1" },
        //            new SelectListItem { Value = "2", Text = "Category 2" },
        //            new SelectListItem { Value = "3", Text = "Category 3" }
        //        },

        //            Roles = new List<SelectListItem>
        //        {
        //            new SelectListItem { Value = "Admin", Text = "Administrator" },
        //            new SelectListItem { Value = "User", Text = "Regular User" },
        //            new SelectListItem { Value = "Guest", Text = "Guest User" }
        //        },

        //            Genders = new List<SelectListItem>
        //        {
        //            new SelectListItem { Value = "Male", Text = "Male" },
        //            new SelectListItem { Value = "Female", Text = "Female" },
        //            new SelectListItem { Value = "Other", Text = "Other" }
        //        }
        //    };

        //    return View(model);
        //}

        // ViewResult
        public ViewResult ViewResultTest()
        {
            return View("RazorSyntax");
        }

        // JsonResult
        public JsonResult JsonResultTest()
        {
            return Json(new { name = "Mohsin Raza", age = 30, city = "Multan" });
        }
        //ContentResult
        public ContentResult ContentResultTest()
        {
            return Content("<h1>Hello World</h1>", "text/html");
        }
        //RedirectResult
        // Redirection to google.com
        public RedirectResult RedirectResultTest()
        {
            return Redirect("https://google.com");
        }

        // RedirectToActionResult
        public RedirectToActionResult RedirectToActionResultTest()
        {
            return RedirectToAction("ExtensionMethod", "Practice");
        }

        // StatusCodeResult
        public StatusCodeResult StatusCodeResultTest()
        {
            return StatusCode(402);
        }
        // EmptyResult
        public EmptyResult EmptyResultTest()
        {
            return new EmptyResult();
        }

        // FileResult
        public FileResult FileResultTest()
        {
            // return File("images/vecteezy_fruit-farm-logo_11186933.jpg", "image/jpg");
            // Returning Downloadable file
            var path = "images/vecteezy_fruit-farm-logo_11186933.jpg";
            var fileName = "Fruit Farm Logo.jpg";
            var contentType = "application/octet-stream"; // generic binary type forces download

            return PhysicalFile(path, contentType, fileName);
        }



    }
}
