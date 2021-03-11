using Lin.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lin.IServices
{
    public interface IRecordService :IBaseService<SelectRecord>
    {
        public Task AddAsync(SelectRecord entity);

    }
}
