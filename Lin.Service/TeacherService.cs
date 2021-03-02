using Lin.Data.DBContext;
using Lin.Entity.Models;
using Lin.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lin.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly SystemDBContext _context;

        public TeacherService(SystemDBContext context)
        {
            _context = context;
        }

        public void Add(Teacher entity)
        {
            _context.Teacher.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Expression<Func<Teacher, bool>> where)
        {
            Teacher teacher = _context.Teacher.Where(where).FirstOrDefault();
            teacher.IsDelete = true;
            _context.Update(teacher);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            this.Delete(x => x.TeacherID == id);
        }

        public void Delete(Guid id)
        {
            this.Delete(x => x.ID == id);
        }

        public async Task<Teacher> Select(Expression<Func<Teacher, bool>> where)
        {
            return await _context.Teacher.AsNoTracking().Where(where).FirstOrDefaultAsync();
        }


        public async Task<Teacher> Select(Guid id)
        {
            return await this.Select(x => x.ID == id);
        }

        public async Task<Teacher> Select(string id)
        {
            return await this.Select(x => x.TeacherID == id);
        }

        public async Task<List<Teacher>> SelectList(Expression<Func<Teacher, bool>> where)
        {
            return await _context.Teacher.AsNoTracking().Where(where).ToListAsync();
        }

        public async Task<List<Teacher>> SelectList()
        {
            return await this.SelectList(x=>!x.IsDelete);
        }

        public void Update(Teacher entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

    }
}
