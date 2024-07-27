using HW31DataBaseTest.Models;
using MySqlConnector;
using System.Data;

namespace HW31DataBaseTest.Methods
{
    public class DBMethods
    {
        private string? _connectionString;

        public DBMethods(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool IsDataBaseConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            using (connection)
            {
                connection.Open();
                return connection.State.Equals(ConnectionState.Open);
            }
        }

        public void CreateNewStudent(string name, int groupNumber)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO Users (Name, IdGroupList) VALUES ('{name}', '{groupNumber}')";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("Name", name);
                    command.Parameters.AddWithValue("IdGroupList", groupNumber);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void CreateNewGroup(string groupName, int groupNumber)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO GroupList (Name, IdGroupList) VALUES ('{groupNumber}', '{groupNumber}')";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("Name", groupNumber);
                    command.Parameters.AddWithValue("IdGroupList", groupNumber);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRow(string table, string attribute, string value)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"DELETE FROM {table} WHERE {attribute}='{value}';";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool IsUserDeleted(StudentModel student)
        {
            return student.Name == null && student.IdGroupList == 0;
        }

        public bool IsUserExist(StudentModel studentmodel)
        {
            var student = GetUserByAttribute("Name", studentmodel.Name);

            return studentmodel.Name == student.Name;
        }

        public int GetStudentId(StudentModel student)
        {
            var createdStudent = GetUserByAttribute("Name", student.Name);

            return createdStudent.IdUsers;
        }

        public StudentModel GetUserByAttribute(string attribute, string value)
        {
            var student = new StudentModel();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"SELECT Name, IdGroupList, idUsers FROM Users WHERE {attribute} = '{value}'";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student = new StudentModel
                            {
                                IdGroupList = reader.GetInt32("IdGroupList"),
                                Name = reader.GetString("Name"),
                                IdUsers = reader.GetInt32("idUsers")
                            };
                        }
                    }
                }
            }

            return student;
        }

        public List<StudentModel> GetStudents()
        {
            var students = new List<StudentModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Name, IdGroupList FROM Users";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var student = new StudentModel
                            {
                                IdGroupList = reader.GetInt32("IdGroupList"),
                                Name = reader.GetString("Name")
                            };
                            students.Add(student);
                        }
                    }
                }
            }
            foreach (var student in students)
            {
                Console.WriteLine(student.Name + ": " + student.IdGroupList + "\n");
            }
            return students;
        }


        public void UpdateUser(string changedAttribute, string newValue, string whereAttribute, string targetValue )
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"UPDATE Users SET {changedAttribute} = '{newValue}' WHERE {whereAttribute} = '{targetValue}'";

                using (var command = new MySqlCommand(query, connection))
                {   
                    command.Parameters.AddWithValue(changedAttribute, newValue);
                    command.Parameters.AddWithValue(whereAttribute, targetValue);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}