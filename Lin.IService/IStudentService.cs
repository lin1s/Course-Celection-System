﻿using Lin.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lin.IService
{
    public interface IStudentService
    {
        public void Add(Student entity);
        public void Delete(int id);
        public void Delete(Guid id);
        public void Delete(Student entity);
        public void Update(Student entity);
        public Task<Student> Select(Guid id);
        public Task<List<Student>> Select(Expression<Func<Student, bool>> where);
    }
}