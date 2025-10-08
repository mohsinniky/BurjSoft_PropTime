using MVC_Application.Models;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace MVC_Application
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-3OT71VJ;Database=SchoolManagement;User Id=sa;Password=123;TrustServerCertificate=true;Encrypt=false;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    CourseId = 1,
                    CourseName = "Introduction to Programming",
                    CourseCode = "CS101",
                    Description = "Basic programming concepts and fundamentals"
                },
                new Course
                {
                    CourseId = 2,
                    CourseName = "Database Management Systems",
                    CourseCode = "CS201",
                    Description = "Relational databases and SQL programming"
                },
                new Course
                {
                    CourseId = 3,
                    CourseName = "Web Development",
                    CourseCode = "CS301",
                    Description = "Building web applications using modern technologies"
                },
                new Course
                {
                    CourseId = 4,
                    CourseName = "Mathematics for Computing",
                    CourseCode = "MATH101",
                    Description = "Discrete mathematics and algorithms"
                },
                new Course
                {
                    CourseId = 5,
                    CourseName = "Software Engineering",
                    CourseCode = "CS401",
                    Description = "Software development lifecycle and methodologies"
                },
                new Course
                {
                    CourseId = 6,
                    CourseName = "Data Structures",
                    CourseCode = "CS202",
                    Description = "Advanced data structures and algorithms"
                },
                new Course
                {
                    CourseId = 7,
                    CourseName = "Network Fundamentals",
                    CourseCode = "CS305",
                    Description = "Computer networks and communication protocols"
                }
             );
        }

    }
}
