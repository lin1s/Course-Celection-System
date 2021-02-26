using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lin.Entity.Models
{
    public class ViewModelCourse : Course
    {
        public string TeacherName { get; set; }

        public ViewModelCourse AutoCopy(Course parent)
        {
            ViewModelCourse child = new ViewModelCourse();

            var ParentType = typeof(Course);

            var Properties = ParentType.GetProperties();

            foreach (var Propertie in Properties)
            {
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }

            return child;
        }
    }
}
