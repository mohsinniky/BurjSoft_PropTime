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


        [HttpPost]
        public ActionResult Create(Students student, string CountryName)
        {
            using (var context = new StudentContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        student.CreatedDate = DateTime.Now;
                        context.Students.Add(student);
                        context.SaveChanges(); // This generates the Student ID

                        if (!string.IsNullOrEmpty(CountryName))
                        {
                            var studentCountry = new StudentCountry
                            {
                                CountryName = CountryName,
                                StudentId = student.Id
                            };
                            context.StudentCountry.Add(studentCountry);
                            context.SaveChanges();
                        }

                        transaction.Commit();
                        TempData["SuccessMessage"] = "Student added successfully!";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError("", "Error adding student: " + ex.Message);
                        ViewBag.GradeId = new SelectList(context.Grades.ToList(), "GradeID", "GradeName");
                        return View(student);
                    }
                }
            }
        }

        // GET: Student/Edit/5
        public IActionResult Edit(int id)
        {
            using (var context = new StudentContext())
            {
                var student = context.Students
                    .Include(s => s.StudentCountry) // Include the country data
                    .FirstOrDefault(s => s.Id == id);

                if (student == null)
                {
                    return NotFound();
                }

                // Pass the country name to the view
                ViewBag.CountryName = student.StudentCountry?.CountryName;

                // Your existing grade dropdown setup
                ViewBag.AllGrades = context.Grades.ToList();
                ViewBag.SelectedGradeId = student.GradeId;

                return View(student);
            }
        }
        // POST: Student/Edit
        [HttpPost]
        public ActionResult Edit(Students student, string CountryName)
        {
            using (var context = new StudentContext())
            {
                if (studentRepository.UpdateStudent(student, CountryName, context))
                {
                    TempData["SuccessMessage"] = "Student updated successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error updating student.");
                }

                // Repopulate ViewBag data if update fails
                ViewBag.AllGrades = context.Grades.ToList();
                ViewBag.SelectedGradeId = student.GradeId;
                ViewBag.CountryName = CountryName;
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
