using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativeProject1.Models
{
    //this class defines what represents a student
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentFName { get; set; }
        public string StudentLName { get; set; }
        public string StudentNumber { get; set; }
        public DateTime EnrolDate { get; set; }
    }
}