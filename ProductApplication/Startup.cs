using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using DataAccessLayer.FileRepositories;
using DataAccessLayer.DBRepositories;
using DataAccessLayer.Interfaces;
using ProductApplication.ExceptionHandler;
using ProductApplication.Logging;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;


namespace ProductApplication
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

            services.AddControllers();

            services.AddControllersWithViews();

            services.AddSingleton<ILogging, LoggingNLog>();

            services.AddScoped<IProductService, ProductService>();

            services.AddSingleton<IProductRepository, ProductFileRepository>();

            //services.AddSingleton<IProductRepository, ProductDBRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductApplication", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogging logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductApplication v1"));
            }

            app.UseHttpsRedirection();
            
            app.ExceptionsHandler(logger);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
