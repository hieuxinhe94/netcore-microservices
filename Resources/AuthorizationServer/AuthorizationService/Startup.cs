using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AuthorizationService.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AuthorizeService.Core.Midware;
using AuthorizationService.Dal.Implemented;
using AuthorizationService.Dal.Interfaces;
using IdentityServer4.Validation;
using IdentityServer4.Services;
using AuthorizeService.Core.Configurations.Services;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace AuthorizationService
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
            var isDemoMode = Configuration.GetValue<bool>("IsDemoMode");

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            // var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
                .AddTransient<IProfileService, LoginProfileService>()
                .AddTransient<IAuthRepository, AuthRepository>();

            services.AddDbContext<LogInDbContext>(options =>
            {
                if (isDemoMode)
                {
                    options.UseInMemoryDatabase("IdentityServerDb");
                }
                else
                {
                    options.UseSqlServer(connectionString);
                }
            });

            if (isDemoMode)
            {
                services.AddIdentityServer()
                           .AddInMemoryClients(ConfigForDemo.GetClients())
                           .AddInMemoryIdentityResources(ConfigForDemo.GetIdentityResources())
                           .AddInMemoryApiResources(ConfigForDemo.GetApiResources())
                           .AddTestUsers(ConfigForDemo.Get())
                           .AddInMemoryPersistedGrants()
                           .AddDeveloperSigningCredential();
            }
            else
            {
                services.AddIdentityServer()
                          .AddInMemoryIdentityResources(IdentityResourcesConfig.GetIdentityResource())
                          .AddInMemoryApiResources(ApisConfig.GetApis())
                          .AddInMemoryClients(ClientConfig.GetClients())
                          ;

            }

            services.AddMvc();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseIdentityServer();
            // app.UseHttpsRedirection();

            app.UseRouting(routes =>
            {
                routes.MapControllers();
            });

            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
