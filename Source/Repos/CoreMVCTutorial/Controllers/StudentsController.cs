using CoreMVCTutorial.Models;
using CoreMVCTutorial.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVCTutorial.Controllers
{
    public class StudentsController : Controller
    {

        private StudentRepository studentRepository;

        public StudentsController()
        {
            studentRepository = new StudentRepository();
        }

        // GET: Student
        public ActionResult Index()
        {
            var students = studentRepository.GetAllStudents();
            return View(students);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Students student)
        {
            if (ModelState.IsValid)
            {
                if (studentRepository.AddStudent(student))
                {
                    TempData["SuccessMessage"] = "Student added successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error adding student.");
                }
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            var student = studentRepository.GetStudentById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Students student)
        {
            if (ModelState.IsValid)
            {
                if (studentRepository.UpdateStudent(student))
                {
                    TempData["SuccessMessage"] = "Student updated successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error updating student.");
                }
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            var student = studentRepository.GetStudentById(id);
            if (student == null)
            {
                throw new NotImplementedException();
            }
            return View(student);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (studentRepository.DeleteStudent(id))
            {
                TempData["SuccessMessage"] = "Student deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Error deleting student.";
            }
            return RedirectToAction("Index");
        }




    }
}
