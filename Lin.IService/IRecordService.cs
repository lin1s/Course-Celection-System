using Lin.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lin.IServices
{
    public interface IRecordService :IBaseService<SelectRecord>
    {
        public Task AddAsync(SelectRecord entity);
        public int GetCount(Expression<Func<SelectRecord, bool>> where);
        public int GetCount();

    }
}
