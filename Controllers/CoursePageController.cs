using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cumulative1.Controllers
{
    public class CoursePageController : Controller
    {
        private readonly CourseAPIController _api;
        //Use the API to get Teacher info
        public CoursePageController(CourseAPIController api)
        {
            _api = api;
        }
        // Retrieve teachers by name in list format GET : TeacherPage/List
        public IActionResult List()
        {
            List<Course> Courses = _api.ListCourses();
            return View(Courses);
        }
        // extract teacher id GET : TeacherPage/Show/{id} 
        public IActionResult Show(int id)
        {
            Course SelectedCourse = _api.FindCourse(id);
            return View(SelectedCourse);
        }

    }
}