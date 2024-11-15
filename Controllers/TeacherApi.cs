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
        /// Returns a list of Teacher(s) in the system
        /// </summary>
        /// <example>
        /// retrieve Teacher list GET api/Teacher/ListTeachers
        /// </example>
        /// <returns>
        /// A list of Teacher(s)
        /// </returns>
        [HttpGet]
        [Route("TeacherList")]
        public List<Teacher> ListTeachers()
        {
            // Creating an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher>();

            // 'using' will close the connection after the code runs
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                // Create a new query for our DB
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "SELECT teachers.*, GROUP_CONCAT(courses.coursename SEPARATOR ', ') AS courses FROM teachers LEFT JOIN courses ON teachers.teacherid = courses.teacherid GROUP BY teachers.teacherid";

                // Gather results of queries into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    // Loop through each row from the results
                    while (ResultSet.Read())
                    {
                        // Retrieve column information by the DB column name as an index
                        int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        string TeacherFName = ResultSet["teacherfname"].ToString();
                        string TeacherLName = ResultSet["teacherlname"].ToString();
                        string EmployeeNumber = ResultSet["employeenumber"].ToString();
                        DateTime hiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                        double salary = Convert.ToDouble(ResultSet["salary"]);
                        string Courses = ResultSet["courses"].ToString();

                        // Create a new Teacher object and set properties
                        Teacher CurrentTeacher = new Teacher
                        {
                            TeacherId = TeacherId,
                            TeacherFName = TeacherFName,
                            TeacherLName = TeacherLName,
                            EmployeeNumber = EmployeeNumber,
                            hiredate = hiredate,
                            salary = salary,
                            Courses = Courses
                        };

                        Teachers.Add(CurrentTeacher);
                    }
                }
            }

            // Return the final list of teachers
            return Teachers;
        }

        /// <summary>
        /// Returns a Teacher from the DB by their ID
        /// </summary>
        /// <example>
        /// GET api/Teacher/FindTeacher/7
        /// </example>
        /// <returns>
        /// A matching Teacher by their ID. Empty result if teacher not found
        /// </returns>
        [HttpGet]
        [Route("FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            // Create an empty Teacher object
            Teacher SelectedTeacher = new Teacher();

            // 'using' will close the connection after the code runs
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                // Establish a new command (query) for our database
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "SELECT teachers.*, GROUP_CONCAT(courses.coursename SEPARATOR ', ') AS courses FROM teachers LEFT JOIN courses ON teachers.teacherid = courses.teacherid WHERE teachers.teacherid = @id GROUP BY teachers.teacherid";
                Command.Parameters.AddWithValue("@id", id);

                // Gather results of the query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    // Loop through each row of the result
                    if (ResultSet.Read()) // using if to get a single teacher based on ID
                    {
                        int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        string TeacherFName = ResultSet["teacherfname"].ToString();
                        string TeacherLName = ResultSet["teacherlname"].ToString();
                        string EmployeeNumber = ResultSet["employeenumber"].ToString();
                        DateTime hiredate = Convert.ToDateTime(ResultSet["hiredate"]);
                        double salary = Convert.ToDouble(ResultSet["salary"]);
                        string Courses = ResultSet["courses"].ToString();

                        // Set properties of the SelectedTeacher object
                        SelectedTeacher.TeacherId = TeacherId;
                        SelectedTeacher.TeacherFName = TeacherFName;
                        SelectedTeacher.TeacherLName = TeacherLName;
                        SelectedTeacher.EmployeeNumber = EmployeeNumber;
                        SelectedTeacher.hiredate = hiredate;
                        SelectedTeacher.salary = salary;
                        SelectedTeacher.Courses = Courses;
                    }
                }
            }

            // Return SelectedTeacher
            return SelectedTeacher;
        }
    }
}