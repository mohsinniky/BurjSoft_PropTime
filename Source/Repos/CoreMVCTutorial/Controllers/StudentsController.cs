using CoreMVCTutorial.Models;
using CoreMVCTutorial.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            using (var context = new StudentContext())
            {
                ViewBag.GradeNames = context.Grades.ToDictionary(g => g.GradeID, g => g.GradeName);
            }
            return View(students);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            var context = new StudentContext();
            ViewBag.GradeId = new SelectList(context.Grades, "GradeID", "GradeName");
            return View();
        }


        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Students student)
        {

            try
            {
                using (var context = new StudentContext())
                {
                    context.Students.Add(student);
                    int result = context.SaveChanges();

                    if (result > 0)
                    {
                        TempData["SuccessMessage"] = "Student added successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error adding student.");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the student: " + ex.Message);
            }

            using (var context = new StudentContext())
            {
                ViewBag.GradeId = new SelectList(context.Grades.ToList(), "GradeID", "GradeName");
            }

            return View(student);
        }

        // GET: Student/Edit/5
        // GET: Student/Edit/5
        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
           
                using (var context = new StudentContext())
                {
                    var student = context.Students.FirstOrDefault(x => x.Id == id);
                    if (student == null)
                    {
                        return NotFound();
                    }

                    // Simple approach - just pass the list and selected value separately
                    ViewBag.AllGrades = context.Grades.ToList(); // Pass the entire list
                    ViewBag.SelectedGradeId = student.GradeId;   // Pass the selected value

                    return View(student);
                }
            
        }
        //Edit Post 
        [HttpPost]
        public ActionResult Edit(Students student)
        {
            using (var context = new StudentContext())
            {
                if (studentRepository.UpdateStudent(student, context))
                {
                    TempData["SuccessMessage"] = "Student updated successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error updating student.");
                }

                // Repopulate dropdown using the same context
                ViewBag.GradeId = new SelectList(context.Grades, "GradeID", "GradeName", student.GradeId);
            }


            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            var student = studentRepository.GetStudentById(id);
            if (student == null)
            {
                Console.WriteLine("Student Not Found");
            }

            // Get GradeName for display
            using (var context = new StudentContext())
            {
                var grade = context.Grades.FirstOrDefault(g => g.GradeID == student.GradeId);
                ViewBag.GradeName = grade?.GradeName ?? "No Grade Assigned";
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
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
