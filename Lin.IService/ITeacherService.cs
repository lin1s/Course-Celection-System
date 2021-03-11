using Lin.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lin.IServices
{
    public interface ITeacherService : IBaseService<Teacher>
    {
        public void Delete(string id);
        public Task<Teacher> Select(string id);

    }
}
