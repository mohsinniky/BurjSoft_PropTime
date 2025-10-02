using Microsoft.AspNetCore.Mvc;
using MVC_Application.Services.Interfaces;
using MVC_Application.ViewModels;
using MVC_Application.Models;

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
            var viewModel = new StudentOperationsViewModel()
            {
                AvailableCourses = await _studentService.GetAllCoursesAsync(),
                SelectedCourseIds = new List<int>(),
                Students = students
            };


            return View(viewModel);
        }

        // POST: Student/Index
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] StudentOperationsViewModel viewModel)
        {
            var newStudent = await _studentService.CreateStudentAsync(viewModel.Student, viewModel.SelectedCourseIds);
            return Json(new { success = true, studentId = newStudent.StudentId, student = newStudent });
        }

        // GET: Student/GetStudentsTable
        public async Task<IActionResult> GetStudentsTable()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return PartialView("_StudentsTable", students);
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }


        // GET: Student/GetStudent/{id}
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            var studentCourses = await _studentService.GetStudentCoursesAsync(id);

            var studentData = new
            {
                studentId = student.StudentId,
                firstName = student.FirstName,
                lastName = student.LastName,
                email = student.Email,
                phoneNumber = student.PhoneNumber,
                courses = studentCourses.Select(c => new { courseId = c.CourseId }).ToList()
            };

            return Json(studentData);
        }

        // POST: Student/Update
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] StudentOperationsViewModel viewModel)
        {
                await _studentService.UpdateStudentAsync(viewModel.Student, viewModel.SelectedCourseIds);
                return Json(new { success = true, message = "Student updated successfully!" });
 
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
