using CoreMVCTutorial.Models;
namespace CoreMVCTutorial.Interfaces
{
    public interface ITeacherRepository
    {
        //Step 1: Interface Creation
        List<Teacher> GetAllTeacher();
        Teacher GetTeacherById(int? id);
        void AddTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(int id);
        bool TeacherExists(int id);
        int GetNextTeacherId();

    }
}
