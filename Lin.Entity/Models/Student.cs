using System;
using System.Collections.Generic;
using System.Text;

namespace Lin.Entity.Models
{
    public class Student : BaseEntity
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string StudentClass { get; set; }
        public string Password { get; set; }
    }
}
