using CoreMVCTutorial.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
namespace CoreMVCTutorial.Repositories

{
    public class StudentRepository
    {

        //CodeFirst Approach
        public List<Students> GetAllStudents()
        {
            using (var context = new StudentContext())
            {
                var students = context.Students.ToList();

                return students;
            }
        }

        public Students GetStudentById(int id)
        {
            Students student = new Students();
            using (var context = new StudentContext())
            {
                student = context.Students.FirstOrDefault(x => x.Id == id);
                return student;
            }
        }
        // UPDATE - Update student using provided context
        public bool UpdateStudent(Students student, StudentContext context)
        {
            var existingStudent = context.Students.FirstOrDefault(x => x.Id == student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
                existingStudent.Age = student.Age;
                existingStudent.GradeId = student.GradeId;

                context.SaveChanges();
                return true;
            }
            return false;
        }

        // Delete
        public bool DeleteStudent(int id)
        {
            using (var context = new StudentContext())
            {
                context.Database.EnsureCreated();
                var studentDeleted = context.Students.FirstOrDefault(x => x.Id == id);
                context.Students.Remove(studentDeleted);

                context.SaveChanges();

                return true;
                
            }
        }
    }
}
