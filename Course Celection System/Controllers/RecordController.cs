using Lin.Entity.Enum;
using Lin.Entity.Models;
using Lin.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lin.Data.Redis;

namespace Course_Celection_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : Controller
    {
        private readonly IRecordService _record;
        private readonly ICourseService _course;
        private readonly IStudentService _student;
        private readonly HashOperator _redis = new HashOperator();

        public RecordController(IRecordService record, ICourseService course,IStudentService student)
        {
            _record = record;
            _course = course;
            _student = student;
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> RecordAdd([FromBody] SelectRecord record)
        {
            record.ID = Guid.NewGuid();
            record.CreateBy = (Guid)record.LastUpdateBy;
            record.CreateTime = DateTime.Now;
            record.LastUpdateTime = DateTime.Now;
            record.IsDelete = false;
            try
            {
                await _record.AddAsync(record);
                _redis.Set("recordList", record.ID.ToString(), record);
            }
            catch (Exception e)
            {
                return new JsonResult(new { code = ResultCode.错误, message = e.Message });
            }
            return new JsonResult(new { code = ResultCode.正常 });
        }

        [Route("getCourse")]
        [HttpGet]
        public async Task<IActionResult> GetCourse()
        {
            List<Course> courseList = _redis.GetAll<Course>("courseList");
            if (courseList.Count < _course.GetCount())
            {
                courseList = await _course.SelectList();
                courseList.ForEach(x =>
                {
                    _redis.Set("courseList", x.ID.ToString(), x);
                });
            }
            return new JsonResult(new { code = ResultCode.正常, data = courseList });
        }

        [Route("getChoose")]
        [HttpGet]
        public async Task<IActionResult> GetChoose(string studentId)
        {
            List<SelectRecord> recordList = _redis.GetAll<SelectRecord>("recordList");
            recordList = (List<SelectRecord>)recordList.Where(x => x.StudentID == new Guid(studentId));
            if (recordList.Count<_record.GetCount(x=>x.StudentID==new Guid(studentId)))
            {
                recordList = await _record.SelectList(x => x.StudentID == new Guid(studentId));
                recordList.ForEach(x =>
                {
                    _redis.Set("recordList", x.ID.ToString(), x);
                });
            }
            return new JsonResult(new { code = ResultCode.正常, data = recordList });
        }

        [Route("getStudentByCourse")]
        [HttpGet]
        public async Task<IActionResult> GetSutdentByCourseAsync(string courseId)
        {

            List<SelectRecord> recordList = await _record.SelectList(x => x.CourseID == new Guid( courseId));
            List<Student> studentList = new List<Student>();
            foreach(var record in recordList)
            {
                Student student = await _student.Select(record.StudentID);
                studentList.Add(student);
            }
            return new JsonResult(new { code = ResultCode.正常, data = studentList });

        }
    }
}
