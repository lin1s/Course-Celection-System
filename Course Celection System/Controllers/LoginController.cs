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
    public class LoginController : Controller
    {
        private readonly ITeacherService _teacher;
        private readonly IStudentService _student;

        public LoginController(ITeacherService teacher, IStudentService student)
        {
            _teacher = teacher;
            _student = student;
        }

        [HttpPost]
        public async Task<IActionResult> StudentLogin(string UserName, string Password)
        {
            JsonMessage result = new JsonMessage();
            Student user = await _student.Select(x => x.StudentID == UserName && !x.IsDelete);
            if(user.Password!=Password)
            {
                result.status = 500;
                result.message = "账号或者密码错误";
            }
            result.status = 200;
            result.message = "登陆成功";
            return new JsonResult(result);
        }
    }
}
