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

Added display for all Courses taught by that teacher On Teacher/Show Page

