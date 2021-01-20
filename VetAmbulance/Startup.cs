using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VetAmbulance;
using VetAmbulance.DAL;
using VetAmbulance.DAL.Mappers;
using VetAmbulance.Helpers;

namespace VetAmbulance
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();

            services.AddScoped<LoginHelper>();

            services.AddScoped<PatientMapper>();
            services.AddScoped<VetAmbulance.BL.Models.Patient>();

            services.AddScoped<DiagnosisMapper>();
            services.AddScoped<VetAmbulance.BL.Models.Diagnosis>();

            services.AddScoped<VetMapper>();
            services.AddScoped<VetAmbulance.BL.Models.Vet>();

            services.AddScoped<AmbulanceMapper>();
            services.AddScoped<VetAmbulance.BL.Models.Ambulance>();

            services.AddScoped<DrugMapper>();
            services.AddScoped<VetAmbulance.BL.Models.Drug>();

            services.AddScoped<ReservationMapper>();
            services.AddScoped<VetAmbulance.BL.Models.Reservation>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddEntityFrameworkSqlServer().AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection"),
                b => b.MigrationsAssembly("VetAmbulance")));

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => options.LoginPath = "/Auth/Login");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
