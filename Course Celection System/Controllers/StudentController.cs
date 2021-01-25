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

        [Route("getByID")]
        [HttpGet]
        public async Task<IActionResult> GetStudent(string UserID)
        {
            Student student = await _student.Select(x => x.StudentID == UserID);
            return new JsonResult(student);
        }

        [Route("getByGuid")]
        [HttpGet]
        public async Task<IActionResult> GetStudentByGuid(Guid ID)
        {
            Student student = await _student.Select(ID);
            return new JsonResult(student);
        }
    }
}
