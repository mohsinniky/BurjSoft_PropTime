using CoreMVCTutorial.Interfaces;
using CoreMVCTutorial.Models;


namespace CoreMVCTutorial.Services
{
    public class TeacherService: ITeacherService
    {
        public ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public List<Teacher> GetAllTeachers()
        {
            return _teacherRepository.GetAllTeacher();
        }

        public Teacher GetTeacherById(int id)
        {
            return _teacherRepository.GetTeacherById(id);
        }

        public void CreateTeacher(Teacher teacher)
        {
            _teacherRepository.AddTeacher(teacher);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            _teacherRepository.UpdateTeacher(teacher);
        }

        public void DeleteTeacher(int id)
        {
            _teacherRepository.DeleteTeacher(id);
        }

        public (List<string> Courses, List<string> Hobbies, List<string> Skills) GetDropdownData()
        {
            var courses = new List<string> { "B.Tech", "M.Tech", "MBA", "BBA" };
            var hobbies = new List<string> { "Reading", "Traveling", "Music", "Sports", "Photography" };
            var skills = new List<string> { "C#", "Python", "SQL", "Machine Learning", "Physics", "Research", "Data Analysis" };

            return (courses, hobbies, skills);
        }

    }
}
