using Lin.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lin.Entity.Models
{
    public class Student : BaseEntity
    {
        public int StuID { get; set; }
        public string StuName { get; set; }
        public Sex Sex { get; set; }
        public string Phone { get; set; }
    }
}
