using CoreMVCTutorial.Models;

namespace CoreMVCTutorial.Interfaces
{
    public interface ITeacherService
    {
        List<Teacher> GetAllTeachers();
        Teacher GetTeacherById(int id);
        void CreateTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(int id);
        dynamic GetDropdownData();
    }
}