//using System;
//using Microsoft.Data.SqlClient;
//using System.Data;

//namespace DatabasePractice
//{
//    public class ADOdotNETPractice
//    {
//        public void PracticeProgram()
//        {
//            string connectionString = "Server=DESKTOP-3OT71VJ;Database=TeachersManagement;User Id=sa;Password=123;Encrypt=False;TrustServerCertificate=True";

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                try
//                {

//                    if (connection.State == ConnectionState.Open)
//                    {
//                        Console.WriteLine("Connection Established Successfully!");
//                    }
//                    else
//                    {
//                        Console.WriteLine("Connection Failed To Establish");
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"General Error: {ex.Message}");
//                }
//                bool loopController = true;
//                while (loopController)
//                {

//                    //Here Main Code starts
//                    Console.WriteLine("1. Test Connection");
//                    Console.WriteLine("2. Show Students Table");
//                    Console.WriteLine("3. Use DataReader");
//                    Console.WriteLine("4. Use StoredProcedure");
//                    Console.WriteLine("5. Use StoredProcedure With Parameter");
//                    Console.WriteLine("6. Use StoredProcedure For Insertion");
//                    Console.WriteLine("7. Use StoredProcedure For Deletion");

//                    Console.WriteLine("0. Close Application");
//                    Console.Write("Choose an option:   ");

//                    string choice = Console.ReadLine();

//                    switch (choice)
//                    {
//                        case "1":
//                            {
//                                connection.Open();

//                                if (connection.State == ConnectionState.Open)
//                                {
//                                    Console.WriteLine("Connection Established Successfully!");
//                                }
//                                else
//                                {
//                                    Console.WriteLine("Connection Failed To Establish");
//                                }
//                                Console.WriteLine("\n");
//                                connection.Close();

//                                break;
//                            }
//                        case "2": //Displaying Table
//                            {
//                                connection.Open();

//                                string query = "Select Id From Students";
//                                using (SqlCommand command = new SqlCommand(query, connection))
//                                {
//                                    var result = command.ExecuteScalar();
//                                    Console.WriteLine("Result: " + result);

//                                }
//                                Console.WriteLine("\n");
//                                connection.Close();

//                                break;
//                            }
//                        case "3": //Displaying Table with Condition
//                            {
//                                connection.Open();

//                                string query = "Select * From Students where Id > 7";
//                                using (SqlCommand command = new SqlCommand(query, connection))
//                                {
//                                    SqlDataReader reader = command.ExecuteReader();
//                                    while (reader.Read())
//                                    {
//                                        Console.WriteLine(reader["Id"] + "\t" + reader["Name"] + "\t" + reader["Email"] + "\t" + reader["Age"]);
//                                    }
//                                }
//                                Console.WriteLine("\n");
//                                connection.Close();

//                                break;
//                            }
//                        case "4":
//                            {
//                                connection.Open();

//                                using (SqlCommand command = new SqlCommand("FirstProcedure", connection) { CommandType = CommandType.StoredProcedure })
//                                {
//                                    SqlDataReader reader = command.ExecuteReader();
//                                    Console.WriteLine("Using StoredProcedure");
//                                    Console.WriteLine("Name" + "\t" + "Email" + "\t" + "Age");

//                                    while (reader.Read())
//                                    {
//                                        Console.WriteLine(reader["Name"] + "\t" + reader["Email"] + "\t" + reader["Age"]);
//                                    }
//                                }
//                                Console.WriteLine("\n");
//                                connection.Close();

//                                break;
//                            }

//                        case "5":
//                            {
//                                connection.Open();
//                                SqlParameter parameter = new SqlParameter("@Id", 5);
//                                using (SqlCommand command = new SqlCommand("GetByIdProcedure", connection) { CommandType = CommandType.StoredProcedure })
//                                {
//                                    command.Parameters.Add(parameter);
//                                    SqlDataReader reader = command.ExecuteReader();
//                                    Console.WriteLine("Using StoredProcedure");
//                                    Console.WriteLine("Name" + "\t" + "Email" + "\t" + "Age");

//                                    while (reader.Read())
//                                    {
//                                        Console.WriteLine(reader["Name"] + "\t" + reader["Email"] + "\t" + reader["Age"]);
//                                    }
//                                }
//                                Console.WriteLine("\n");
//                                connection.Close();

//                                break;
//                            }

//                        case "6": //Insertion
//                            {
//                                connection.Open();
//                                using (SqlCommand command = new SqlCommand("insertValues", connection) { CommandType = CommandType.StoredProcedure })
//                                {
//                                    command.Parameters.AddWithValue("@Name", "Mohsin");
//                                    command.Parameters.AddWithValue("@Email", "test@gmail.com");
//                                    command.Parameters.AddWithValue("@Age", 25);
//                                    command.Parameters.AddWithValue("@CreatedDate", "2000-02-02");
//                                    int rowsaffected = command.ExecuteNonQuery();
//                                    Console.WriteLine("RowsAffected: " + rowsaffected);

//                                }
//                                connection.Close();

//                                break;
//                            }
//                        case "7": //Deletion
//                            {
//                                connection.Open();
//                                using (SqlCommand command = new SqlCommand("DeleteRow", connection) { CommandType = CommandType.StoredProcedure })
//                                {
//                                    Console.Write("Enter Id to Be Deleted:  ");
//                                    int deleteId = Convert.ToInt32(Console.ReadLine());
//                                    command.Parameters.AddWithValue("@Id", deleteId);

//                                    int rowsaffected = command.ExecuteNonQuery();
//                                    Console.WriteLine("RowsAffected: " + rowsaffected);

//                                }
//                                connection.Close();

//                                break;
//                            }

//                        case "0": //Closing Application Table
//                            {
//                                loopController = false;
//                                break;
//                            }

//                    }




//                }





//            }
//        }
//    }
//}
