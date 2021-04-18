
using Autofac;
using Autofac.Extras.DynamicProxy;
using CC.Yi.API.Extension;
using CC.Yi.API.Filter;
using CC.Yi.BLL;
using CC.Yi.Common.Cache;
using CC.Yi.Common.Castle;
using CC.Yi.Common.Jwt;
using CC.Yi.DAL;
using CC.Yi.IBLL;
using CC.Yi.IDAL;
using CC.Yi.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public void ConfigureServices(IServiceCollection services)
        {
            // ����Jwt
            services.AddAuthorization(options =>
            {
                //���û��ڲ��Ե���֤
                options.AddPolicy("myadmin", policy =>
                    policy.RequireRole("admin"));
            });

           
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options => {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,//�Ƿ���֤Issuer
                           ValidateAudience = true,//�Ƿ���֤Audience
                           ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                           ClockSkew = TimeSpan.FromSeconds(30),
                           ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                           ValidAudience = JwtConst.Domain,//Audience
                           ValidIssuer = JwtConst.Domain,//Issuer���������ǰ��ǩ��jwt������һ��
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConst.SecurityKey))//�õ�SecurityKey
                       };
                   });


            services.AddControllers();
            services.AddSwaggerService();
            services.AddSession();



            //���ù�����
            Action<MvcOptions> filters = new Action<MvcOptions>(r => {
                //r.Filters.Add(typeof(DbContextFilter));
            });
            services.AddMvc(filters);

            //�������ݿ�����
            string connection1 = Configuration["ConnectionStringBySQL"];
            string connection2 = Configuration["ConnectionStringByMySQL"];
            string connection3 = Configuration["ConnectionStringBySQLite"];
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connection1, b => b.MigrationsAssembly("CC.Yi.API"));//�������ݿ�
            });


            //����ע��ת����Autofac
            //services.AddScoped(typeof(IBaseDal<>), typeof(BaseDal<>));
            //services.AddScoped(typeof(IstudentBll), typeof(studentBll));

            //reidsע��
            //services.AddSingleton(typeof(ICacheWriter), new RedisCacheService(new Microsoft.Extensions.Caching.Redis.RedisCacheOptions()
            //{
            //    Configuration = Configuration.GetSection("Cache.ConnectionString").Value,
            //    InstanceName = Configuration.GetSection("Cache.InstanceName").Value
            //}));


            //����Identity�����֤
            //services.AddIdentity<result_user, IdentityRole>(options =>
            // {
            //     options.Password.RequiredLength = 6;//������̳���
            //     options.Password.RequireDigit = false;//������������
            //     options.Password.RequireLowercase = false;//��������Сд��ĸ
            //     options.Password.RequireNonAlphanumeric = false;//�������������ַ�
            //     options.Password.RequireUppercase = false;//���������д��ĸ
            //    //options.User.RequireUniqueEmail = false;//ע�������Ƿ���Բ��ظ�
            //    //options.User.AllowedUserNameCharacters="abcd"//����ֻ������������ַ�
            //}).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

            //�����������
            services.AddCors(options => options.AddPolicy("CorsPolicy",
             builder =>
             {
                 builder.AllowAnyMethod()
                     .SetIsOriginAllowed(_ => true)
                     .AllowAnyHeader()
                     .AllowCredentials();
             }));
        }

        //��ʼ��ʹ�ú���
        private void InitData(IServiceProvider serviceProvider)
        {
            //var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            //var context = serviceScope.ServiceProvider.GetService<DataContext>();
            //DbContentFactory.Initialize(context);//���þ�̬�෽��ע��
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerService();
            }

            //app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            InitData(app.ApplicationServices);
        }
    }
}
