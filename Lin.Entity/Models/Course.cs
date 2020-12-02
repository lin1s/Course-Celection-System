using System;
using System.Collections.Generic;
using System.Text;

namespace Lin.Entity.Models
{
    public class Course : BaseEntity
    {
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public Guid TeacherID { get; set; }
        public string TeacherName { get; set; }
        public int MaxNum { get; set; }
        public int HaveStu { get; set; }
    }
}
