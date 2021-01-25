using Lin.Entity.Models;
using Lin.IService;
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
            JsonMessage result = new JsonMessage();
            if (Permission == "teacher")
            {
                if (!await TeacherLoginCheckAsync(UserID, Password))
                {
                    result.status = 500;
                    result.message = "登录失败，请确认账号或密码";
                }
                result.status = 200;
                result.message = "登陆成功";
            }
            else if (Permission == "student")
            {
                if (!await StudentLoginCheck(UserID, Password))
                {
                    result.status = 500;
                    result.message = "登录失败，请确认账号或密码";
                }
                result.status = 200;
                result.message = "登陆成功";
            }
            else if (Permission == "admin")
            {

            }
            HttpContext.Session.SetString("userName", UserID);
            HttpContext.Session.SetString("permission", Permission);
            return new JsonResult(result);
        }

        private async Task<bool> TeacherLoginCheckAsync(string UserID, string Password)
        {
            Teacher user = await _teacher.Select(x => x.TeacherID == UserID);
            if (user.Password != Password)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> StudentLoginCheck(string UserID, string Password)
        {
            Student user = await _student.Select(x => x.StudentID == UserID);
            if (user.Password != Password)
            {
                return false;
            }
            return true;
        }

        private async void AdminLoginCheck(string UserName, string Password)
        {
            Teacher user = await _teacher.Select(x => x.TeacherID == UserName);
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout(string UserName)
        {
            JsonMessage result = new JsonMessage();
            HttpContext.Session.Remove(UserName);
            HttpContext.Session.Remove("permission");
            result.status = 200;
            result.message = "登出成功";
            return new JsonResult(result);
        }
    }
}
