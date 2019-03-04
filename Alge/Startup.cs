using System.IO;
using System.Reflection;
using Alge.Controllers;
using Alge.Domain.Enums;
using Alge.Domain.Infastructure;
using Alge.Domain.Interfaces.Infastructure;
using Alge.Domain.Services;
using Alge.Helpers;
using Alge.Interfaces.Services;
using Alge.Middleware;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Alge
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddViewOptions(opt => opt.HtmlHelperOptions.ClientValidationEnabled = false).AddControllersAsServices();
            services.AddSignalR();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly(), Assembly.Load("Alge.Domain")).AsImplementedInterfaces().PropertiesAutowired();

            //Controllers
            builder.RegisterType<OcspController>().PropertiesAutowired();

            //Special Registrations
            builder.RegisterType<NonceService>().As<INonceService>().InstancePerLifetimeScope();
            builder.RegisterInstance(new Settings(Configuration["Version"], Configuration.GetConnectionString("Publish Date"), Configuration["CSP"], Configuration["LogDirectory"])).As<ISettings>().SingleInstance();
            builder.RegisterType<Ssl2ScanService>().Keyed<ITlsScanService>(Tls.SSL2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseStatusCodePages();
                Log.Logger = new LoggerConfiguration().MinimumLevel.Error().WriteTo.RollingFile(Path.Combine(Configuration["LogDirectory"], "log-{Date}.txt")).CreateLogger();
            }

            AutoMapperConfig.Init();

            app.UseMiddleware<NonceMiddleware>();
            app.UseMiddleware<ErrorMiddleware>();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });

            loggerFactory.AddSerilog();
        }
    }
}
