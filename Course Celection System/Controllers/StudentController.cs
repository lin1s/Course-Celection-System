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

        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> GetStudentList()
        {
            List<Student> studentList = await _student.SelectList();
            return new JsonResult(studentList);
        }

        [Route("add")]
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            student.ID = Guid.NewGuid();
            student.CreateTime = DateTime.Now;
            student.IsDelete = false;
            try
            {
                _student.Add(student);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { code = ResultCode.错误, message = ex.Message });
            }
            return new JsonResult(new { code = ResultCode.正常 });
        }


    }
}
