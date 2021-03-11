using Lin.Data.DBContext;
using Lin.Entity.Models;
using Lin.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbContextUtils;

namespace Lin.Services
{
    public class CourseService : ICourseService
    {
        private readonly SystemDBContext _context;

        public CourseService(SystemDBContext context)
        {
            _context = context;
        }

        public void Add(Course entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            _context.DetachAll();
        }

        public void Delete(Expression<Func<Course, bool>> where)
        {
            Course entity =_context.Course.Where(where).FirstOrDefault();
            entity.IsDelete = true;
            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            this.Delete(x => x.CourseID == id);
        }

        public void Delete(Guid id)
        {
            this.Delete(x => x.ID == id);
        }

        public async Task<Course> Select(Expression<Func<Course, bool>> where)
        {
            return await _context.Course.Where(where).FirstOrDefaultAsync();
        }

        public async Task<Course> Select(Guid id)
        {
            return await this.Select(x => x.ID == id);
        }

        public async Task<Course> Select(string id)
        {
            return await this.Select(x => x.CourseID == id);
        }

        public async Task<List<Course>> SelectList(Expression<Func<Course, bool>> where)
        {
            return await _context.Course.Where(where).ToListAsync();
        }

        public async Task<List<Course>> SelectList()
        {
            return await this.SelectList(x => !x.IsDelete);
        }

        public void Update(Course entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            _context.DetachAll();
        }
    }
}
