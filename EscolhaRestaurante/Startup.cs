using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EscolhaRestaurante.Models;
using Microsoft.EntityFrameworkCore;

namespace EscolhaRestaurante
{
    public class Startup
    {
        public string ContentRootPathToken = "%CONTENTROOTPATH%";
        private string _contentRootPath = "";
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _contentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["Data:EscolhaRestaurante:ConnectionString"];
            if (connectionString.Contains(ContentRootPathToken))
                connectionString = connectionString.Replace(ContentRootPathToken, _contentRootPath);

            services.AddDbContext<EscolhaRestauranteDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddTransient<IEscolhaRestauranteRepository, EFEscolhaRestauranteRepository>();
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Restaurante}/{action=Index}/{id?}");
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
