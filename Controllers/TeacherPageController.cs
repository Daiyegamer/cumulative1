using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly TeacherAPIController _api;
        //Use the API to get Teacher info
        //ideally would have an article service
        // where both the MVC and API can call the service
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
        //GET:// TeacherPage/New --> A webpage that prompts the user
        //to enter new Teacher info
        [HttpGet]
        public IActionResult New()
        {
            //direct to /Views/TeacherPage/New.cshtml
            return View();

        }
        //POST: TeacherPage/Create-->List Teachers page with the new Teacher added
        //Request body:
        //TeacherFName+TeacherLName = {employeenumber}{salary}{hiredate}
        [HttpPost]
        public IActionResult Create(Teacher NewTeacher)
        {
            // Call AddTeacher and retrieve the result as an integer
            int result = _api.AddTeacher(NewTeacher);

            // Handle errors based on the integer result
            // if  The first name or last name of the teacher is empty
            // Set an error message in TempData to display on the "New" view
            if (result == -1)
            {
                // if The first name or last name of the teacher is empty
                // Set an error message in TempData to display on the "New" view
                TempData["ErrorMessage"] = "Teacher first name and last name cannot be empty.";
                // Redirect the user back to the "New" form to correct the input
                return RedirectToAction("New");
            }
            else if (result == -2)
            {
                // if The hire date is in the future, which is invalid
                // Set an error message in TempData to inform the user of the invalid hire date
                TempData["ErrorMessage"] = "Hire date cannot be in the future.";
                // Redirect the user back to the "New" form to correct the input
                return RedirectToAction("New");
            }
            else if (result == -3)
            {
                // if The employee number is already taken by another teacher
                // Set an error message in TempData to notify the user of the conflict
                TempData["ErrorMessage"] = $"Employee number '{NewTeacher.EmployeeNumber}' is already taken by another teacher.";
                // Redirect the user back to the "New" form to use a different employee number
                return RedirectToAction("New");
            }
            else if (result <= 0)
            {
                // if Any other unexpected error occurred during the addition process
                // This typically happens if the insertion failed for unknown reasons
                // Set a generic error message in TempData
                TempData["ErrorMessage"] = "An unexpected error occurred while adding the teacher.";
                // Redirect the user back to the "New" form
                return RedirectToAction("New");
            }

            // if The teacher was added successfully
            // Set a success message in TempData with the ID of the newly added teacher
            TempData["SuccessMessage"] = $"Teacher added successfully with ID {result}.";
            // Redirect to the "Show" view, passing the ID of the newly created teacher as a route parameter
            return RedirectToAction("Show", new { id = result });
        } 
            /// <summary>
            /// GET /TeacherPage/DeleteConfirm/{id} --> A webpage Asking the user if
            /// they are sure they want to delete this teacher
            /// </summary>
            /// <returns></returns>
            [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            // Retrieve the teacher object from the API
            Teacher SelectedTeacher = _api.FindTeacher(id);

            if (SelectedTeacher == null)
            {
                // If the teacher does not exist, redirect to the list page with an error message
                TempData["ErrorMessage"] = "The teacher you are trying to delete does not exist.";
                return RedirectToAction("List");
            }

            // Pass the teacher to the DeleteConfirm view
            return View(SelectedTeacher);
        }
        // POST: TeacherPage/Delete/{id}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            int result = _api.DeleteTeacher(id);
             
            if (result <= 0)
            {
                TempData["ErrorMessage"] = "Failed to delete the teacher. The teacher might not exist.";
                return RedirectToAction("List");
            }

            TempData["SuccessMessage"] = "Teacher successfully deleted.";
            return RedirectToAction("List");
        }

    }
}