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

    }
}
