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
                Student = new Student(),
                AvailableCourses = await _studentService.GetAllCoursesAsync(),
                SelectedCourseIds = new List<int>()
            };
            
            var indexViewModel = new StudentIndexViewModel
            {
                Students = students,
                StudentForm = viewModel
            };
            return View(indexViewModel);
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

        // POST: Student/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]StudentOperationsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _studentService.CreateStudentAsync(viewModel.Student, viewModel.SelectedCourseIds);
                return Json(new { success = true, message = "Student created successfully!" });
            }

            // If validation fails, return JSON with errors
            var errors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            return Json(new { success = false, errors = errors });
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _studentService.GetStudentFormDataAsync(id);
            if (viewModel.Student == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentOperationsViewModel viewModel)
        {
            if (id != viewModel.Student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _studentService.UpdateStudentAsync(viewModel.Student, viewModel.SelectedCourseIds);
                return RedirectToAction("Index");
            }

            // If we got this far, something failed; redisplay form
            viewModel.AvailableCourses = await _studentService.GetStudentCoursesAsync(0);
            return View(viewModel);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction("Index");
        }
    }
}
