using Lin.Data.DBContext;
using Lin.Data.Redis;
using Lin.Entity.Models;
using Lin.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lin.Services
{
    public class CourseService : ICourseService
    {
        private readonly SystemDBContext _context;

        public void Add(Course entity)
        {
            _context.Add(entity);
        }

        public void Delete(Expression<Func<Course, bool>> where)
        {
            Course entity =_context.Course.Where(where).FirstOrDefault();
            entity.IsDelete = true;
            _context.Update(entity);
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Course> Select(Expression<Func<Course, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<Course> Select(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Course>> SelectList(Expression<Func<Course, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<List<Course>> SelectList()
        {
            throw new NotImplementedException();
        }

        public void Update(Course entity)
        {
            throw new NotImplementedException();
        }
    }
}
