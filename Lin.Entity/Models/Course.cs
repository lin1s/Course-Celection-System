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
        public int MaxNum { get; set; }
        public int HaveStu { get; set; }
        public string CourseTime { get; set; }
        public string CoursePlace { get; set; }
        public int Credit { get; set; }
        public string Semester { get; set; }
    }
}
