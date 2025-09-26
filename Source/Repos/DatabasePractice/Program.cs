using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DatabasePractice
{

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                context.Database.EnsureCreated();

                var grade1 = new Grade(){ GradeName = "Grade 1st" };
                var student1 = new Student(){ FirstName = "Mohsin", LastName = "Raza", Grade = grade1 };

                context.Students.Add(student1);
                context.Students.Add(student1);
                context.Students.Add(student1);

                context.SaveChanges();
                foreach (var std in context.Students)
                {
                    Console.WriteLine($"First Name: {std.FirstName}, Last Name: {std.LastName}");
                }
            } 
        }
    }
}