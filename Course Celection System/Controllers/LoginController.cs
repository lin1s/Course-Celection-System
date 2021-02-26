using Lin.Entity.Enum;
using Lin.Entity.Models;
using Lin.IServices;
using Microsoft.AspNetCore.Http;
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

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(string UserID, string Password, string Permission)
        {
            int code = (int)ResultCode.错误;
            string token = null, message = null;
            if (Permission == "teacher")
            {
                if (!await TeacherLoginCheckAsync(UserID, Password))
                {
                    code = (int)ResultCode.登陆失败;
                    message = "账号或密码错误，请重新输入";
                }
                else
                {
                    code = (int)ResultCode.正常;
                    token = "teacher-token";
                }
            }
            else if (Permission == "student")
            {
                if (!await StudentLoginCheckAsync(UserID, Password))
                {
                    code = (int)ResultCode.登陆失败;
                    message = "账号或密码错误，请重新输入";
                }
                else
                {
                    code = (int)ResultCode.正常;
                    token = "teacher-token";
                }
            }
            else if (Permission == "admin")
            {

            }
            return new JsonResult(new { code = code, data = token, message = message });
        }

        private async Task<bool> TeacherLoginCheckAsync(string TeacherID, string Password)
        {
            Teacher user = await _teacher.Select(x => x.TeacherID == TeacherID);
            if (user.Password != Password)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> StudentLoginCheckAsync(string StudentID, string Password)
        {
            Student user = await _student.Select(x => x.StudentID == StudentID);
            if (user.Password != Password)
            {
                return false;
            }
            return true;
        }

        private async void AdminLoginCheck(string UserName, string Password)
        {
        }
    }
}
