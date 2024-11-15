using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cumulative1.Controllers
{
    public class StudentPageController : Controller
    {
        private readonly StudentAPIController _api;
        //Use the API to get Student info
        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }
        // Retrieve students by name in list format GET : StudentPage/List
        public IActionResult List()
        {
            List<Student> Students = _api.ListStudents();
            return View(Students);
        }
        // extract students id GET :StudentPage/Show/{id} 
        public IActionResult Show(int id)
        {
            Student SelectedStudent = _api.FindStudent(id);
            return View(SelectedStudent);
        }

    }
}