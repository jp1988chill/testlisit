using Prueba.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using Prueba.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using System.Text.Json;
using Prueba.Domain;
using Prueba.Domain.Interfaces.Helper;

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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Rest API",
                    Description = "RESTAPI.",
                    TermsOfService = new Uri("http://exampleServer/exampleservice.svc"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact() { Name = "Swagger Developer", Email = "swagger@dotNet.com" }
                });
                //var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<PruebaContext>( //Registrar EntifyFramework Core
                options => options
                    .UseSqlServer(Configuration.GetConnectionString("database"))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            //NetCore 3.0 has CamelCase resolver enabled by default, but still, we enforce it.
            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);

            //NetCore 3.x+ requires this to prevent external methods overriding EndPointRouting
            services.AddMvc(options => options.EnableEndpointRouting = false);

            //Enforce NewtonsoftJson Swagger NetCore 2.x deserialization, over the newer NetCore 3.x one
            services.AddControllers().AddNewtonsoftJson();

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

            //Acceso a AppSettings
            services.AddSingleton<IAppSettingsRepository, AppSettingsRepository>();

            //Acceso a MiscHelpers
            services.AddSingleton<IMiscHelpers, MiscHelpers>();
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

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseMvc();
        }
    }
}
