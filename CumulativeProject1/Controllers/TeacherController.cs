using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using CumulativeProject1.Models;
using System.Web.Mvc;
using System.Diagnostics;

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
        /// GET: New Teacher View
        /// </summary>
        /// <returns>
        /// The New Teacher View
        /// </returns>

        public ActionResult New()
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

        /// <summary>
        /// POST: Teacher/Create
        /// </summary>
        public ActionResult Create(Teacher NewTeacher)
        {
            // C# Input Validation (Initiative)
            if (string.IsNullOrWhiteSpace(NewTeacher.TeacherFName) ||
                string.IsNullOrWhiteSpace(NewTeacher.TeacherLName) ||
                string.IsNullOrWhiteSpace(NewTeacher.EmployeeNumber) ||
                NewTeacher.HireDate == default(DateTime) ||
                NewTeacher.Salary <= 0) //salary input has to be higher than 0
            {
                Debug.WriteLine("Invalid input data. Please correct and try again."); //Logs the Error
                ViewBag.ErrorMessage = "Invalid input data. Please correct and try again.";
                return View("New");
            }

            NewTeacher.TeacherFName = NewTeacher.TeacherFName;
            NewTeacher.TeacherLName = NewTeacher.TeacherLName;
            NewTeacher.EmployeeNumber = NewTeacher.EmployeeNumber;
            NewTeacher.HireDate = DateTime.Now;
            NewTeacher.Salary = NewTeacher.Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }


        //POST : /Author/Delete/{id}
        [System.Web.Http.HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }


    }

}