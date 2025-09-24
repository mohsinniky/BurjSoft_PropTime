using System;
using Microsoft.Data.SqlClient;


namespace DatabasePractice
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().CreateTable();
            Console.ReadKey();
        }

        public void CreateTable()
        {
            SqlConnection con = null;
            try
            {
                // Creating Connection
                string connectionString = "Server=DESKTOP-3OT71VJ;  Database=TeachersManagement;  User Id=sa;  Password=123; Encrypt=False ";
                // Executing Connection
                con = new SqlConnection(connectionString);
                // Opening Connection  
                con.Open();


                // writing sql queries
                // TableCreation
                //SqlCommand cm = new SqlCommand("Create Table Teachers(Id int, Name Varchar(50), Department VarChar(50));", con);
                //Table Insertion
                //SqlCommand cm = new SqlCommand("INSERT INTO Teachers (Id, Name, Department)\r\nVALUES \r\n    (1, 'John Doe', 'Mathematics'),\r\n    (2, 'Jane Smith', 'Science');", con);
                // Executing the SQL query  
                //cm.ExecuteNonQuery();

                SqlCommand cm = new SqlCommand("Select * From Teachers;", con);
                SqlDataReader dataReader = cm.ExecuteReader();

                while (dataReader.Read()) 
                {
                    int id = dataReader.GetInt32(0);
                    string name = dataReader.GetString(1);
                    string department = dataReader.GetString(2);

                    Console.WriteLine($"{id} \t {name} \t {department}");
                }
                dataReader.Close();


                // Displaying the table  
                Console.WriteLine(dataReader);
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }
        }
    }
}