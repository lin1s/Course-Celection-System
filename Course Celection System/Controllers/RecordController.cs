﻿using Lin.Entity.Enum;
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
        private readonly ITeacherService _teacher;
        private readonly HashOperator _redis = new HashOperator();

        public RecordController(IRecordService record, ICourseService course,IStudentService student,ITeacherService teacher)
        {
            _record = record;
            _course = course;
            _student = student;
            _teacher = teacher;  
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> RecordAdd([FromBody] SelectRecord record)
        {
            int a = _record.GetCount(x => x.CourseID == record.CourseID && x.StudentID == record.StudentID);
            if (a > 0) return new JsonResult(new { code = ResultCode.错误, message = "已选过该课程，请确认后再提交" });
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
            List<ViewModelCourse> viewCourseList = _redis.GetAll<ViewModelCourse>("courseList");
            List<Course> courseList = await _course.SelectList();
            if (viewCourseList.Count < _course.GetCount())
            {
                courseList = await _course.SelectList();
                foreach (var course in courseList)
                {
                    ViewModelCourse item = new ViewModelCourse();
                    item = item.AutoCopy(course);
                    Teacher teacher = await _teacher.Select(item.TeacherID);
                    item.TeacherName = teacher.TeacherName;
                    viewCourseList.Add(item);
                }
                courseList.ForEach(x =>
                {
                    _redis.Set("courseList", x.ID.ToString(), x);
                });
            }
            return new JsonResult(new { code = ResultCode.正常, data = viewCourseList });

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
