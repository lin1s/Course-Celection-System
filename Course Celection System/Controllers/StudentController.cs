using Lin.Entity.Enum;
using Lin.Entity.Models;
using Lin.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course_Celection_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentService _student;

        public StudentController(IStudentService student)
        {
            _student = student;
        }

        [Route("getList")]
        [HttpGet]
        public async Task<IActionResult> GetStudentList()
        {
            List<Student> studentList = await _student.SelectList();
            return new JsonResult(new { code = ResultCode.正常, data = studentList });
        }

        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> GetStudent(string studentId)
        {
            Student student = await _student.Select(studentId);
            return new JsonResult(new {code = ResultCode.正常, data = student});
        }

        [Route("add")]
        [HttpPost]
        public IActionResult StudentAdd([FromBody] Student student)
        {
            student.ID = Guid.NewGuid();
            student.CreateTime = DateTime.Now;
            student.IsDelete = false;
            try
            {
                _student.Add(student);
            }
            catch (Exception e)
            {
                return new JsonResult(new { code = ResultCode.错误, message = e.Message });
            }
            return new JsonResult(new { code = ResultCode.正常 });
        }

        [Route("update")]
        [HttpPost]
        public IActionResult StudentUpdate([FromBody] Student student)
        {
            student.LastUpdateTime=DateTime.Now;
            try
            {
                _student.Update(student);
            }
            catch (Exception e)
            {
                return new JsonResult(new { code = ResultCode.错误, message = e.Message });
            }
            return new JsonResult(new { code = ResultCode.正常 });

        }

        [Route("del")]
        [HttpGet]
        public IActionResult StudentDel(string studentId)
        {
            try
            {
                _student.Delete(studentId);
            }
            catch (Exception e)
            {
                return new JsonResult(new { code = ResultCode.错误, message = e.Message });
            }
            return new JsonResult(new { code = ResultCode.正常 });
        }
    }
}
