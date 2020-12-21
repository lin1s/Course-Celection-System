using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lin.Entity.Models
{
    public class SelectRecord : BaseEntity
    {
        public Guid StudentID { get; set; }
        public Guid CourseID { get; set; }
    }
}
