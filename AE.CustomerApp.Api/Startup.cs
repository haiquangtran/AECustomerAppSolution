using AE.CustomerApp.Core;
using AE.CustomerApp.Core.Constants;
using AE.CustomerApp.Infra.Data.Context;
using AE.CustomerApp.Infra.IoC;
using AE.CustomerApp.Infra.IoC.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace AE.CustomerApp.Api
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
            services.AddDbContext<CustomerAppDbContext>(options => 
            {
                options.UseInMemoryDatabase(Configuration.GetConnectionString(nameof(ConnectionStringConfiguration.CustomerInMemoryDb)));
            });

            services.AddMvc(options => 
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc(ApiConstants.ApiVersion.Current, new Info
                {
                    Version = ApiConstants.ApiVersion.Current,
                    Title = ApiConstants.ApiTitle,
                    Description = ApiConstants.ApiDescription
                });

                // Xml file generated to use for Swagger comments
                var swaggerXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var swaggerXmlFilePath = Path.Combine(AppContext.BaseDirectory, swaggerXmlFile);
                var swaggerModelXmlFilePath = Path.Combine(AppContext.BaseDirectory, "AE.CustomerApp.Core.xml");
                // Enable Swagger to use Xml Comments
                config.IncludeXmlComments(swaggerXmlFilePath);
                config.IncludeXmlComments(swaggerModelXmlFilePath);

                // Enable Swagger Annotations
                config.EnableAnnotations();
            });

            ConfigureDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Set up global exception handler
            GlobalExceptionHandler.SetupGlobalExceptionHandler(app);
            
            app.UseHttpsRedirection();
            app.UseMvc();
            
            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint($"/swagger/{ApiConstants.ApiVersion.Current}/swagger.json", $"AE Customer API {ApiConstants.ApiVersion.Current}");
            });
        }

        #region Private methods

        private void ConfigureDependencies(IServiceCollection services)
        {
            // App settings configuration
            services.AddOptions();
            services.Configure<ConnectionStringConfiguration>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<AppSettingsConfiguration>(Configuration.GetSection("AppSettings"));
            services.AddSingleton(Configuration);

            // Register services
            DependencyContainer.RegisterServices(services);

            // Register mapping profiles
            DependencyContainer.RegisterMappingProfiles(services);
        }

        #endregion

    }
}
