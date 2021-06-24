using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YSoftSoftware.Data.Abstract;
using YSoftSoftware.Data.Concrete.adonet;
using YSoftSoftware.Data.Concrete.ef;
using YSoftSoftware.Data.Concrete.Identity;

namespace YSoftSoftware.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, AdoUnitOfWork>();
            services.AddTransient<ICompensationRepository, AdoCompensationRepository>();
            services.AddTransient<IProjectRepository, AdoProjectRepository>();
            services.AddTransient<IPersonelRepository, AdoPersonelRepository>();
            services.AddTransient<IDepartmentRepository, AdoDepartmentRepository>();
            services.AddTransient<IAccountingProgramRepository, AdoAccountingProgramRepository>();
            
            services.AddDbContext<YSoftContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),b=>b.MigrationsAssembly("YSoftSoftware.WebUI")));
            services.AddDbContext<ApplicationIdentityDbContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"),b=>b.MigrationsAssembly("YSoftSoftware.WebUI")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseRouting();
            app.UseAuthentication();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });

            SeedData.Seed(app);
            SeedIdentity.CreateIdentityUser(app.ApplicationServices, Configuration).Wait();

        }
    }
}
