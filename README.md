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
