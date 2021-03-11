﻿using Lin.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lin.IServices
{
    public interface ICourseService : IBaseService<Course>
    {
        public void Delete(string id);
        public Task<Course> Select(string id);

    }
}
