using Microsoft.AspNetCore.Mvc;
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
            return View(students);
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

        // GET: Student/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = await _studentService.GetStudentFormDataAsync();
            return View(viewModel);
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentOperationsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _studentService.CreateStudentAsync(viewModel.Student, viewModel.SelectedCourseIds);
                return RedirectToAction(nameof(Index));
            }

            // If we got this far, something failed; redisplay form
            viewModel.AvailableCourses = await _studentService.GetStudentCoursesAsync(0); // Get all courses
            return View(viewModel);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentOperationsViewModel viewModel)
        {
            if (id != viewModel.Student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _studentService.UpdateStudentAsync(viewModel.Student, viewModel.SelectedCourseIds);
                return RedirectToAction(nameof(Index));
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
