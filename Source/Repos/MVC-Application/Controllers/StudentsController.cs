using Microsoft.AspNetCore.Mvc;
using MVC_Application.DTOs;
using MVC_Application.Services.Interfaces;
using MVC_Application.ViewModels;

namespace MVC_Application.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: Student/Index
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();
            var courses = await _studentService.GetAllCoursesAsync();

            var viewModel = new StudentOperationsViewModel()
            {
                StudentForm = new StudentViewModel(),
                AvailableCourses = courses.Select(c => new CourseViewModel
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseCode = c.CourseCode,
                    Description = c.Description
                }).ToList(),
                Students = students.Select(s => new StudentViewModel
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber
                }).ToList()
            };

            return View(viewModel);
        }

        // GET: Student/GetStudent/{id} - For Edit and Details modals
        [HttpGet]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var studentCourses = await _studentService.GetStudentCoursesAsync(id);

            var studentData = new
            {
                studentId = student.StudentId,
                firstName = student.FirstName,
                lastName = student.LastName,
                email = student.Email,
                phoneNumber = student.PhoneNumber,
                courses = studentCourses.Select(c => new
                {
                    courseId = c.CourseId,
                    courseName = c.CourseName,
                    courseCode = c.CourseCode
                }).ToList()
            };

            return Json(studentData);
        }

        // POST: Student/Upsert - Single endpoint for both create and update
        [HttpPost]
        public async Task<IActionResult> Upsert([FromBody] StudentUpsertDto studentUpsertDto)
        {

            var student = await _studentService.UpsertStudentAsync(studentUpsertDto);

            return Json(new
            {
                success = true,
                student = new StudentViewModel
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    PhoneNumber = student.PhoneNumber
                }
            });
        }

        // POST: Student/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (result)
            {
                return Json(new { success = true, message = "Student deleted successfully!" });
            }
            else
            {
                return Json(new { success = false, message = "Error deleting student!" });
            }
        }
    }
}
