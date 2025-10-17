using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MVC_Application.Models;

namespace MVC_Application.Controllers
{
    public class DappersController : Controller
    {
        private readonly SqlConnection connection;
        public DappersController()
        {
            string connectionString = "Server=DESKTOP-3OT71VJ;Database=SchoolManagement;User Id=sa;Password=123;TrustServerCertificate=true;Encrypt=false;";
            connection = new SqlConnection(connectionString);
        }
        public IActionResult Index()
        {
            //Simple Query
            string sqlQuery = "SELECT * FROM Courses";
            var courses = connection.Query<Course>(sqlQuery).ToList();

            //// Parameterized Query
            //string sqlQuery2 = "SELECT * FROM Courses where CourseId = @param OR CourseId = @param2";
            //var coursesNo1 = connection.Query<Course>(sqlQuery2, new { param = 1, param2 = 2 }).ToList();

            var coursesByProcedure = connection.Query<Course>(
                "GetAllCourses",
                commandType: System.Data.CommandType.StoredProcedure
            ).ToList();


            return View();
        }
    }
}
