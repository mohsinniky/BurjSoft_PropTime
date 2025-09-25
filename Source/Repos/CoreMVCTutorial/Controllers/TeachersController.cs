using CoreMVCTutorial.Models;
using CoreMVCTutorial.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVCTutorial.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;

        // Constructor Injection
        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTeachers()
        {
            var teachers = _teacherService.GetAllTeachers();
            return PartialView("_TeachersTable", teachers);
        }

        public IActionResult ShowTeacherModal(int? id)
        {
            var dropdownData = _teacherService.GetDropdownData();
            ViewBag.Courses = dropdownData.Courses;
            ViewBag.Hobbies = dropdownData.Hobbies;
            ViewBag.Skills = dropdownData.Skills;

            Teacher model;
            if (id.HasValue)
            {
                model = _teacherService.GetTeacherById(id.Value) ?? new Teacher
                {
                    HobbiesList = new List<string>(),
                    SkillsList = new List<string>()
                };
            }
            else
            {
                model = new Teacher
                {
                    HobbiesList = new List<string>(),
                    SkillsList = new List<string>()
                };
            }
            return PartialView("_addTeacherModalView", model);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult UpdateTeachers([FromBody] Teacher teacher) // Changed from teacherObject to teacher
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    status = "Error",
                    message = "Model binding failed",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            if (teacher == null)
            {
                return Json(new { status = "Error", message = "No data Received" });
            }

            try
            {
                if (teacher.TeacherId > 0)
                {
                    _teacherService.UpdateTeacher(teacher);
                    return Json(new { status = "Success", message = "Teacher updated successfully!" });
                }
                else
                {
                    _teacherService.CreateTeacher(teacher);
                    return Json(new { status = "Success", message = "Teacher added successfully!" });
                }
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Email already exists"))
            {
                return Json(new { status = "Error", message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult DeleteTeacher(int id)
        {
            try
            {
                var teacher = _teacherService.GetTeacherById(id);
                if (teacher == null)
                    return Json(new { status = "Error", message = "Teacher not found" });
                else
                {
                    _teacherService.DeleteTeacher(id);
                    return Json(new { status = "Success", message = "Teacher deleted successfully!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}