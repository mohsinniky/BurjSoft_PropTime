using CoreMVCTutorial.Models;
using CoreMVCTutorial.Interfaces;

namespace CoreMVCTutorial.Repositories
{
    //Step1: Here We inherit the interface And Defince The actions
    public class TeacherRepository : ITeacherRepository
    {
        public List<Teacher> _teachers;
        private int _nextId;

        public TeacherRepository()
        {
            _teachers = new List<Teacher>
            {
                new Teacher
                {
                    TeacherId = 1,
                    FullName = "Mohsin Raza",
                    FatherName = "Raza",
                    Email = "mohsin@example.com",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Phone = "03001234567",
                    Password = "StrongPass123",
                    Course = "B.Tech",
                    Gender = Gender.Male,
                    Address = "123 Street, Lahore",
                    TermsAndConditions = true,
                    Hobbies = new List<string> { "Reading", "Music" },
                    Skills = new List<string> { "C#", "SQL" }
                },
                new Teacher
                {
                    TeacherId = 2,
                    FullName = "Ali Ahmed",
                    FatherName = "Ahmed",
                    Email = "ali@example.com",
                    DateOfBirth = new DateTime(1988, 10, 20),
                    Phone = "03111234567",
                    Password = "Pass@123",
                    Course = "MBA",
                    Gender = Gender.Male,
                    Address = "456 Street, Karachi",
                    TermsAndConditions = true,
                    Hobbies = new List<string> { "Sports", "Photography" },
                    Skills = new List<string> { "Python", "Machine Learning" }
                }
            };

            _nextId = _teachers.Count > 0 ? (_teachers.Max(t => t.TeacherId) ?? 0) + 1 : 1;

        }

        public List<Teacher> GetAllTeacher()
        {
            return _teachers;
        }

        public Teacher GetTeacherById(int? id)
        {
            return _teachers.FirstOrDefault(x=> x.TeacherId == id);
        }

        public void AddTeacher(Teacher teacher)
        {
            teacher.TeacherId = _nextId++;
            _teachers.Add(teacher);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            var existingTeacher = GetTeacherById(teacher.TeacherId);
            if (existingTeacher != null)
            {
                existingTeacher.FullName = teacher.FullName;
                existingTeacher.FatherName = teacher.FatherName;
                existingTeacher.Email = teacher.Email;
                existingTeacher.DateOfBirth = teacher.DateOfBirth;
                existingTeacher.Phone = teacher.Phone;
                existingTeacher.Password = teacher.Password;
                existingTeacher.Course = teacher.Course;
                existingTeacher.Gender = teacher.Gender;
                existingTeacher.Address = teacher.Address;
                existingTeacher.TermsAndConditions = teacher.TermsAndConditions;
                existingTeacher.Hobbies = teacher.Hobbies;
                existingTeacher.Skills = teacher.Skills;
            }
        }

        public void DeleteTeacher(int id)
        {
            var teacher = GetTeacherById(id);
            if (teacher != null)
            {
                _teachers.Remove(teacher);
            }
        }

        public bool TeacherExists(int id)
        {
            return _teachers.Any(t => t.TeacherId == id);
        }

        public int GetNextTeacherId()
        {
            return _nextId  ;
        }

    }
}
