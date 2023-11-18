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

    public class TeacherController : Controller {

        /// <summary>
        /// GET: Teacher Index View
        /// </summary>
        /// <returns>
        /// The Teacher Index view
        /// </returns>

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: Teacher/List View
        /// </summary>
        /// <param name="TeacherSearchKey">
        /// a string that will search against teacher db by first name, last name, hire date and salary.
        /// </param>
        /// <returns>
        /// A list of Teachers matching the TeacherSearchKey
        /// </returns>
        public ActionResult List(string TeacherSearchKey)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(TeacherSearchKey);
            return View(Teachers);
        }

        /// <summary>
        /// GET: Teacher/Show/{id}
        /// </summary>
        /// <param name="id">id = teacherid in my school data database</param>
        /// <returns>
        /// A view of the teacher object
        /// </returns>
        public ActionResult Show(int id)
        {
           TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
    }

}
