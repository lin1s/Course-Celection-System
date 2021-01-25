﻿using Lin.Data.DBContext;
using Lin.Data.Redis;
using Lin.Entity.Models;
using Lin.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<Student, bool>> where)
        {
            Student student = _context.Student.Where(where).FirstOrDefault();
            student.IsDelete = true;
            _context.Update(student);
        }

        public void Delete(string id)
        {
            this.Delete(x => x.StudentID == id);
        }

        public void Delete(Guid id)
        {
            this.Delete(x => x.ID == id);
        }

        public async Task<Student> Select(Expression<Func<Student, bool>> where)
        {
            return await _context.Student.Where(where).FirstOrDefaultAsync();
        }


        public async Task<Student> Select(Guid id)
        {
            return await this.Select(x => x.ID == id);
        }


        public async Task<List<Student>> SelectList(Expression<Func<Student, bool>> where)
        {
            return await _context.Student.Where(where).ToListAsync();
        }

        public async Task<List<Student>> SelectList()
        {
            return await this.SelectList(x => true);
        }

        public void Update(Student entity)
        {
            _context.Update(entity);
        }
    }
}
