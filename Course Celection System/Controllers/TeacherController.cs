using Lin.Entity.Enum;
using Lin.Entity.Models;
using Lin.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Celection_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacher;

        public TeacherController(ITeacherService teacher)
        {
            _teacher = teacher;
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Add(Teacher teacher,string UserKey)
        {
            JsonMessage result = new JsonMessage();
            teacher.ID = Guid.NewGuid();
            teacher.CreateTime = DateTime.Now;
            teacher.CreateBy = new Guid(UserKey);
            teacher.LastUpdateTime = DateTime.Now;
            teacher.LastUpdateBy = new Guid(UserKey);
            teacher.IsDelete = false;
            
            try
            {
                _teacher.Add(teacher);
            }
            catch (Exception ex)
            {
                result.code = 500;
                result.message = ex.Message;
                return new JsonResult(result);
            }

            result.code = 20000;
            result.message = "添加成功";
            return new JsonResult(result);
        }

        [Route("del")]
        [HttpGet]   
        public IActionResult Delete(string id)
        {
            JsonMessage result = new JsonMessage();
            try
            {
                _teacher.Delete(id);
            }
            catch (Exception ex)
            {
                result.code = 500;
                result.message = ex.Message;
                return new JsonResult(result);
            }
            result.code = 20000;
            result.message = "删除成功";
            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeacherList()
        {
            List<Teacher> teacherList = await _teacher.SelectList(x => !x.IsDelete);
            JsonMessage result = new JsonMessage();
            result.code = 20000;
            result.message = "查询成功";
            //return new JsonResult(result);
            return new JsonResult(new { code = ResultCode.正常, data = teacherList });
        }

        [Route("getTeacher")]
        [HttpGet]
        public async Task<IActionResult> GetTeacher(Guid id)
        {
            Teacher teacher = await _teacher.Select(x => x.ID == id);
            return new JsonResult(teacher);
        }
    }
}
