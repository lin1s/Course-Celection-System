﻿using Lin.Entity.Models;
using Lin.IService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Add(Teacher teacher)
        {
            JsonMessage result = new JsonMessage();
            try
            {
                _teacher.Add(teacher);
            }
            catch
            {
                result.status = 500;
                result.message = "错误";
                return new JsonResult(result);
            }
            result.status = 200;
            result.message = "添加成功";
            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Teacher> teacherList = await _teacher.Select(x => !x.IsDelete);
            return new JsonResult(teacherList);
        }
    }
}