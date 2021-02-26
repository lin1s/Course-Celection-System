using System;
using System.Collections.Generic;
using System.Text;

namespace Lin.Entity
{
    public class BaseEntity
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public Guid LastUpdateBy { get; set; }
        public bool IsDelete { get; set; }
        public Guid? DeleteBy { get; set; }
    }
}
