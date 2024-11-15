namespace Cumulative1.Models
{
    public class Course
    {
        //Represents Course

        public int CourseId { get; set; }
        //Course Id
        public string? CourseCode { get; set; }
        //Course Code

        public int TeacherId { get; set; }
        //Teacher name
        
        public string? CourseName { get; set; }
        // Course Name
       
        public DateTime startdate { get; set; }
        //start date
        public DateTime finishdate { get; set; }
        // end date
        public string TeacherFName { get; set; }
        // Teacher First Name
        public string TeacherLName { get; set; }
        // Teacher Last Name
    }
}