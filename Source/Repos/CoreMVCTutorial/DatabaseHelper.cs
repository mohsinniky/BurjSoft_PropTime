using Microsoft.Extensions.Configuration;
using System.IO;
namespace CoreMVCTutorial
{

    public class DatabaseHelper
    {
        public static string ConnectionString { get; private set; }

        static DatabaseHelper()
        {
            // Build configuration object and load appsettings.Development.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();

            // Read connection string
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }





        /* <-------------------------------> */
        //private readonly IConfiguration _configuration;
        //private readonly string _connectionString;

        //public DatabaseHelper(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    _connectionString = _configuration.GetConnectionString("DefaultConnection");
        //}

        //// Method to create and open connection
        //public SqlConnection CreateConnection()
        //{
        //    var connection = new SqlConnection(_connectionString);
        //    connection.Open();
        //    return connection;
        //}

        //// Execute query and return DataTable
        //public DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        //{
        //    using (var connection = CreateConnection())
        //    using (var command = new SqlCommand(query, connection))
        //    {
        //        if (parameters != null)
        //        {
        //            command.Parameters.AddRange(parameters);
        //        }

        //        var dataTable = new DataTable();
        //        using (var adapter = new SqlDataAdapter(command))
        //        {
        //            adapter.Fill(dataTable);
        //        }
        //        return dataTable;
        //    }
        //}

        //// Execute non-query (INSERT, UPDATE, DELETE)
        //public int ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        //{
        //    using (var connection = CreateConnection())
        //    using (var command = new SqlCommand(commandText, connection))
        //    {
        //        if (parameters != null)
        //        {
        //            command.Parameters.AddRange(parameters);
        //        }
        //        return command.ExecuteNonQuery();
        //    }
        //}

        //// Execute scalar query
        //public object ExecuteScalar(string commandText, params SqlParameter[] parameters)
        //{
        //    using (var connection = CreateConnection())
        //    using (var command = new SqlCommand(commandText, connection))
        //    {
        //        if (parameters != null)
        //        {
        //            command.Parameters.AddRange(parameters);
        //        }
        //        return command.ExecuteScalar();
        //    }
        //}

        //// New method: Execute reader with action delegate for better resource management
        //public void ExecuteReader(string query, Action<SqlDataReader> action, params SqlParameter[] parameters)
        //{
        //    using (var connection = CreateConnection())
        //    using (var command = new SqlCommand(query, connection))
        //    {
        //        if (parameters != null)
        //        {
        //            command.Parameters.AddRange(parameters);
        //        }

        //        using (var reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                action(reader);
        //            }
        //        }
        //    }
        //}
    }
}