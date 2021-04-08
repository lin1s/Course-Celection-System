using Lin.Data.GlobaSettings;
using Lin.Data.Redis;
using Lin.IServices;
using Lin.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Course_Celection_System
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            GlobaSettings.SetBaseConfig(
               Configuration.GetConnectionString("SqlConnection"),
               Configuration.GetSection("RedisStrings")["SqlConnection"],
               Configuration.GetSection("RedisStrings")["RedisMaxReadPool"],
               Configuration.GetSection("RedisStrings")["RedisMaxWritePool"]);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Course_Celection_System", Version = "v1" });
            });

            services.AddDatabase(GlobaSettings.SqlServerConnectionString);

            services.AddScoped(typeof(IStudentService), typeof(StudentService));

            services.AddScoped(typeof(ITeacherService), typeof(TeacherService));

            services.AddScoped(typeof(ICourseService), typeof(CourseService));

            services.AddScoped(typeof(IRecordService), typeof(RecordService));

            services.AddScoped(typeof(HashOperator),typeof(HashOperator));

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1800);
                options.Cookie.HttpOnly = true;
            });

            services.AddDistributedMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    //.AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Course_Celection_System v1"));
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("cors");
            app.UseAuthorization();

            app.UseCookiePolicy();
            app.UseSession();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
