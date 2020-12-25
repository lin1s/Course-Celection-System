using Lin.Data.DBContext;
using Lin.Data.Redis;
using Lin.Entity.Models;
using Lin.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lin.Service
{
    public class StudentService : IStudentService
    {
        private readonly SystemDBContext _context;

        public StudentService(SystemDBContext context)
        {
            _context = context;
        }

        public void Add(Student entity)
        {
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            entity.IsDelete = false;
            entity.LastUpdateTime = DateTime.Now;
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            Student student = _context.student.Where(x => x.ID == id).FirstOrDefault();
            student.IsDelete = true;
            _context.Update(student);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            Student student = _context.student.Where(x => x.StudentID == id).FirstOrDefault();
            student.IsDelete = true;
            _context.Update(student);
            _context.SaveChanges();
        }

        public void Delete(Student entity)
        {
            entity.IsDelete = true;
            _context.Update(entity);
            _context.SaveChanges();
        }

        public async Task<List<Student>> SelectList(Expression<Func<Student, bool>> where)
        {
            return await _context.student.Where(where).ToListAsync();
        }

        public async Task<Student> Select(Guid id)
        {
            return await _context.student.Where(x => x.ID == id && !x.IsDelete).FirstOrDefaultAsync();
        }

        public async Task<Student> Select(Expression<Func<Student, bool>> where)
        {
            return await _context.student.Where(where).FirstOrDefaultAsync();
        }

        public void Update(Student entity)
        {
            Student student = _context.student.Where(x => x.ID == entity.ID).FirstOrDefault();
            student.LastUpdateTime = DateTime.Now;
            student.Sex = entity.Sex;
            student.StudentName = entity.StudentName;
            _context.Update(student);
            _context.SaveChanges();
        }
    }
}
