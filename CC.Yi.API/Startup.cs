
using Autofac;
using Autofac.Extras.DynamicProxy;
using CC.Yi.BLL;
using CC.Yi.Common.Cache;
using CC.Yi.Common.Castle;
using CC.Yi.DAL;
using CC.Yi.IBLL;
using CC.Yi.IDAL;
using CC.Yi.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Yi.API
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
         
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CC.Yi.API", Version = "v1" });
            });
            string connection = Configuration["ConnectionStringBySQL"];
            string connection2 = Configuration["ConnectionStringByMySQL"];
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connection, b => b.MigrationsAssembly("CC.Yi.API"));//设置数据库
            });


            //依赖注入转交给Autofac
            //services.AddScoped(typeof(IBaseDal<>), typeof(BaseDal<>));
            //services.AddScoped(typeof(IstudentBll), typeof(studentBll));
            services.AddSingleton(typeof(ICacheWriter), new RedisCacheService(new Microsoft.Extensions.Caching.Redis.RedisCacheOptions()
            {
                Configuration = Configuration.GetSection("Cache.ConnectionString").Value,
                InstanceName = Configuration.GetSection("Cache.InstanceName").Value
            }));


            //配置Identity身份认证
            services.AddIdentity<result_user, IdentityRole>(options =>
             {
                 options.Password.RequiredLength = 6;//密码最短长度
                 options.Password.RequireDigit = false;//密码需求数字
                 options.Password.RequireLowercase = false;//密码需求小写字母
                 options.Password.RequireNonAlphanumeric = false;//密码需求特殊字符
                 options.Password.RequireUppercase = false;//密码需求大写字母
                //options.User.RequireUniqueEmail = false;//注册邮箱是否可以不重复
                //options.User.AllowedUserNameCharacters="abcd"//密码只允许在这里的字符
            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
        }

        private void InitData(IServiceProvider serviceProvider)
        {
            var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                DbContentFactory.Initialize(context);//调用静态类方法注入
            
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CC.Yi.API v1"));
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            InitData(app.ApplicationServices);
        }
    }
}
