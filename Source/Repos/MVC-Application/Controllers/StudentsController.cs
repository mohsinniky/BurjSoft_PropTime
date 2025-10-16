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
        public async Task<IActionResult> Index(int page = 1, int pageSize = 2)
        {
            var (students, totalCount) = await _studentService.GetStudentsPageAsync(page, pageSize);
            var courses = await _studentService.GetAllCoursesAsync();
            var grades = await _studentService.GetAllGradesAsync();

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
                AvailableGrades = grades,

                Students = students.Select(s => new StudentViewModel
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    GradeId = s.GradeId,
                    GradeName = s.GradeName,
                    StudentAddress = s.StudentAddress != null ? new StudentAddressViewModel
                    {
                        StudentAddressId = s.StudentAddress.StudentAddressId,
                        City = s.StudentAddress.City,
                        State = s.StudentAddress.State,
                        StudentId = s.StudentAddress.StudentId
                    } : new StudentAddressViewModel(),
                    CoursesDisplay = s.CoursesDisplay
                }).ToList(),
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(viewModel);
        }

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
                gradeId = student.GradeId,
                studentAddress = student.StudentAddress != null ? new
                {
                    studentAddressId = student.StudentAddress.StudentAddressId,
                    city = student.StudentAddress.City,
                    state = student.StudentAddress.State
                } : null,
                courses = studentCourses.Select(c => new { courseId = c.CourseId }).ToList()
            };

            return Json(studentData);
        }

        // POST: Student/Upsert - Single endpoint for both create and update
        [HttpPost]
        public async Task<IActionResult> Upsert([FromBody] StudentUpsertDto studentUpsertDto)
        {
            var student = await _studentService.UpsertStudentAsync(studentUpsertDto);

            var studentCourses = await _studentService.GetStudentCoursesAsync(student.StudentId);

            var coursesDisplay = string.Join(", ", studentCourses.Select(c => c.CourseDisplay));

            return Json(new
            {
                success = true,
                student = new
                {
                    studentId = student.StudentId,
                    firstName = student.FirstName,
                    lastName = student.LastName,
                    email = student.Email,
                    phoneNumber = student.PhoneNumber,
                    gradeId = student.GradeId,
                    gradeName = student.GradeName,
                    studentAddress = student.StudentAddress != null ? new
                    {
                        studentAddressId = student.StudentAddress.StudentAddressId,
                        city = student.StudentAddress.City,
                        state = student.StudentAddress.State
                    } : null,
                    coursesDisplay 
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
