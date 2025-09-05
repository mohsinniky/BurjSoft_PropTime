using CoreMVCTutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVCTutorial.Controllers
{
    public class TeachersController : Controller
    {
        private static List<Teacher> teachers = new List<Teacher>
        {
            new Teacher
            {
                TeacherId = 1,
                FullName = "Mohsin Raza",
                FatherName = "Raza",
                Email = "mohsin@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Phone = "03001234567",
                Password = "StrongPass123",
                Course = "B.Tech",
                Gender = Gender.Male,
                Address = "123 Street, Lahore",
                TermsAndConditions = true,
                Hobbies = new List<string> { "Reading", "Music" },
                Skills = new List<string> { "C#", "SQL" }
            },
            new Teacher
            {
                TeacherId = 2,
                FullName = "Ali Ahmed",
                FatherName = "Ahmed",
                Email = "ali@example.com",
                DateOfBirth = new DateTime(1988, 10, 20),
                Phone = "03111234567",
                Password = "Pass@123",
                Course = "MBA",
                Gender = Gender.Male,
                Address = "456 Street, Karachi",
                TermsAndConditions = true,
                Hobbies = new List<string> { "Sports", "Photography" },
                Skills = new List<string> { "Python", "Machine Learning" }
            }
        };

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Courses = new List<string> { "B.Tech", "M.Tech", "MBA", "BBA" };
            ViewBag.Hobbies = new List<string> { "Reading", "Traveling", "Music", "Sports", "Photography" };
            ViewBag.Skills = new List<string> { "C#", "Python", "SQL", "Machine Learning", "Physics", "Research", "Data Analysis" };

            var model = new Teacher
            {
                Hobbies = new List<string>(),
                Skills = new List<string>()
            };
            ViewBag.Teachers = teachers;
            return View(model);
        }

        public IActionResult ShowTeacherModal()
        {
            ViewBag.Courses = new List<string> { "B.Tech", "M.Tech", "MBA", "BBA" };
            ViewBag.Hobbies = new List<string> { "Reading", "Traveling", "Music", "Sports", "Photography" };
            ViewBag.Skills = new List<string> { "C#", "Python", "SQL", "Machine Learning", "Physics", "Research", "Data Analysis" };

            var model = new Teacher
            {
                Hobbies = new List<string>(),
                Skills = new List<string>()
            };
            return PartialView("_addTeacherModalView", model);
        }

        [HttpGet]
        public IActionResult GetTeachersTable()
        {
            return PartialView("_TeachersTable", teachers);
        }

        // Add teacher to the list
        [HttpPost]
        public IActionResult AddTeacher([FromBody] Teacher teacherObject)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = "Error", message = "Model binding failed", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }
            if (teacherObject == null)
            {
                return Json(new { status = "Error", message = "No data Received" });
            }
            teacherObject.TeacherId = teachers.Count + 1;
            teachers.Add(teacherObject);
            string msg = $"Teacher added successfully!";
            return Json(new { status = "Success", message = msg });
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

        //With Object Such as Teacher
        [HttpPost]
        public IActionResult SaveTeacher([FromBody] Teacher teacherObject)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = "Error", message = "Model binding failed", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }
            if (teacherObject == null)
            {
                return Json(new { status = "Error", message = "No data Received" });
            }
            string msg = $"Saved {teacherObject.FullName}, whose Email is {teacherObject.Email}";
            return Json(new { status = "Success", message = msg });
        }

        //With List Such as Teacher list
        //With Object Such as Teacher
        [HttpPost]
        public IActionResult SaveMultipleTeacher([FromBody] List<Teacher> teacherList)
        {
            if (teacherList == null)
            {
                return Json(new { status = "Error", message = "No data Received" });
            }
            string msg = $"Total Number of Teachers: {teacherList.Count}";
            return Json(new { status = "Success", message = msg });
        }

        // Get teacher data for editing
        [HttpGet]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = teachers.FirstOrDefault(t => t.TeacherId == id);
            if (teacher == null)
                return Json(new { status = "Error", message = "Teacher not found" });
            return Json(new { status = "Success", data = teacher });
        }

        // Update teacher
        [HttpPost]
        public IActionResult UpdateTeacher([FromBody] Teacher updatedTeacher)
        {
            var teacher = teachers.FirstOrDefault(t => t.TeacherId == updatedTeacher.TeacherId);
            if (teacher == null)
                return Json(new { status = "Error", message = "Teacher not found" });

            // Update fields
            teacher.FullName = updatedTeacher.FullName;
            teacher.FatherName = updatedTeacher.FatherName;
            teacher.Email = updatedTeacher.Email;
            teacher.DateOfBirth = updatedTeacher.DateOfBirth;
            teacher.Phone = updatedTeacher.Phone;
            teacher.Password = updatedTeacher.Password;
            teacher.Course = updatedTeacher.Course;
            teacher.Gender = updatedTeacher.Gender;
            teacher.Address = updatedTeacher.Address;
            teacher.TermsAndConditions = updatedTeacher.TermsAndConditions;
            teacher.Hobbies = updatedTeacher.Hobbies;
            teacher.Skills = updatedTeacher.Skills;

            return Json(new { status = "Success", message = "Teacher updated successfully!" });
        }

        // Delete teacher
        [HttpPost]
        public IActionResult DeleteTeacher(int id)
        {
            var teacher = teachers.FirstOrDefault(t => t.TeacherId == id);
            if (teacher == null)
                return Json(new { status = "Error", message = "Teacher not found" });

            teachers.Remove(teacher);
            return Json(new { status = "Success", message = "Teacher deleted successfully!" });
        }
    }
}
