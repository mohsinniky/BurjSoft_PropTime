
using Microsoft.EntityFrameworkCore;
using MVC_Application.DTOs;
using MVC_Application.Models;
using MVC_Application.Repository.Interfaces;
using System.Linq;
namespace MVC_Application.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students.Include(s=> s.StudentAddress).Include(s=>s.Grade).FirstOrDefaultAsync(s => s.StudentId == id);
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            // Above we are Explicitly telling EF to treat the above Entity as Modified regardless of tracking States

            //// AsTracking
            //var studentTracked = _context.Students.AsTracking().First(st=> st.StudentId.Equals(student.StudentId));
            //studentTracked.FirstName = student.FirstName;
            //studentTracked.LastName = student.LastName;
            //studentTracked.Email = student.Email;
            //studentTracked.PhoneNumber = student.PhoneNumber;

            //// AsNotTracking
            //var studentNotTracked = _context.Students.AsNoTracking().First(st => st.StudentId.Equals(student.StudentId));
            //studentNotTracked.FirstName = student.FirstName;
            //studentNotTracked.LastName = student.LastName;
            //studentNotTracked.Email = student.Email;
            //studentNotTracked.PhoneNumber = student.PhoneNumber;
            //_context.Students.Attach(studentNotTracked);
            //_context.Entry(studentNotTracked).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EnrollStudentInCoursesAsync(int studentId, List<int> courseIds)
        {
            var currentEnrollments = await _context.StudentCourse
                .Where(sc => sc.StudentId == studentId)
                .ToListAsync();

            var currentCourseIds = currentEnrollments.Select(sc => sc.CourseId);
            var newCourseIds = courseIds ?? new List<int>();

            var toRemove = currentEnrollments
                .Where(sc => !newCourseIds.Contains(sc.CourseId));

            var toAdd = newCourseIds
                .Except(currentCourseIds)
                .Select(courseId => new StudentCourse
                {
                    StudentId = studentId,
                    CourseId = courseId
                });

            _context.StudentCourse.RemoveRange(toRemove);
            _context.StudentCourse.AddRange(toAdd);

            // Practice Code

            var intersectionLINQ = currentCourseIds.Intersect(newCourseIds).ToList();
            var unionLINQ = currentCourseIds.Union(newCourseIds).ToList();

            var distinctNames = _context.Students
                .Select(s => s.FirstName).Distinct().ToList();
            var countCourses = _context.Courses.Count();
            var averageId = _context.Students.Average(x => x.StudentId);

            var otherColumnSearchLINQ = _context.Students.Where(s => s.FirstName == "Mohsin");
            var findLINQ = _context.Students.Find(1017);
            var disOrderedStudents = _context.Students.OrderByDescending(s => s.FirstName);
            var orderedStudents = _context.Students.OrderBy(s => s.FirstName);
            var includedStudents = _context.Students.OrderBy(s => s.FirstName).Include(s => s.StudentCourses).ThenInclude(sc => sc.Course);
            var joinStudents = _context.Students
                .Join(_context.StudentCourse,
                s => s.StudentId,
                sc => sc.StudentId,
                (s, sc) => new { s.FirstName, sc.CourseId }
                );


            var tt = (
                      //from st in _context.Students
                      //join sc in _context.StudentCourse on
                      //st.StudentId == sc.StudentId
                      //select

                      //from st in _context.Students
                      //select st.FirstName join 
                      //from sc in _context.StudentCourse select sc.CourseId

                      from st in _context.Students
                      join sc in _context.StudentCourse on st.StudentId equals sc.StudentId
                      group sc by st.FirstName into g

                      select new
                      {
                          FirstName = g.Key,
                          CourseIds = g.Select(x => x.CourseId).ToList(),
                          CourseName = g.Select(x => x.Course).ToList()
                      }



                      ).ToList();


            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Course>> GetStudentCoursesAsync(int studentId)
        {
            //return await _context.StudentCourse
            //    .Where(sc => sc.StudentId == studentId)
            //    .Include(sc => sc.Course)
            //    .Select(sc => sc.Course)
            //    .ToListAsync();

            return await (from sc in _context.StudentCourse
                          where sc.StudentId == studentId
                          select sc.Course).ToListAsync();

        }

        public async Task<(List<Student> Students, int TotalCount)> GetStudentsPageAsync(int page, int pageSize)
        {
            var totalCount = await _context.Students.CountAsync();

            var students = await _context.Students
                .AsNoTracking()
                .Include(s=> s.StudentAddress)
                .Include(s=> s.Grade)
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .OrderBy(s => s.StudentId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (students, totalCount);
        }

        public async Task<List<GradeDto>> GetAllGradesAsync()
        {
            var grades = await _context.Grades.ToListAsync();
            return grades.Select(g => new GradeDto
            {
                GradeId = g.GradeId,
                GradeName = g.GradeName
            }).ToList();
        }


        // Address related methods
        public async Task<StudentAddress> AddStudentAddressAsync(StudentAddress studentAddress)
        {
            _context.StudentAddresses.Add(studentAddress);
            await _context.SaveChangesAsync();
            return studentAddress;
        }

        public async Task<StudentAddress> UpdateStudentAddressAsync(StudentAddress studentAddress)
        {
            try
            {
                _context.StudentAddresses.Update(studentAddress);
                await _context.SaveChangesAsync();
                return studentAddress;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateStudentAddressAsync: {ex.Message}");
                throw;
            }
        }
        public async Task<StudentAddress> GetStudentAddressAsync(int studentId)
        {
            return await _context.StudentAddresses
        .AsNoTracking()
        .FirstOrDefaultAsync(sa => sa.StudentId == studentId);
        }

    }
}
