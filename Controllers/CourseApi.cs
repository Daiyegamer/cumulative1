using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Cumulative1.Models;
using cumulative1.Models;

namespace Cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseAPIController : Controller
    {
        private readonly SchoolDbContext _context;

        public CourseAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route(template: "CourseList")]
        public List<Course> ListCourses()
        {
            List<Course> Courses = new List<Course>();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();

                // Updated SQL query to join the teachers table
                Command.CommandText = @"
                    SELECT 
                        courses.*, 
                        teachers.teacherfname, 
                        teachers.teacherlname 
                    FROM 
                        courses
                    LEFT JOIN 
                        teachers 
                    ON 
                        courses.teacherid = teachers.teacherid";

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        int CourseId = Convert.ToInt32(ResultSet["courseid"]);
                        string CourseCode = ResultSet["coursecode"].ToString();
                        int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        DateTime startdate = Convert.ToDateTime(ResultSet["startdate"]);
                        DateTime finishdate = Convert.ToDateTime(ResultSet["finishdate"]);
                        string CourseName = ResultSet["coursename"].ToString();
                        string TeacherFName = ResultSet["teacherfname"].ToString();
                        string TeacherLName = ResultSet["teacherlname"].ToString();

                        Course CurrentCourse = new Course()
                        {
                            CourseId = CourseId,
                            CourseCode = CourseCode,
                            TeacherId = TeacherId,
                            startdate = startdate,
                            finishdate = finishdate,
                            CourseName = CourseName,
                            TeacherFName = TeacherFName,
                            TeacherLName = TeacherLName
                        };
                        Courses.Add(CurrentCourse);
                    }
                }
            }

            return Courses;
        }

        [HttpGet]
        [Route(template: "FindCourse/{id}")]
        public Course FindCourse(int id)
        {
            Course SelectedCourse = new Course();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();

                // Updated SQL query to join the teachers table
                Command.CommandText = @"
                    SELECT 
                        courses.*, 
                        teachers.teacherfname, 
                        teachers.teacherlname 
                    FROM 
                        courses
                    LEFT JOIN 
                        teachers 
                    ON 
                        courses.teacherid = teachers.teacherid
                    WHERE 
                        courses.courseid = @id";
                Command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        int CourseId = Convert.ToInt32(ResultSet["courseid"]);
                        string CourseCode = ResultSet["coursecode"].ToString();
                        int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        DateTime startdate = Convert.ToDateTime(ResultSet["startdate"]);
                        DateTime finishdate = Convert.ToDateTime(ResultSet["finishdate"]);
                        string CourseName = ResultSet["coursename"].ToString();
                        string TeacherFName = ResultSet["teacherfname"].ToString();
                        string TeacherLName = ResultSet["teacherlname"].ToString();

                        SelectedCourse.CourseId = CourseId;
                        SelectedCourse.CourseCode = CourseCode;
                        SelectedCourse.TeacherId = TeacherId;
                        SelectedCourse.startdate = startdate;
                        SelectedCourse.finishdate = finishdate;
                        SelectedCourse.CourseName = CourseName;
                        SelectedCourse.TeacherFName = TeacherFName;
                        SelectedCourse.TeacherLName = TeacherLName;
                    }
                }
            }

            return SelectedCourse;
        }
    }
}