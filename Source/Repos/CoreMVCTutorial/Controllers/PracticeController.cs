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
        public IActionResult StronglyTypedMethod()
        {
            var model = new PracticeModel
            {
                UserName = "John Doe",
                Description = "Default description text",
                IsActive = true,
                Gender = "Male",
                Category = "2",
                UserId = 123,

                // Populate dropdown options
                Categories = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Category 1" },
                    new SelectListItem { Value = "2", Text = "Category 2" },
                    new SelectListItem { Value = "3", Text = "Category 3" }
                },

                    Roles = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Admin", Text = "Administrator" },
                    new SelectListItem { Value = "User", Text = "Regular User" },
                    new SelectListItem { Value = "Guest", Text = "Guest User" }
                },

                    Genders = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                    new SelectListItem { Value = "Other", Text = "Other" }
                }
            };

            return View(model);
        }
    }
}
