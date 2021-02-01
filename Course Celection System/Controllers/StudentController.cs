using Lin.Entity.Models;
using Lin.IService;
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
            JsonMessage result = new JsonMessage();
            student.ID = Guid.NewGuid();
            student.CreateTime = DateTime.Now;
            student.IsDelete = false;
            try
            {
                _student.Add(student);
            }
            catch (Exception ex)
            {
                result.status = 500;
                result.message = ex.Message;
                return new JsonResult(result);
            }
            result.status = 200;
            result.message = "添加成功";
            return new JsonResult(result);
        }


    }
}
