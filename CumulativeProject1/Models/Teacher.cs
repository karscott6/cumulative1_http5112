using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativeProject1.Models
{
    //this class defines what represents a teacher
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherFName { get; set; }
        public string TeacherLName { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

    }
}