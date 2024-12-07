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
        /// <summary>
        /// Displays the Edit page for a specific teacher.
        /// </summary>
        /// <param name="id">The unique identifier of the teacher to edit.</param>
        /// <returns>
        /// The "Edit" view with the teacher's details if the teacher exists. 
        /// If the teacher does not exist, redirects to the "List" page with an error message.
        /// </returns>
        /// <example>
        /// Example URL: /TeacherPage/Edit/10
        /// If the teacher with ID 10 exists, the method fetches their details and returns the "Edit" view.
        /// If not, the method redirects to the "List" page with the error message:
        /// "The teacher you are trying to edit does not exist."
        /// </example>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);

            if (SelectedTeacher == null)
            {
                TempData["ErrorMessage"] = "The teacher you are trying to edit does not exist.";
                return RedirectToAction("List");
            }

            return View(SelectedTeacher);
        }

        /// <summary>
        /// Updates the details of a specific teacher.
        /// </summary>
        /// <param name="id">The unique identifier of the teacher to update.</param>
        /// <param name="TeacherFName">The first name of the teacher (required).</param>
        /// <param name="TeacherLName">The last name of the teacher (required).</param>
        /// <param name="EmployeeNumber">The unique employee number of the teacher (required).</param>
        /// <param name="hiredate">The hire date of the teacher (cannot be in the future).</param>
        /// <param name="salary">The salary of the teacher (must be zero or greater).</param>
        /// <returns>
        /// Redirects to the "Show" page if the update is successful, or back to the "Edit" page with error messages if validation fails or an error occurs.
        /// </returns>
        /// <example>
        /// Example Input:
        /// id = 10
        /// TeacherFName = "John"
        /// TeacherLName = "Doe"
        /// EmployeeNumber = "EMP123"
        /// hiredate = "2023-01-01"
        /// salary = 45000.00
        ///
        /// Workflow:
        /// 1. Validates input parameters.
        /// 2. Calls `_api.UpdateTeacher` to update the teacher in the database.
        /// 3. If successful, redirects to `/TeacherPage/Show/10` with the success message:
        ///    "Teacher updated successfully."
        /// 4. If validation fails, redirects back to `/TeacherPage/Edit/10` with appropriate error messages.
        /// </example>
        [HttpPost]
        public IActionResult Update(int id, string TeacherFName, string TeacherLName, string EmployeeNumber, DateTime hiredate, double salary)
        {
            // Input Validation:
            if (string.IsNullOrWhiteSpace(TeacherFName) || string.IsNullOrWhiteSpace(TeacherLName))
            {
                TempData["ErrorMessage"] = "Teacher's first name and last name cannot be empty.";
                return RedirectToAction("Edit", new { id = id });
            }

            if (hiredate > DateTime.Now)
            {
                TempData["ErrorMessage"] = "Hire date cannot be in the future.";
                return RedirectToAction("Edit", new { id = id });
            }

            if (salary < 0)
            {
                TempData["ErrorMessage"] = "Salary cannot be less than 0.";
                return RedirectToAction("Edit", new { id = id });
            }

            // Construct a Teacher object with the updated values.
            Teacher UpdatedTeacher = new Teacher
            {
                TeacherFName = TeacherFName,
                TeacherLName = TeacherLName,
                EmployeeNumber = EmployeeNumber,
                hiredate = hiredate,
                salary = salary
            };

            // Call the API to update the teacher.
            int result = _api.UpdateTeacher(id, UpdatedTeacher);

            // Check the result of the update operation.
            if (result <= 0)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred while updating the teacher.";
                return RedirectToAction("Edit", new { id = id });
            }

            TempData["SuccessMessage"] = "Teacher updated successfully.";
            return RedirectToAction("Show", new { id = id });
        }

    }
}