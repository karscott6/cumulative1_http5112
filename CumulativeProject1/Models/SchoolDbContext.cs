using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient; //search for mysql.data and install to project

namespace CumulativeProject1.Models
{
    public class SchoolDbContext
    {
        // My Local School Database Information
        // These are kept as private properties
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "schooldb"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //ConnectionString = credentials
        //convert zero datetime, returns 0000-00-00 as null

        protected static string DatabaseConnectionString { 
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

        ///<summary>
        /// This method will return a connection to my school database.
        ///</summary>
        ///<example>
        ///private SchoolDBContext School = new SchoolDbContext();
        ///MySqlConnection Connection = School.AccessDatabase();
        ///</example>
        ///<returns>
        ///A MySqlConnection Object to the schooldb
        ///</returns>
        ///

        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(DatabaseConnectionString);
        }

    }
}