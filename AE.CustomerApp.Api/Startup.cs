using AE.CustomerApp.Core;
using AE.CustomerApp.Core.Mappings;
using AE.CustomerApp.Infra.Data.Context;
using AE.CustomerApp.Infra.IoC;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            RegisterAppSettings(services);
            RegisterServices(services);
            RegisterMappingProfiles(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        #region Private methods

        private static void RegisterServices(IServiceCollection services)
        {
            // Register services
            DependencyContainer.RegisterServices(services);
        }

        private void RegisterAppSettings(IServiceCollection services)
        {
            // App settings configuration
            services.AddOptions();
            services.Configure<ConnectionStringConfiguration>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<AppSettingsConfiguration>(Configuration.GetSection("AppSettings"));
        }

        private void RegisterMappingProfiles(IServiceCollection services)
        {
            // Register mapping profiles
            services.AddAutoMapper(typeof(DtoMappingProfile));
        }

        #endregion

    }
}
