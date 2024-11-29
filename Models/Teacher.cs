namespace Cumulative1.Models
{
    public class Teacher
    {
        //Represents Teacher

        //Teacher Id
        public int TeacherId { get; set; }
        //Teacher first name
        public string? TeacherFName { get; set; }
        //Teacher last name
        public string? TeacherLName { get; set; }
        // Teacher employee number
        public string? EmployeeNumber { get; set; }
        // Teacher date of hire
        public DateTime hiredate { get; set; }
        //Teacher salary
        public double salary { get; set; } 
        
        
    }
}