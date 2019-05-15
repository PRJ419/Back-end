using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Database;
using Database.Interfaces;
using Database.Repository_Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using WebApi.Controllers;
using WebApi.DTOs.AutoMapping;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Areas.Identity.Data;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi
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
            // *** Inspired from below link *******************
            // https://stackoverflow.com/questions/40275195/how-to-setup-automapper-in-asp-net-core
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            // *************************************************

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // Dependency injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 0;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            // Need to access controller from Identity user registration 
            services.AddTransient<CustomerController>();
            services.AddTransient<BarController>();
            services.AddTransient<BarRepresentativeController>();

            services.AddMvc();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("BarRep", policy => policy.RequireClaim("Role", "BarRep"));
                options.AddPolicy("Kunde", policy => policy.RequireClaim("Role", "Kunde"));
                options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            "the secret that needs to be at least 16 characters long for HmacSha256")),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });



            //var connection = @"Data Source=DESKTOP-UGIDUH3;Initial Catalog=PRJ4Database;Integrated Security=True";
            //services.AddDbContext<BarOMeterContext>(options => options.UseSqlServer(connection));
            services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new Info
                {
                    Title = "Bar-O-Meter API",
                    Version = "v1",
                    Description = "RESTful API der tillader adgang til database.",
                    Contact = new Contact
                    {
                        Name = "Tobias Sandø Lund"
                    }
                });

            
            c.OperationFilter<SecurityRequirementsOperationFilter>();

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
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

            app.UseAuthentication();
            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "swagger";
            });
            app.UseMvc();

        }
    }
}
