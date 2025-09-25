using CoreMVCTutorial.Models;

namespace CoreMVCTutorial.Interfaces
{
    public interface ITeacherRepository
    {
        List<Teacher> GetAllTeacher();
        Teacher GetTeacherById(int? id);
        void AddTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(int id);
        bool TeacherExists(int id);
        bool EmailExists(string email, int? excludeTeacherId = null); // Add this method
        int GetNextTeacherId();
    }
}