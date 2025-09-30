using CoreMVCTutorial.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
namespace CoreMVCTutorial.Repositories

{
    public class StudentRepository
    {

        //Connected State
        //public bool AddStudent(Students student)
        //{
        //    using (var context = new StudentContext())
        //    {
        //        context.Database.EnsureCreated();

        //        var studentData = new Students() { Name = student.Name, Email = student.Email , Age = student.Age, CreatedDate = DateTime.Now};

        //        context.Students.Add(studentData);

        //        context.SaveChanges();


        //        int result = 1;
        //        return result > 0;

        //    }
        //}

        //Disconnected State
        public bool AddStudent(Students student)
        {
            var studentData = new Students
            {
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                CreatedDate = DateTime.Now,
                GradeId = student.GradeId   // <- include selected grade
            };
            using (var context = new StudentContext())
            {
                // EnsureCreated is usually only for simple dev scenarios; it's harmless but optional
                context.Database.EnsureCreated();

                context.Students.Add(studentData);
                int saved = context.SaveChanges();
                return saved > 0;
            }
        }


        //CodeFirst Approach
        public List<Students> GetAllStudents()
        {
            using (var context = new StudentContext())
            {
                var students = context.Students.ToList();

                return students;
            }
        }

        //public List<Students> GetAllStudents()
        //{
        //    using (var context = new StudentContext())
        //    {
        //        // Ensure database exists (creates if not)
        //        context.Database.EnsureCreated();

        //        // Fetch all students from the table
        //        var students = context.Students.ToList();

        //        return students;
        //    }
        //}

        public Students GetStudentById(int id)
        {
            Students student = new Students();
            using (var context = new StudentContext())
            {
                student = context.Students.FirstOrDefault(x => x.Id == id);
                return student;
            }
        }
        //Code First Approach Update
        // UPDATE - Update student using provided context
        public bool UpdateStudent(Students student, StudentContext context)
        {
            // Get fresh student from database using the SAME context
            var existingStudent = context.Students.FirstOrDefault(x => x.Id == student.Id);
            if (existingStudent != null)
            {
                // Update properties manually
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
                existingStudent.Age = student.Age;
                existingStudent.GradeId = student.GradeId;

                context.SaveChanges();
                return true;
            }
            return false;
        }
        //// UPDATE - Update student
        //public bool UpdateStudent(Students student)
        //{
        //    using (var context = new StudentContext())
        //    {
        //        context.Database.EnsureCreated();
        //        var studentUpdated = context.Students.FirstOrDefault(x => x.Id == student.Id);
        //        studentUpdated.Name = student.Name;
        //        studentUpdated.Email = student.Email;
        //        studentUpdated.Age = student.Age;

        //        context.SaveChanges();
        //        return true;
        //    }
        //}

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
