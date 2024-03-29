using DatabaseAccessLayer.EFCore.DBContexts;
using DatabaseAccessLayer.EFCore.Repositories;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPanel.SignalR;

namespace WebPanel
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("AppCore"));
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            #region Services

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSignalR();

            #endregion

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{Controller=Home}/{action=Index}/{Id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<RequestTripHub>("/RequestTripHub");
            });

        }
    }
}
