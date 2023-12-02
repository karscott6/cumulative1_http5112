using CumulativeProject1.Models;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CumulativeProject1.Controllers
{
    public class TeacherDataController : ApiController
    {
        //Use My Class to Access the Database
        private SchoolDbContext School = new SchoolDbContext();

        ///<summary>
        ///Returns a list of the Teacher objects in mySchoolDb database
        ///</summary>
        ///<returns>
        ///A list of the teacher objects
        ///</returns>
        ///<example>
        ///GET api/TeacherData/ListTeachers > []
        ///</example>

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/TeacherData/ListTeachers")]

        public List<Teacher> ListTeachers(string TeacherSearchKey)
        {
            //Create a MySQL Connection to my mySchoolDb
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection to the db
            Connection.Open();

            //Create a command
            MySqlCommand Command = Connection.CreateCommand();

            //Command Text is MySQL Query; Will search teachers fname, lname, hiredate or salary
            Command.CommandText = "SELECT * FROM teachers WHERE teacherfname LIKE @key OR teacherlname LIKE @key OR salary LIKE @key OR hiredate LIKE @key";
            Command.Parameters.AddWithValue("@key", "%" + TeacherSearchKey + "%");
            Command.Prepare();

            //Get SQL Result set into a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            //set up a list of teachers
            List<Teacher> Teachers = new List<Teacher>();

            //loop through my resultset
            while (ResultSet.Read())
            {
                //Collect Teacher Information
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = (string)ResultSet["teacherfname"];
                string TeacherLName = (string)ResultSet["teacherlname"];
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                //Create Teacher Object
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFName = TeacherFName;
                NewTeacher.TeacherLName = TeacherLName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //Add the object to the list
                Teachers.Add(NewTeacher);
            }

            // Close MySQL Connection
            Connection.Close();

            //Return Teachers

            return Teachers;
        }

        /// <summary>
        /// Returns ateacher from the mySchoolDb by specifying the primary key = teacherid
        /// </summary>
        /// <param name="id">the teacher's ID in the database</param>
        /// <returns>the teacher object where param=id = teacherid</returns>
        [System.Web.Http.HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create a MySQL Connection to my mySchoolDb
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL QUERY
            Command.CommandText = "Select * from teachers where teacherid = @id";
            Command.Parameters.AddWithValue("@id", id);
            Command.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            while (ResultSet.Read())
            {
                //Collect Teacher Information
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = (string)ResultSet["teacherfname"];
                string TeacherLName = (string)ResultSet["teacherlname"];
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFName = TeacherFName;
                NewTeacher.TeacherLName = TeacherLName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }


            return NewTeacher;
        }

        ///<summary>
        /// Add a teacher to the db
        /// </summary>
        /// <param name="NewTeacher">Object that links to teacher db</param>
        /// <example>
        /// POST api/TeacherData/New
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFName":"Kyle",
        ///	"TeacherLName":"Scott",
        ///	"TeacherEmployeeNumber":"A1234",
        ///	"TeacherHireDate":Now()",
        ///	"TeacherSalary":"50.00"
        /// }
        /// </example>
        /// 
        [System.Web.Http.HttpPost]

        public void AddTeacher([FromBody] Teacher NewTeacher)
        {

            //Create a MySQL Connection to my mySchoolDb
            MySqlConnection Connection = School.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFName);

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL QUERY
            Command.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFName,@TeacherLName,@EmployeeNumber, @HireDate, @Salary)";
            Command.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFName);
            Command.Parameters.AddWithValue("@TeacherLName", NewTeacher.TeacherLName);
            Command.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            Command.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            Command.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            Command.Prepare();

            Command.ExecuteNonQuery();

            Connection.Close();
        }

        /// <summary>
        /// Delete a teacher from the database if the ID exists.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/13</example>
        [System.Web.Http.HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL QUERY
            Command.CommandText = "Delete from teachers where teacherid=@id";
            Command.Parameters.AddWithValue("@id", id);
            Command.Prepare();

            Command.ExecuteNonQuery();

            Connection.Close();


        }

    }
}