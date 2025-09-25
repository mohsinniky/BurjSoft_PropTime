//using CoreMVCTutorial.Models;
//using CoreMVCTutorial.Interfaces;
//using Microsoft.Data.SqlClient;
//using System.Data;
//using CoreMVCTutorial.Services;

//namespace CoreMVCTutorial.Repositories
//{
//    public class TeacherRepository : ITeacherRepository
//    {
//        private readonly DatabaseHelper _dbHelper;

//        public TeacherRepository(DatabaseHelper dbHelper)
//        {
//            _dbHelper = dbHelper;
//        }

//        public List<Teacher> GetAllTeacher()
//        {
//            var teachers = new List<Teacher>();
//            var query = @"
//                SELECT TeacherId, FullName, FatherName, Email, DateOfBirth, 
//                       Phone, Password, Course, Gender, Address, 
//                       TermsAndConditions, Hobbies, Skills
//                FROM Teachers 
//                ORDER BY TeacherId";

//            _dbHelper.ExecuteReader(query, reader =>
//            {
//                teachers.Add(MapReaderToTeacher(reader));
//            });

//            return teachers;
//        }

//        public Teacher GetTeacherById(int? id)
//        {
//            if (id == null) return null;

//            var query = @"
//                SELECT TeacherId, FullName, FatherName, Email, DateOfBirth, 
//                       Phone, Password, Course, Gender, Address, 
//                       TermsAndConditions, Hobbies, Skills
//                FROM Teachers 
//                WHERE TeacherId = @TeacherId";

//            Teacher teacher = null;
//            var parameters = new[] { new SqlParameter("@TeacherId", id) };

//            _dbHelper.ExecuteReader(query, reader =>
//            {
//                teacher = MapReaderToTeacher(reader);
//            }, parameters);

//            return teacher;
//        }

//        public void AddTeacher(Teacher teacher)
//        {
//            var query = @"
//                INSERT INTO Teachers 
//                (FullName, FatherName, Email, DateOfBirth, Phone, Password, 
//                 Course, Gender, Address, TermsAndConditions, Hobbies, Skills)
//                VALUES 
//                (@FullName, @FatherName, @Email, @DateOfBirth, @Phone, @Password,
//                 @Course, @Gender, @Address, @TermsAndConditions, @Hobbies, @Skills);
//                SELECT SCOPE_IDENTITY();";

//            var parameters = CreateTeacherParameters(teacher);

//            var newId = _dbHelper.ExecuteScalar(query, parameters);
//            teacher.TeacherId = Convert.ToInt32(newId);
//        }

//        public void UpdateTeacher(Teacher teacher)
//        {
//            var query = @"
//                UPDATE Teachers 
//                SET FullName = @FullName, 
//                    FatherName = @FatherName, 
//                    Email = @Email, 
//                    DateOfBirth = @DateOfBirth, 
//                    Phone = @Phone, 
//                    Password = @Password,
//                    Course = @Course, 
//                    Gender = @Gender, 
//                    Address = @Address, 
//                    TermsAndConditions = @TermsAndConditions,
//                    Hobbies = @Hobbies, 
//                    Skills = @Skills
//                WHERE TeacherId = @TeacherId";

//            var parameters = CreateTeacherParameters(teacher);
//            // Add TeacherId parameter
//            var parameterList = parameters.ToList();
//            parameterList.Add(new SqlParameter("@TeacherId", teacher.TeacherId));

//            _dbHelper.ExecuteNonQuery(query, parameterList.ToArray());
//        }

//        public void DeleteTeacher(int id)
//        {
//            var query = "DELETE FROM Teachers WHERE TeacherId = @TeacherId";
//            var parameters = new[] { new SqlParameter("@TeacherId", id) };

//            _dbHelper.ExecuteNonQuery(query, parameters);
//        }

//        public bool TeacherExists(int id)
//        {
//            var query = "SELECT COUNT(1) FROM Teachers WHERE TeacherId = @TeacherId";
//            var parameters = new[] { new SqlParameter("@TeacherId", id) };

//            var result = _dbHelper.ExecuteScalar(query, parameters);
//            return Convert.ToInt32(result) > 0;
//        }

//        public bool EmailExists(string email, int? excludeTeacherId = null)
//        {
//            var query = "SELECT COUNT(1) FROM Teachers WHERE Email = @Email";
//            var parameters = new List<SqlParameter>
//            {
//                new SqlParameter("@Email", email)
//            };

