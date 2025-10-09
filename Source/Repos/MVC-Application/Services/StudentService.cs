using Microsoft.EntityFrameworkCore;
using MVC_Application.DTOs;
using MVC_Application.Models;
using MVC_Application.Repository.Interfaces;
using MVC_Application.Services.Interfaces;

namespace MVC_Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public StudentService(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public async Task<StudentDto> UpsertStudentAsync(StudentUpsertDto studentUpsertDto)
        {
            Student student;

            if (studentUpsertDto.StudentId == 0)
            {
                // Create new student
                student = new Student
                {
                    FirstName = studentUpsertDto.FirstName,
                    LastName = studentUpsertDto.LastName,
                    Email = studentUpsertDto.Email,
                    PhoneNumber = studentUpsertDto.PhoneNumber,
                    GradeId = studentUpsertDto.GradeId
                };
                student = await _studentRepository.AddStudentAsync(student);

                // Create student address if provided
                if (studentUpsertDto.StudentAddress != null)
                {
                    var studentAddress = new StudentAddress
                    {
                        StudentId = student.StudentId,
                        City = studentUpsertDto.StudentAddress.City,
                        State = studentUpsertDto.StudentAddress.State
                    };
                    await _studentRepository.AddStudentAddressAsync(studentAddress);
                }
            }
            else
            {
                // Update existing student
                student = new Student
                {
                    StudentId = studentUpsertDto.StudentId,
                    FirstName = studentUpsertDto.FirstName,
                    LastName = studentUpsertDto.LastName,
                    Email = studentUpsertDto.Email,
                    PhoneNumber = studentUpsertDto.PhoneNumber,
                    GradeId = studentUpsertDto.GradeId
                };
                student = await _studentRepository.UpdateStudentAsync(student);

                // Update student address if provided
                if (studentUpsertDto.StudentAddress != null)
                {
                    var studentAddress = new StudentAddress
                    {
                        StudentAddressId = studentUpsertDto.StudentAddress.StudentAddressId,
                        StudentId = student.StudentId,
                        City = studentUpsertDto.StudentAddress.City,
                        State = studentUpsertDto.StudentAddress.State
                    };
                    await _studentRepository.UpdateStudentAddressAsync(studentAddress);
                }
            }

            // Handle course enrollment
            if (studentUpsertDto.SelectedCourseIds != null && studentUpsertDto.SelectedCourseIds.Any())
            {
                await _studentRepository.EnrollStudentInCoursesAsync(student.StudentId, studentUpsertDto.SelectedCourseIds);
            }

            // Return complete student with relationships
            var completeStudent = await _studentRepository.GetStudentByIdAsync(student.StudentId);
            var studentCourses = await _studentRepository.GetStudentCoursesAsync(student.StudentId);

            return new StudentDto
            {
                StudentId = completeStudent.StudentId,
                FirstName = completeStudent.FirstName,
                LastName = completeStudent.LastName,
                Email = completeStudent.Email,
                PhoneNumber = completeStudent.PhoneNumber,
                GradeId = completeStudent.GradeId,
                GradeName = completeStudent.Grade?.GradeName,
                StudentAddress = completeStudent.StudentAddress != null ? new StudentAddressDto
                {
                    StudentAddressId = completeStudent.StudentAddress.StudentAddressId,
                    City = completeStudent.StudentAddress.City,
                    State = completeStudent.StudentAddress.State,
                    StudentId = completeStudent.StudentAddress.StudentId
                } : null,
                CoursesDisplay = string.Join(", ", studentCourses
                    .Select(sc => sc.CourseDisplay)
                    .OrderBy(courseName => courseName))
            };
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null) return null;

            return new StudentDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber
            };
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentRepository.DeleteStudentAsync(id);
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return courses.Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                CourseCode = c.CourseCode,
                Description = c.Description
            }).ToList();
        }

        public async Task<List<CourseDto>> GetStudentCoursesAsync(int studentId)
        {
            var courses = await _studentRepository.GetStudentCoursesAsync(studentId);
            return courses.Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                CourseCode = c.CourseCode,
                Description = c.Description
            }).ToList();
        }

        public async Task<(List<StudentDto> Students, int TotalCount)> GetStudentsPageAsync(int page = 1, int pageSize = 10)
{
    var (students, totalCount) = await _studentRepository.GetStudentsPageAsync(page, pageSize);
    
    var studentDtos = students.Select(s => new StudentDto
    {
        StudentId = s.StudentId,
        FirstName = s.FirstName,
        LastName = s.LastName,
        Email = s.Email,
        PhoneNumber = s.PhoneNumber,
        GradeId = s.GradeId,
        GradeName = s.Grade?.GradeName,
        StudentAddress = s.StudentAddress != null ? new StudentAddressDto
        {
            StudentAddressId = s.StudentAddress.StudentAddressId,
            City = s.StudentAddress.City,
            State = s.StudentAddress.State,
            StudentId = s.StudentAddress.StudentId
        } : null,
        CoursesDisplay = string.Join(", ", s.StudentCourses
            .Select(sc => sc.Course.CourseDisplay)
            .OrderBy(courseName => courseName))
    }).ToList();

    return (studentDtos, totalCount);
}

        public async Task<List<GradeDto>> GetAllGradesAsync()
        {
            return await _studentRepository.GetAllGradesAsync();
        }




    }
}
