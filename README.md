Added Data Access and Model Type Description File: A class that connects to the MySQL database and interacts with the Teachers table.

File: /Models/SchoolDbContext.cs Model: Represents a teacher, including properties that map to the database fields for teachers.

File: /Models/Teacher.cs 2. Controllers Web API Controller: Allows access to information about teachers via API endpoints for CRUD operations.

File: /Controllers/TeacherAPIController.cs MVC Controller: Manages routing to dynamic pages for displaying and managing teacher data through server-rendered views.

File: /Controllers/TeacherPageController.cs 3. Views Teacher List View: Displays a list of teachers from the MySQL Database.

File: /Views/Teacher/List.cshtml Teacher Detail View: Displays individual teacher details from the MySQL Database.

File: /Views/Teacher/Show.cshtml Project Components and Functionality CRUD Operations (MVP) Create: Added new teacher records to the database. Read: Retrieve teacher data, with Part 1 specifically focusing on read functionality. Database Context (SchoolDbContext) The SchoolDbContext class is responsible for configuring the connection to the MySQL database and setting up the required DbSet for the Teachers table.

Controller Functionality TeacherAPIController: Contains Web API methods for performing CRUD operations on the Teachers table, allowing access to teacher information for applications or external services.

TeacherPageController: Renders web pages using server-side MVC, enabling users to view teacher lists and individual teacher profiles.

Views The project includes two server-rendered views for teachers:

List View (List.cshtml): Renders a list of all teachers. Show View (Show.cshtml): Displays detailed information about a specific teacher.

Included evidence of testing as a PDF file. This includes: Screenshots of cURL commands Screenshots of webpages displaying CRUD functionality

Added Layout and Styling Adjustments to Teacher List

Added Read functionality for Students

Added Read functionality for Courses

Added Data Access and Model Type Description File: A class that connects to the MySQL database and interacts with the Teachers table.

File: /Models/SchoolDbContext.cs Model: Represents a teacher, including properties that map to the database fields for teachers.

File: /Models/Teacher.cs 2. Controllers Web API Controller: Allows access to information about teachers via API endpoints for CRUD operations.

File: /Controllers/TeacherAPIController.cs MVC Controller: Manages routing to dynamic pages for displaying and managing teacher data through server-rendered views.

File: /Controllers/TeacherPageController.cs 3. Views Teacher List View: Displays a list of teachers from the MySQL Database.

File: /Views/Teacher/List.cshtml Teacher Detail View: Displays individual teacher details from the MySQL Database.

File: /Views/Teacher/Show.cshtml Project Components and Functionality CRUD Operations (MVP) Create: Added new teacher records to the database. Read: Retrieve teacher data, with Part 1 specifically focusing on read functionality. Database Context (SchoolDbContext) The SchoolDbContext class is responsible for configuring the connection to the MySQL database and setting up the required DbSet for the Teachers table.

Controller Functionality TeacherAPIController: Contains Web API methods for performing CRUD operations on the Teachers table, allowing access to teacher information for applications or external services.

TeacherPageController: Renders web pages using server-side MVC, enabling users to view teacher lists and individual teacher profiles.

Views The project includes two server-rendered views for teachers:

List View (List.cshtml): Renders a list of all teachers. Show View (Show.cshtml): Displays detailed information about a specific teacher.

Included evidence of testing as a PDF file. This includes: Screenshots of cURL commands Screenshots of webpages displaying CRUD functionality

Added Layout and Styling Adjustments to Teacher List

Added Read functionality for Students

Added Read functionality for Courses

CUMULATIVE 2

Added Functionality in Cumulative 2

Add Teacher:

Web page to add new teacher records.

Validation for:

Empty names.

Hire dates in the future.

Duplicate employee numbers.

Delete Teacher:

Web page to confirm and delete teacher records.

Error handling for cases where the teacher does not exist.

Error Handling:


Detailed error messages for the following cases:

Empty teacher name.

Future hire date.

Duplicate employee numbers.

Attempting to delete a non-existent teacher.

API Testing:

Testing API for adding a teacher using POST with cURL commands.

Testing API for deleting a teacher using DELETE with cURL commands.

Web Page Testing:

Verified pages for: 

Adding a teacher.

Deleting a teacher (confirmation and success pages).

Handled edge cases and displayed appropriate error messages.

Read Functionality:

Added support for displaying Students and Courses data.

Expanded functionality for listing and displaying related records.

Evidence of Testing

Testing was performed for all functionalities, and screenshots are provided as a PDF file. Testing highlights include:


API Testing with cURL Commands:


Add teacher: Validated successful additions and error scenarios (empty name, duplicate employee number, future hire date).

Delete teacher: Verified successful deletions and handling non-existent teacher records.

Web Page Testing:

Added teacher: Verified form validation, error handling, and successful record addition.

Delete teacher: Verified confirmation page, successful deletion, and error handling for missing records.

ADDED Cumulative 3 part of Project
Update Teacher
Web Page:
A form to update teacher details (Name, Hire Date, Salary, etc.).
Validations:
Name cannot be empty.
Hire Date must not be in the future.
Salary must not be less than 0.
API Endpoint:
PUT /api/teachers/{id}
Updates a teacher's information.
Handles errors if the teacher does not exist.

Evidence of Testing
All tests were documented, and screenshots of API and web page testing are included in the accompanying PDF file.

Added API Testing with cURL Commands

Update Teacher:
Tested successful updates.
Verified error handling for:
Non-existent teacher. (server side)
Empty name. (client side)
Future hire date. (client side)
Negative salary.(client side)
Web Page Testing

Update Teacher:
Verified form validations and error messages.
Tested successful updates.
Technical Documentation
API Methods

Updates teacher information with validation for empty names, future hire dates, and negative salary.
Teacher Model Properties
Name: string (required, non-empty)
EmployeeNumber: int (unique)
HireDate: DateTime (cannot be in the future)
Salary: decimal (cannot be negative)

Added Improvements
Server-Side Error Handling:
Added error response for updates when trying to update a non-existent teacher.
Client-Side Error Handling:
Validations for:
Empty teacher name.
Future hire dates.
Negative salaries.

Testing Evidence
Refer to the included PDF file (testing C3.pdf) for:

Screenshots of successful and error-case tests for APIs using cURL.
Screenshots of web page form validations and success/error messages.
