using CoreMVCTutorial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace CoreMVCTutorial
{
    public class StudentContext : DbContext
    {
        public DbSet<Students> Students { get; set; }
        public DbSet<StudentCountry> StudentCountry { get; set; }
        public DbSet<Grade> Grades { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-3OT71VJ;Database=StudentsManagement;User Id=sa;Password=123;TrustServerCertificate=true;Encrypt=false;"
            );
        }

    }
}
