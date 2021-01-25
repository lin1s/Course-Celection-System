using Lin.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lin.IService
{
    public interface IStudentService
    {
        public void Add(Student entity);
        public void Delete(Expression<Func<Student, bool>> where);
        public void Delete(string id);
        public void Delete(Guid id);
        public Task<Student> Select(Expression<Func<Student, bool>> where);
        public Task<Student> Select(Guid id);
        public Task<List<Student>> SelectList(Expression<Func<Student, bool>> where);
        public Task<List<Student>> SelectList();
        public void Update(Student entity);
    }
}
