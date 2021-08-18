using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Susep.SISRH.Application.Configurations;
using SUSEP.Framework.CoreFilters.Concrete;

namespace Susep.SISRH.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            ContainerBuilder container = new ContainerBuilder();
            services.AddMvc(options =>
            {
                options.Filters.Add(new HeaderFilter());
                options.Filters.Add(new ProtocolFilter());
                //options.Filters.Add(new ValidatorFilter());
            });

            services.ConfigureOptions(Configuration);

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential() //AddSigningCredential()
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                .AddResourceOwnerValidator<Application.Auth.ResourceOwnerPasswordValidator>();
            //.AddLdapUsers<OpenLdapAppUser>(Configuration.GetSection("ldapActiveDirectory"), UserStore.InMemory);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Sistema de recursos humanos",
                        Version = "v1",
                        Description = "API REST para manutenção de dados de colaboradores da Susep",
                    });
            });

            services.ConfigureOptions(Configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MediatorModuleConfiguration());
            builder.RegisterModule(new ApplicationModuleConfiguration(Configuration));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment() || env.IsEnvironment("Homolog"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema de corretores");
                });
            }
            app.UseHsts();

            app.UseCors(option => option.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader());

            app.UseRouting();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
