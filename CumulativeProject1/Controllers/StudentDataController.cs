using CumulativeProject1.Models;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CumulativeProject1.Controllers
{
    public class StudentDataController : ApiController
    {
        //Use My Class to Access the Database
        private SchoolDbContext School = new SchoolDbContext();

        ///<summary>
        ///Returns a list of the student objects in mySchoolDb database
        ///</summary>
        ///<returns>
        ///A list of the student objects
        ///</returns>
        ///<example>
        ///GET api/StudentData/ListTeachers > []
        ///</example>

        [HttpGet]
        [Route("api/StudentData/ListStudents")]

        public List<Student> ListStudents(string StudentSearchKey)
        {
            //Create a MySQL Connection to my mySchoolDb
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection to the db
            Connection.Open();

            //Create a command
            MySqlCommand Command = Connection.CreateCommand();

            //Command Text is MySQL Query; Will search students fname, lname, studentnumber or enrollment date
            Command.CommandText = "SELECT * FROM students WHERE studentfname LIKE @key OR studentlname LIKE @key OR studentnumber LIKE @key OR enroldate LIKE @key";
            Command.Parameters.AddWithValue("@key", "%" + StudentSearchKey + "%");
            Command.Prepare();

            //Get SQL Result set into a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            //set up a list of teachers
            List<Student> Students = new List<Student>();

            //loop through my resultset
            while (ResultSet.Read())
            {
                //Collect Student Information
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFName = (string)ResultSet["studentfname"];
                string StudentLName = (string)ResultSet["studentlname"];
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];

                //Create Student Object
                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFName = StudentFName;
                NewStudent.StudentLName = StudentLName;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;

                //Add the object to the list
                Students.Add(NewStudent);
            }

            // Close MySQL Connection
            Connection.Close();

            //Return Teachers

            return Students;
        }

        /// <summary>
        /// Returns a student from the mySchoolDb by specifying the primary key = teacherid
        /// </summary>
        /// <param name="id">the student's ID in the database</param>
        /// <returns>the student object where param=id = studentid</returns>
        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            //Create a MySQL Connection to my mySchoolDb
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL QUERY
            Command.CommandText = "Select * from students where studentid = @id";
            Command.Parameters.AddWithValue("@id", id);
            Command.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            while (ResultSet.Read())
            {
                //Collect student Information
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFName = (string)ResultSet["studentfname"];
                string StudentLName = (string)ResultSet["studentlname"];
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFName = StudentFName;
                NewStudent.StudentLName = StudentLName;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;
            }


            return NewStudent;
        }


    }
}
