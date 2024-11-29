using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Cumulative1.Models;
using cumulative1.Models;
using System.Collections.Generic;

namespace Cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIController : Controller
    {
        // Dependency injection of database context
        private readonly SchoolDbContext _context;

        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all teachers in the system.
        /// </summary>
        /// <example>
        /// GET api/TeacherAPI/TeacherList
        /// </example>
        /// <returns>A list of Teacher objects</returns>
        [HttpGet]
        [Route("TeacherList")]
        public List<Teacher> ListTeachers()
        {
            // Initialize an empty list to store teacher records
            List<Teacher> Teachers = new List<Teacher>();

            // Open the database connection using the injected context
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                // Define the SQL query to retrieve all teachers
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "SELECT * FROM teachers";

                // Execute the query and process the results
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        // Map the database columns to a Teacher object
                        Teacher CurrentTeacher = new Teacher
                        {
                            TeacherId = Convert.ToInt32(ResultSet["teacherid"]),
                            TeacherFName = ResultSet["teacherfname"].ToString(),
                            TeacherLName = ResultSet["teacherlname"].ToString(),
                            EmployeeNumber = ResultSet["employeenumber"].ToString(),
                            hiredate = Convert.ToDateTime(ResultSet["hiredate"]),
                            salary = Convert.ToDouble(ResultSet["salary"])
                        };

                        // Add the teacher to the list
                        Teachers.Add(CurrentTeacher);
                    }
                }
            }

            // Return the list of teachers
            return Teachers;
        }

        /// <summary>
        /// Finds a teacher by their ID.
        /// </summary>
        /// <example>
        /// GET api/TeacherAPI/FindTeacher/7
        /// </example>
        /// <param name="id">The ID of the teacher</param>
        /// <returns>The matching Teacher object or null if not found</returns>
        [HttpGet]
        [Route("FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            // Initialize a Teacher object to store the result
            Teacher SelectedTeacher = null;

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                // Define the SQL query to find the teacher by ID
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "SELECT * FROM teachers WHERE teacherid = @id";
                Command.Parameters.AddWithValue("@id", id);

                // Execute the query and process the result
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    if (ResultSet.Read())
                    {
                        SelectedTeacher = new Teacher
                        {
                            TeacherId = Convert.ToInt32(ResultSet["teacherid"]),
                            TeacherFName = ResultSet["teacherfname"].ToString(),
                            TeacherLName = ResultSet["teacherlname"].ToString(),
                            EmployeeNumber = ResultSet["employeenumber"].ToString(),
                            hiredate = Convert.ToDateTime(ResultSet["hiredate"]),
                            salary = Convert.ToDouble(ResultSet["salary"])
                        };
                    }
                }
            }

            // Return the teacher or null if not found
            return SelectedTeacher;
        }

        /// <summary>
        /// Adds a new teacher to the database.
        /// </summary>
        /// <example>
        /// POST api/TeacherAPI/AddTeacher
        /// Content-Type: application/json
        /// {
        ///     "TeacherFName": "John",
        ///     "TeacherLName": "Doe",
        ///     "EmployeeNumber": "T123",
        ///     "hiredate": "2024-11-23T00:00:00",
        ///     "salary": 50000.0
        /// }
        /// </example>
        /// <param name="NewTeacher">The teacher object to add</param>
        /// <returns>The ID of the newly added teacher</returns>
        [HttpPost(template: "AddTeacher")]
        public int AddTeacher([FromBody] Teacher NewTeacher)
        {
            // Validate Teacher Name
            if (string.IsNullOrWhiteSpace(NewTeacher.TeacherFName) || string.IsNullOrWhiteSpace(NewTeacher.TeacherLName))
            {
                return -1; // Error code for empty first or last name
            }

            // Validate Hire Date
            if (NewTeacher.hiredate > DateTime.Now)
            {
                return -2; // Error code for future hire date
            }

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                // Check for duplicate Employee Number
                string checkQuery = "SELECT COUNT(*) FROM teachers WHERE employeenumber = @employeenum";
                MySqlCommand checkCommand = Connection.CreateCommand();
                checkCommand.CommandText = checkQuery;
                checkCommand.Parameters.AddWithValue("@employeenum", NewTeacher.EmployeeNumber);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count > 0)
                {
                    return -3; // Error code for duplicate employee number
                }

                // Insert the teacher into the database
                string insertQuery = @"
            INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary)
            VALUES (@firstname, @lastname, @employeenum, @date, @salary)";
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = insertQuery;

                // Add parameters
                Command.Parameters.AddWithValue("@firstname", NewTeacher.TeacherFName);
                Command.Parameters.AddWithValue("@lastname", NewTeacher.TeacherLName);
                Command.Parameters.AddWithValue("@employeenum", NewTeacher.EmployeeNumber);
                Command.Parameters.AddWithValue("@date", NewTeacher.hiredate);
                Command.Parameters.AddWithValue("@salary", NewTeacher.salary);

                // Execute the insert query
                Command.ExecuteNonQuery();

                // Retrieve the last inserted ID
                return Convert.ToInt32(Command.LastInsertedId);
            }
        }
        /// <summary>
        /// Deletes a teacher by their ID.
        /// </summary>
        /// <example>
        /// DELETE api/TeacherAPI/DeleteTeacher/13
        /// </example>
        /// <param name="TeacherId">The ID of the teacher to delete</param>
        /// <returns>The number of rows affected</returns>
        [HttpDelete(template: "DeleteTeacher/{TeacherId}")]
        public int DeleteTeacher(int TeacherId)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                // Define the SQL query to delete the teacher
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "DELETE FROM teachers WHERE teacherid = @id";
                Command.Parameters.AddWithValue("@id", TeacherId);

                // Execute the query and return the number of rows affected
                return Command.ExecuteNonQuery();
            }
        }
    }
}