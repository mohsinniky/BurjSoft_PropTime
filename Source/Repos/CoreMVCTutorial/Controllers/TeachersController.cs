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
            return View();
        }

        public IActionResult GetTeachers()
        {
            return PartialView("_TeachersTable", teachers);
        }

        public IActionResult ShowTeacherModal(int? id)
        {
            ViewBag.Courses = new List<string> { "B.Tech", "M.Tech", "MBA", "BBA" };
            ViewBag.Hobbies = new List<string> { "Reading", "Traveling", "Music", "Sports", "Photography" };
            ViewBag.Skills = new List<string> { "C#", "Python", "SQL", "Machine Learning", "Physics", "Research", "Data Analysis" };

            Teacher model;
            if (id.HasValue)
            {
                model = teachers.FirstOrDefault(t => t.TeacherId == id.Value) ?? new Teacher
                {
                    Hobbies = new List<string>(),
                    Skills = new List<string>()
                };
            }
            else
            {
                model = new Teacher
                {
                    Hobbies = new List<string>(),
                    Skills = new List<string>()
                };
            }
            return PartialView("_addTeacherModalView", model);
        }


        // Add teacher to the list
        [HttpPost]
        public IActionResult UpdateTeachers([FromBody] Teacher teacherObject)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = "Error", message = "Model binding failed", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }
            if (teacherObject == null)
            {
                return Json(new { status = "Error", message = "No data Received" });
            }
            if (teacherObject.TeacherId > 0 && teachers.Any(t => t.TeacherId == teacherObject.TeacherId))
            {
                // Update existing teacher
                var existingTeacher = teachers.FirstOrDefault(t => t.TeacherId == teacherObject.TeacherId);
                existingTeacher.FullName = teacherObject.FullName;
                existingTeacher.FatherName = teacherObject.FatherName;
                existingTeacher.Email = teacherObject.Email;
                existingTeacher.DateOfBirth = teacherObject.DateOfBirth;
                existingTeacher.Phone = teacherObject.Phone;
                existingTeacher.Password = teacherObject.Password;
                existingTeacher.Course = teacherObject.Course;
                existingTeacher.Gender = teacherObject.Gender;
                existingTeacher.Address = teacherObject.Address;
                existingTeacher.TermsAndConditions = teacherObject.TermsAndConditions;
                existingTeacher.Hobbies = teacherObject.Hobbies;
                existingTeacher.Skills = teacherObject.Skills;
                return Json(new { status = "Success", message = "Teacher updated successfully!" });
            }
            // Add new teacher
            teacherObject.TeacherId = teachers.Count > 0 ? teachers.Max(t => t.TeacherId) + 1 : 1;
            teachers.Add(teacherObject);
            return Json(new { status = "Success", message = "Teacher added successfully!" });
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