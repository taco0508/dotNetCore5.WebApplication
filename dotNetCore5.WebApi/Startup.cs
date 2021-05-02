using System;
using System.IO;
using dotNetCore5.Business.Infrastructure.Mappings;
using dotNetCore5.Business.IServices;
using dotNetCore5.Business.Services;
using dotNetCore5.DataAccess.Infrastructure.Connection;
using dotNetCore5.DataAccess.Infrastructure.SqlHelper;
using dotNetCore5.DataAccess.IRepositories;
using dotNetCore5.DataAccess.Repositories;
using dotNetCore5.WebApi.Infrastructure.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace dotNetCore5.WebApi
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

            services.AddControllers();

            //Connection
            services.AddTransient<IConnectionHelper, ConnectionHelper>();
            services.AddTransient<IConnectionStringHelper, ConnectionStringHelper>();

            services.AddScoped<ISqlHelper, SqlHelper>();

            //Repository
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            //Service
            services.AddTransient<ICustomerService, CustomerService>();

            //AutoMapper
            services.AddAutoMapper
            (
                typeof(ControllerMappingProfile),
                typeof(ServiceMappingProfile)
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotNetCore5.WebApi", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFiles = Directory.EnumerateFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly);

                foreach (var xmlFile in xmlFiles)
                {
                    c.IncludeXmlComments(xmlFile);
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotNetCore5.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
