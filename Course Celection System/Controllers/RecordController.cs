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
    public class RecordController : Controller
    {
        private readonly IRecordService _record;

        public RecordController(IRecordService record)
        {
            _record = record;
        }

        public async 
    }
}
