using CoreMVCTutorial.Models;
using CoreMVCTutorial.Interfaces;

namespace CoreMVCTutorial.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public List<Teacher> GetAllTeachers()
        {
            var teachers = _teacherRepository.GetAllTeacher();

            // Ensure HobbiesList and SkillsList are populated for each teacher
            foreach (var teacher in teachers)
            {
                teacher.HobbiesList = teacher.HobbiesList ?? new List<string>();
                teacher.SkillsList = teacher.SkillsList ?? new List<string>();
            }

            return teachers;
        }

        public Teacher GetTeacherById(int id)
        {
            var teacher = _teacherRepository.GetTeacherById(id);
            if (teacher != null)
            {
                teacher.HobbiesList = teacher.HobbiesList ?? new List<string>();
                teacher.SkillsList = teacher.SkillsList ?? new List<string>();
            }
            return teacher;
        }

        public void CreateTeacher(Teacher teacher)
        {
            // Convert lists to comma-separated strings for database storage
            teacher.Hobbies = teacher.HobbiesList != null ? string.Join(",", teacher.HobbiesList) : "";
            teacher.Skills = teacher.SkillsList != null ? string.Join(",", teacher.SkillsList) : "";

            // Validate email uniqueness
            if (_teacherRepository.EmailExists(teacher.Email))
            {
                throw new InvalidOperationException("Email already exists!");
            }

            _teacherRepository.AddTeacher(teacher);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            // Convert lists to comma-separated strings for database storage
            teacher.Hobbies = teacher.HobbiesList != null ? string.Join(",", teacher.HobbiesList) : "";
            teacher.Skills = teacher.SkillsList != null ? string.Join(",", teacher.SkillsList) : "";

            // Validate email uniqueness (excluding current teacher)
            if (_teacherRepository.EmailExists(teacher.Email, teacher.TeacherId))
            {
                throw new InvalidOperationException("Email already exists!");
            }

            _teacherRepository.UpdateTeacher(teacher);
        }

        public void DeleteTeacher(int id)
        {
            _teacherRepository.DeleteTeacher(id);
        }

        public dynamic GetDropdownData()
        {
            return new
            {
                Courses = new List<string> { "B.Tech", "MBA", "MCA", "B.Sc", "M.Tech" },
                Hobbies = new List<string> { "Reading", "Writing", "Sports", "Music", "Traveling", "Photography" },
                Skills = new List<string> { "C#", "ASP.NET", "SQL", "JavaScript", "Python", "Machine Learning", "Web Development" }
            };
        }
    }
}