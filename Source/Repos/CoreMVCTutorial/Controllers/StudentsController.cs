using CoreMVCTutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVCTutorial.Controllers
{
    public class StudentsController : Controller
    {
        // In-memory student list with only three fields
        private static List<Students> students = new List<Students>
        {
            new Students { StudentId = 1, FullName = "Pranaya Rout", DateOfBirth = new DateTime(1990, 1, 1), Branch = Branch.CSE },
            new Students { StudentId = 2, FullName = "Hina Sharma", DateOfBirth = new DateTime(1992, 2, 15), Branch = Branch.ETC },
            new Students { StudentId = 3, FullName = "Anurag Mohanty", DateOfBirth = new DateTime(1988, 11, 23), Branch = Branch.Mechanical }
        };

        // GET: Students/List
        [HttpGet]
        public IActionResult List()
        {
            // Return the list of students
            return View(students);
        }

        // GET: Students/Details/{id}
        [HttpGet]
        public IActionResult Details(int id)
        {
            // Find student by Id
            var student = students.FirstOrDefault(std => std.StudentId == id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        // GET: Students/Register
        [HttpGet]
        public IActionResult Register()
        {
            // Pass available branches to the view
            ViewBag.Branches = Enum.GetValues(typeof(Branch));
            return View(new Students());
        }

        // POST: Students/Register
        [HttpPost]
        public IActionResult Register(Students student)
        {
            if (ModelState.IsValid)
            {
                student.StudentId = students.Count() + 1;
                students.Add(student);
                return RedirectToAction("List");
            }
            ViewBag.Branches = Enum.GetValues(typeof(Branch));
            return View(student);
        }
    }
}
