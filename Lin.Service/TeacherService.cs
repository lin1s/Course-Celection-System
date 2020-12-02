﻿using Lin.Data.DBContext;
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
    public class TeacherService : ITeacherService
    {
        private readonly SystemDBContext _context;

        public TeacherService(SystemDBContext context)
        {
            _context = context;
        }

        public void Add(Teacher entity)
        {
            entity.ID = Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            entity.IsDelete = false;
            entity.LastUpdateTime = DateTime.Now;
            _context.Add(entity);
        }

        public void Delete(Guid id)
        {
            Teacher teacher = _context.teacher.Where(x => x.ID == id).FirstOrDefault();
            teacher.IsDelete = true;
            _context.Update(teacher);
        }

        public void Detele(Teacher entity)
        {
            _context.Remove(entity);
        }

        public async Task<List<Teacher>> Select(Expression<Func<Teacher, bool>> where)
        {
            return await _context.teacher.Where(where).ToListAsync();
        }

        public async Task<Teacher> Select(Guid id)
        {
            return await _context.teacher.Where(x => x.ID == id && !x.IsDelete).FirstOrDefaultAsync();
        }

        public void Update(Teacher entity)
        {
            Teacher teacher = _context.teacher.Where(x => x.ID == entity.ID).FirstOrDefault();
            teacher.LastUpdateTime = DateTime.Now;
            teacher.Sex = entity.Sex;
            teacher.TeacherName = entity.TeacherName;
            _context.Update(teacher);
        }
    }
}