using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using CumulativeProject1.Models;
using System.Web.Mvc;

namespace CumulativeProject1.Controllers
{

    public class StudentController : Controller {

        /// <summary>
        /// GET: Student Index View
        /// </summary>
        /// <returns>
        /// The student index view
        /// </returns>

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: Student/List View
        /// </summary>
        /// <param name="StudentSearchKey">
        /// a string that will search against student db by first name, last name, student number or enrollment date.
        /// </param>
        /// <returns>
        /// A list of students matching the StudentSearchKey
        /// </returns>

        public ActionResult List(string StudentSearchKey)
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents(StudentSearchKey);
            return View(Students);
        }

        /// <summary>
        /// GET: Student/Show/{id}
        /// </summary>
        /// <param name="id">id = studentid in my school data database</param>
        /// <returns>
        /// A view of the student object
        /// </returns>

        public ActionResult Show(int id)
        {
           StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }
    }

}
