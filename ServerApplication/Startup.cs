using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Common;
using WebFramework.Configuration;
using WebFramework.CustomMapping;
using WebFramework.Middlewares;
using WebFramework.Swagger;

namespace ServerApplication
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly SiteSettings _siteSetting;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.InitializeAutoMapper();
            services.AddDbContext(Configuration);
            services.AddCustomIdentity(_siteSetting.IdentitySettings);
            services.AddMinimalMvc();
            services.AddElmahCore(Configuration, _siteSetting);
            services.AddJwtAuthentication(_siteSetting.JwtSettings);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServerApplication", Version = "v1" });
            });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServerApplication v1"));
            }

            app.IntializeDatabase();
            app.UseCustomExceptionHandler();
            app.UseHsts(env);
            app.UseHttpsRedirection();
            app.UseElmahCore(_siteSetting);

            //app.UseSwaggerAndUI();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //Use this config just in Develoment (not in Production)
            //app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseEndpoints(config =>
            {
                config.MapControllers(); // Map attribute routing
                //    .RequireAuthorization(); Apply AuthorizeFilter as global filter to all endpoints
                //config.MapDefaultControllerRoute(); // Map default route {controller=Home}/{action=Index}/{id?}
            });

            //Using 'UseMvc' to configure MVC is not supported while using Endpoint Routing.
            //To continue using 'UseMvc', please set 'MvcOptions.EnableEndpointRouting = false' inside 'ConfigureServices'.
            //app.UseMvc();
        }
    }
}
