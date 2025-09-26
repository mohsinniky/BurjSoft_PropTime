using CoreMVCTutorial.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
namespace CoreMVCTutorial.Repositories

{
    public class StudentRepository : DatabaseHelper
    {
        public bool AddStudent(Students student)
        {
            using (var context = new StudentContext())
            {
                context.Database.EnsureCreated();

                var studentData = new Students() { Name = student.Name, Email = student.Email , Age = student.Age, CreatedDate = DateTime.Now};

                context.Students.Add(studentData);

                context.SaveChanges();


                int result = 1;
                return result > 0;
                
            }
        }

        public List<Students> GetAllStudents()
        {
            using (var context = new StudentContext())
            {
                // Ensure database exists (creates if not)
                context.Database.EnsureCreated();

                // Fetch all students from the table
                var students = context.Students.ToList();

                return students;
            }
        }



        //public void ChangesMethod()
        //{
        //    var studentsTable = GetAllStudents();

        //    studentsTable.Rows[0]["Name"] = "testing";
        //    studentsTable.Rows[1]["Age"] = 1111;

        //    UpdateMultipleStudents(studentsTable);

        //}

        public void UpdateMultipleStudents(DataTable changedTable)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "Select * From Students";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    adapter.Update(changedTable);
                }
            }
        }



        //public DataTable GetAllStudents()
        //{
        //    DataTable students = new DataTable();
        //    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //    {
        //        string query = "SELECT * FROM Students ORDER BY CreatedDate DESC";

        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                students.Load(reader);
        //            }

        //        }
        //    }
        //    return students;
        //}

        public Students GetStudentById(int id)
        {
            Students student = new Students();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Students where Id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Students
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                            };
                        }
                    }

                }
            }
            return null;
        }

        // UPDATE - Update student
        public bool UpdateStudent(Students student)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "UPDATE Students SET Name = @Name, Email = @Email, Age = @Age WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@Id", student.Id);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        // Delete
        public bool DeleteStudent(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "DELETE FROM Students WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}
