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

        public ActionResult Index()
        {
            var students = studentRepository.GetAllStudents();
            return View(students);
        }

 

        [HttpGet]
        public JsonResult GetAllStudents()
        {
            var students = studentRepository.GetAllStudents();
            return Json(students);
        }

        [HttpGet]
        public JsonResult GetStudent(int id)
        {
            var student = studentRepository.GetStudentById(id);
            return Json(student);
        }

        [HttpPost]
        public JsonResult SaveStudent([FromBody] Students student)
        {
            bool result;
            if (student.Id > 0)
                result = studentRepository.UpdateStudent(student);
            else
                result = studentRepository.AddStudent(student);

            return Json(new { success = result });
        }

        [HttpPost]
        public JsonResult DeleteStudent(int id)
        {
            var result = studentRepository.DeleteStudent(id);
            return Json(new { success = result });
        }




    }
}
