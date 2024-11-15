using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Cumulative1.Models;
using cumulative1.Models;

namespace Cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : Controller
    {
        // dependency injection of database context
        private readonly SchoolDbContext _context;

        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Returns a list of Student(s) in the system
        /// </summary>
        /// <example>
        /// retrieve Student list GET api/Student/Liststudent -> 
        /// </example>
        /// <returns>
        /// A list of Student(s)
        /// </returns>


        [HttpGet]
        [Route(template: "StudentList")]

        public List<Student> ListStudents()
        {
            // Creating an empty list of Students
            List<Student> Students = new List<Student>();
            // 'using' will close the connection after the code runs
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                //create a new query for our DB
                MySqlCommand Command = Connection.CreateCommand();

                //SQL QUERY
                Command.CommandText = "select * from students";
                // Gather results of queries into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop through each row from the results
                    while (ResultSet.Read())
                    {
                        //retrieve column information by the DB column name as an index
                        int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                        string StudentFName = ResultSet["studentfname"].ToString();
                        string StudentLName = ResultSet["studentlname"].ToString();
                        string StudentNumber = ResultSet["studentnumber"].ToString();
                        DateTime enroldate = Convert.ToDateTime(ResultSet["enroldate"]);
                       

                        //short cut  to set all properties while creating an object
                        Student CurrentStudent = new Student()
                        {
                            StudentId = StudentId,
                            StudentFName = StudentFName,
                            StudentLName = StudentLName,
                            StudentNumber = StudentNumber,
                            enroldate = enroldate
                            
                        };

                        Students.Add(CurrentStudent);

                    }
                }
            }


            //Return the final list of Students
            return Students;


        }
        /// <summary>
        /// Returns a Student from the DB by their ID
        /// </summary>
        /// <example>
        /// GET api/Sudent/FindStudent/3 -> 
        /// </example>
        /// <returns>
        /// A matching Student  by their ID. Empty results if Student not found
        /// </returns>

        [HttpGet]
        [Route(template: "FindStudent/{id}")]
        //Create empty Student
        public Student FindStudent(int id)
        {

            Student SelectedStudent = new Student();
            // 'using' will close the connection after the code runs
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                //Establish a new command (query) for our database
                MySqlCommand Command = Connection.CreateCommand();
                // @id is replaced with a id
                Command.CommandText = "select * from students where studentid=@id";
                Command.Parameters.AddWithValue("@id", id);
                // Gather results of the query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row of the result
                    while (ResultSet.Read())
                    {
                        int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                        string StudentFName = ResultSet["studentfname"].ToString();
                        string StudentLName = ResultSet["studentlname"].ToString();
                        string StudentNumber = ResultSet["studentnumber"].ToString();
                        DateTime enroldate = Convert.ToDateTime(ResultSet["enroldate"]);


                        SelectedStudent.StudentId = StudentId;
                        SelectedStudent.StudentFName = StudentFName;
                        SelectedStudent.StudentLName = StudentLName;
                        SelectedStudent.StudentNumber = StudentNumber;
                        SelectedStudent.enroldate = enroldate;
                        

                    }
                }
            }


            //Return SelectedStudent
            return SelectedStudent;
        }

    }
}

