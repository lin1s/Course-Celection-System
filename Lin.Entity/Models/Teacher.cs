using Lin.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lin.Entity.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public Sex Sex { get; set; }
        public string TeacherName { get; set; }
    }
}
