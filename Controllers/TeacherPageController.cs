using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly TeacherAPIController _api;
        //Use the API to get Teacher info
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }
        // Retrieve teachers by name in list format GET : TeacherPage/List
        public IActionResult List()
        {
            List<Teacher> Teachers = _api.ListTeachers();
            return View(Teachers);
        }
        // extract teacher id GET : TeacherPage/Show/{id} 
        public IActionResult Show(int id)
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);
            return View(SelectedTeacher);
        }

    }
}