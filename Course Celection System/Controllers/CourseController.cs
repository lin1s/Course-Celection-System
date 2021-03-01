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
    public class CourseController : Controller
    {
        private readonly ICourseService _course;
        private readonly ITeacherService _teacher;

        public CourseController(ICourseService course,ITeacherService teacher)
        {
            _course = course;
            _teacher = teacher;
        }

        [Route("getList")]
        [HttpGet]
        public async Task<IActionResult> GetCourseList()
        {
            List<Course> courseList = await _course.SelectList();
            List<ViewModelCourse> viewCourseList = new List<ViewModelCourse>();
           
            foreach (var course in courseList)
            {
                ViewModelCourse item = new ViewModelCourse();
                item= item.AutoCopy(course);
                Teacher teacher = await _teacher.Select(item.TeacherID);
                item.TeacherName = teacher.TeacherName;
                viewCourseList.Add(item);
            }
            return new JsonResult(new { code = ResultCode.正常, data = viewCourseList.OrderBy(x => x.CreateTime) });
        }

        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> GetCourse(string courseId)
        {
            Course course = await _course.Select(courseId);
            Teacher teacher = await _teacher.Select(course.TeacherID);
            ViewModelCourse viewCourse = new ViewModelCourse();
            viewCourse = viewCourse.AutoCopy(course);
            viewCourse.TeacherName = teacher.TeacherName;
            return new JsonResult(new {code = ResultCode.正常, data = viewCourse});
        }

        [Route("add")]
        [HttpPost]
        public IActionResult CourseAdd([FromBody] Course course)
        {
            course.ID = Guid.NewGuid();
            course.CreateBy = (Guid) course.LastUpdateBy;
            course.CreateTime = DateTime.Now;
            course.LastUpdateTime=DateTime.Now;
            course.IsDelete = false;
            course.HaveStu = 0;
            try
            {
                _course.Add(course);
            }
            catch (Exception e)
            {
                return new JsonResult(new { code = ResultCode.错误, message = e.Message });
            }
            return new JsonResult(new { code = ResultCode.正常 });
        }

        [Route("update")]
        [HttpPost]
        public IActionResult CourseUpdate([FromBody] Course course)
        {
            course.LastUpdateTime=DateTime.Now;
            try
            {
                _course.Update(course);
            }
            catch (Exception e)
            {
                return new JsonResult(new { code = ResultCode.错误, message = e.Message });
            }
            return new JsonResult(new { code = ResultCode.正常 });
        }

        [Route("del")]
        [HttpGet]
        public IActionResult CourseDel(string courseID)
        {
            try
            {
                _course.Delete(courseID);
            }
            catch (Exception e)
            {
                return new JsonResult(new { code = ResultCode.错误, message = e.Message });
            }
            return new JsonResult(new { code = ResultCode.正常 });
        }
    }
}
