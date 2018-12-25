using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HzyAdmin
{
    using System.IO;
    using UEditor.Core;
    using Common;
    using Common.LogService;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogHelper.CreateRepository("Log4Net/log4net.config");
            Configuration = configuration;
            Tools.Log.WriteLog("应用程序已启动！");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container. 
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;//关闭GDPR规范
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //session 注册
            services.AddSession(item =>
            {
                item.IdleTimeout = TimeSpan.FromMinutes(60 * 2);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //自定义 视图 
            services.Configure<Microsoft.AspNetCore.Mvc.Razor.RazorViewEngineOptions>(item =>
            {
                item.AreaViewLocationFormats.Clear();
                item.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");

                item.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
                item.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
                item.AreaViewLocationFormats.Add("/Areas/{2}/Views/Sys/{1}/{0}.cshtml");//系统管理
                item.AreaViewLocationFormats.Add("/Areas/{2}/Views/Base/{1}/{0}.cshtml");//基础信息管理
                item.AreaViewLocationFormats.Add("/Areas/{2}/Views/Operate/{1}/{0}.cshtml");//运营管理
                item.AreaViewLocationFormats.Add("/Areas/{2}/Views/Statistics/{1}/{0}.cshtml");//统计管理
            });

            //注入链接字符串
            DbFrame.DBContext.Initialization(Configuration.GetSection("AppConfig:SqlServerConnStr").Value);

            //Ueditor  编辑器 服务端 注入  configFileRelativePath: "wwwroot/Admin/lib/nUeditor/net/config.json", isCacheConfig: false, basePath: "C:/basepath"
            services.AddUEditorService(configFileRelativePath: Directory.GetCurrentDirectory() + "/wwwroot/Admin/lib/nUeditor/config.json",
                isCacheConfig: false,
                basePath: Directory.GetCurrentDirectory() + "\\wwwroot\\Admin\\lib\\nUeditor\\");

            //配置跨域处理
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("any", builder =>
            //    {
            //        builder.AllowAnyOrigin() //允许任何来源的主机访问
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials();//指定处理cookie
            //    });
            //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //添加控制台输出
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();


            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //session 注册
            app.UseSession();
            //将 对象 IHttpContextAccessor 注入 HttpContextHelper 静态对象中
            Common.HttpContextService.HttpContextHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Login}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
