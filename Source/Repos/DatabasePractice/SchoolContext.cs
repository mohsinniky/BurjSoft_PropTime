using Microsoft.EntityFrameworkCore;
using System;


namespace DatabasePractice
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }





    }
}
