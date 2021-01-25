using Lin.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lin.Data.DBContext
{
    public class SystemDBContext : DbContext
    {
        public SystemDBContext(DbContextOptions<SystemDBContext> options)
            : base(options) { }

        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<SelectRecord> SelectRecords { get; set; }
    }
}
