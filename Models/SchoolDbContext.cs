using MySql.Data.MySqlClient;

namespace cumulative1.Models
{
    public class SchoolDbContext
    {
        //These are readonly properties. 
        //Settings for Xampp
        //DB for school  (backendassignment3) 
        private static string User { get { return "root"; } }
        private static string Password { get { return ""; } }
        private static string Database { get { return "backendassignment3"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //This is how we access the database--
        /// <summary>
        /// Establishes a connection to the school database.
        /// </summary>
        /// <example> 
        /// Example usage:
        /// private SchoolDbContext schoolContext = new SchoolDbContext();
        /// MySqlConnection connection = schoolContext.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection instance</returns>
        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }
       // This is an example of using the MySqlConnection class to create an object, which represents a connection to our school database on localhost, using port 3306.
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
