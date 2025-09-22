using CoreMVCTutorial.Models;
namespace CoreMVCTutorial.Interfaces
{
    public interface ITeacherService
    {
        //Step1: Service Interface
        List<Teacher> GetAllTeachers();
        Teacher GetTeacherById(int id);
        void CreateTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(int id);
        (List<string> Courses, List<string> Hobbies, List<string> Skills) GetDropdownData();
    }
}
