using Lin.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lin.IService
{
    public interface ITeacherService
    {
        public void Add(Teacher entity);
        public void Delete(string id);
        public void Delete(Guid id);
        public void Delete(Teacher entity);
        public Task<Teacher> Select(Guid id);
        public Task<Teacher> Select(Expression<Func<Teacher, bool>> where);
        public Task<List<Teacher>> SelectList(Expression<Func<Teacher, bool>> where);
        public void Update(Teacher entity);
    }
}
