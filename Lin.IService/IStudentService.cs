using Lin.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lin.IServices
{
    public interface IStudentService : IBaseService<Student>
    {
        public void Delete(string id);
        public Task<Student> Select(string id);

    }
}
