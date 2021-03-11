using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lin.IServices
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        public void Add(TEntity entity);
        public void Delete(Expression<Func<TEntity, bool>> where);
        public void Delete(Guid id);
        public Task<TEntity> Select(Expression<Func<TEntity, bool>> where);
        public Task<TEntity> Select(Guid id);
        public Task<List<TEntity>> SelectList(Expression<Func<TEntity, bool>> where);
        public Task<List<TEntity>> SelectList();
        public void Update(TEntity entity);

    }
}
