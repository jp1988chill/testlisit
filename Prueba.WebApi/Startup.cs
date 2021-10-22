using Prueba.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prueba.Domain;
using MediatR;
using System;
using Prueba.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Prueba.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen();
            services.AddDbContext<PruebaContext>( //Registrar EntifyFramework Core
                options => options
                    .UseSqlServer(Configuration.GetConnectionString("database"))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            //Capa seguridad

            //Regla de seguridad: Valida Token por argumento tipo API Key o JWT (URL de servidor: SSO_URL_AUTHORITY, no usado)
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration.GetValue("SSO_URL_AUTHORITY", "");
                    options.Audience = "account";
                });

            //Genera contexto HTTP dentro de Handler de Validación (para acceder al mismo)
            services.AddHttpContextAccessor();

            //Handler Validación Token
            services.AddAuthorization(op =>
            {
                op.AddPolicy("ValidarCliente", policy =>
                    policy.Requirements.Add(new ValidarClienteRequirement(true)));
            });
            services.AddSingleton<IAuthorizationHandler, ValidarClienteHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"v1/swagger.json", "My API V1");
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
