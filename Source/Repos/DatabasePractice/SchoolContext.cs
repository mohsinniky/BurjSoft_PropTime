using Microsoft.EntityFrameworkCore;
using System;


namespace DatabasePractice
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-3OT71VJ;Database=SchoolDb;User Id=sa;Password=123;TrustServerCertificate=true;Encrypt=false;"
            );

        }





    }
}
