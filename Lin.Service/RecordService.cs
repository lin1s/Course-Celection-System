using Lin.Data.DBContext;
using Lin.Entity.Models;
using Lin.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbContextUtils;

namespace Lin.Services
{
    public class RecordService : IRecordService
    {
        private readonly SystemDBContext _context;

        public RecordService(SystemDBContext context)
        {
            _context = context;
        }

        public void Add(SelectRecord entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            _context.DetachAll();
        }

        public async Task AddAsync(SelectRecord entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            _context.DetachAll();
        }


        public void Delete(Expression<Func<SelectRecord, bool>> where)
        {
            SelectRecord entity = _context.SelectRecord.Where(where).FirstOrDefault();
            entity.IsDelete = true;
            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            this.Delete(x => x.ID == id);
        }

        public async Task<SelectRecord> Select(Expression<Func<SelectRecord, bool>> where)
        {
            return await _context.SelectRecord.Where(where).FirstOrDefaultAsync();
        }

        public async Task<SelectRecord> Select(Guid id)
        {
            return await this.Select(x => x.ID == id);
        }

        public async Task<List<SelectRecord>> SelectList(Expression<Func<SelectRecord, bool>> where)
        {
            return await _context.SelectRecord.Where(where).ToListAsync();
        }

        public async Task<List<SelectRecord>> SelectList()
        {
            return await this.SelectList(x => !x.IsDelete);
        }

        public void Update(SelectRecord entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            _context.DetachAll();
        }

        public int GetCount(Expression<Func<SelectRecord, bool>> where)
        {
            return _context.SelectRecord.Where(where).Count();
        }
        public int GetCount()
        {
            return this.GetCount(x => true);
        }
    }
}