//            if (excludeTeacherId.HasValue)
//            {
//                query += " AND TeacherId != @TeacherId";
//                parameters.Add(new SqlParameter("@TeacherId", excludeTeacherId.Value));
//            }

//            var result = _dbHelper.ExecuteScalar(query, parameters.ToArray());
//            return Convert.ToInt32(result) > 0;
//        }

//        public int GetNextTeacherId()
//        {
//            var query = "SELECT IDENT_CURRENT('Teachers') + IDENT_INCR('Teachers')";
//            var result = _dbHelper.ExecuteScalar(query);
//            return result != DBNull.Value ? Convert.ToInt32(result) : 1;
//        }

//        // Helper method to map SqlDataReader to Teacher object
//        private Teacher MapReaderToTeacher(SqlDataReader reader)
//        {
//            return new Teacher
//            {
//                TeacherId = reader["TeacherId"] as int?,
//                FullName = reader["FullName"].ToString(),
//                FatherName = reader["FatherName"].ToString(),
//                Email = reader["Email"].ToString(),
//                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
//                Phone = reader["Phone"] as string ?? string.Empty,
//                Password = reader["Password"].ToString(),
//                Course = reader["Course"] as string ?? string.Empty,
//                Gender = (Gender)Convert.ToInt32(reader["Gender"]),
//                Address = reader["Address"] as string ?? string.Empty,
//                TermsAndConditions = Convert.ToBoolean(reader["TermsAndConditions"]),
//                Hobbies = reader["Hobbies"] as string ?? string.Empty,
//                Skills = reader["Skills"] as string ?? string.Empty
//            };
//        }

//        // Helper method to create parameters for Teacher
//        private SqlParameter[] CreateTeacherParameters(Teacher teacher)
//        {
//            return new[]
//            {
//                new SqlParameter("@FullName", teacher.FullName),
//                new SqlParameter("@FatherName", teacher.FatherName),
//                new SqlParameter("@Email", teacher.Email),
//                new SqlParameter("@DateOfBirth", teacher.DateOfBirth),
//                new SqlParameter("@Phone", (object)teacher.Phone ?? DBNull.Value),
//                new SqlParameter("@Password", teacher.Password),
//                new SqlParameter("@Course", (object)teacher.Course ?? DBNull.Value),
//                new SqlParameter("@Gender", (int)teacher.Gender),
//                new SqlParameter("@Address", (object)teacher.Address ?? DBNull.Value),
//                new SqlParameter("@TermsAndConditions", teacher.TermsAndConditions),
//                new SqlParameter("@Hobbies", (object)teacher.Hobbies ?? DBNull.Value),
//                new SqlParameter("@Skills", (object)teacher.Skills ?? DBNull.Value)
//            };
//        }

//        // Additional method using DataTable approach (alternative implementation)
//        public List<Teacher> GetAllTeacherUsingDataTable()
//        {
//            var query = @"
//                SELECT TeacherId, FullName, FatherName, Email, DateOfBirth, 
//                       Phone, Password, Course, Gender, Address, 
//                       TermsAndConditions, Hobbies, Skills
//                FROM Teachers 
//                ORDER BY TeacherId";

//            var dataTable = _dbHelper.ExecuteQuery(query);
//            var teachers = new List<Teacher>();

//            foreach (DataRow row in dataTable.Rows)
//            {
//                teachers.Add(new Teacher
//                {
//                    TeacherId = row["TeacherId"] as int?,
//                    FullName = row["FullName"].ToString(),
//                    FatherName = row["FatherName"].ToString(),
//                    Email = row["Email"].ToString(),
//                    DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
//                    Phone = row["Phone"] as string ?? string.Empty,
//                    Password = row["Password"].ToString(),
//                    Course = row["Course"] as string ?? string.Empty,
//                    Gender = (Gender)Convert.ToInt32(row["Gender"]),
//                    Address = row["Address"] as string ?? string.Empty,
//                    TermsAndConditions = Convert.ToBoolean(row["TermsAndConditions"]),
//                    Hobbies = row["Hobbies"] as string ?? string.Empty,
//                    Skills = row["Skills"] as string ?? string.Empty
//                });
//            }

//            return teachers;
//        }
//    }
//}