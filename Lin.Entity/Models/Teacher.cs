using System;
using System.Collections.Generic;
using System.Text;

namespace Lin.Entity.Models
{
    public class Teacher : BaseEntity
    {
        public int TeacherID { get; set; }
        public string Sex { get; set; }
        public string TeacherName { get; set; }
        public string Password { get; set; }
    }
}
