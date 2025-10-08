using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVC_Application.Migrations
{
    /// <inheritdoc />
    public partial class SeededDataAttemptThird : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseCode", "CourseName", "Description" },
                values: new object[,]
                {
                    { 1, "CS101", "Introduction to Programming", "Basic programming concepts and fundamentals" },
                    { 2, "CS201", "Database Management Systems", "Relational databases and SQL programming" },
                    { 3, "CS301", "Web Development", "Building web applications using modern technologies" },
                    { 4, "MATH101", "Mathematics for Computing", "Discrete mathematics and algorithms" },
                    { 5, "CS401", "Software Engineering", "Software development lifecycle and methodologies" },
                    { 6, "CS202", "Data Structures", "Advanced data structures and algorithms" },
                    { 7, "CS305", "Network Fundamentals", "Computer networks and communication protocols" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 7);
        }
    }
}
