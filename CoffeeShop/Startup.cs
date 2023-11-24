using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UseCases;
using UseCases.Interfaces;
using UseCases.RepositoryInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.OData.Edm;

using Core;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.OData.ModelBuilder;
using OData.Swagger.Services;

namespace CoffeeShop
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoffeeShop", Version = "v1" });
            });
            services.AddDbContext<CoffeeShopDbContext>(
                //options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")) // DockerConnection
                options => options.UseSqlServer(Configuration.GetConnectionString("DockerConnection")) // DockerConnection
                );
            services.AddIdentity<IdentityUser, IdentityRole>()
                  .AddEntityFrameworkStores<CoffeeShopDbContext>()
                  .AddDefaultTokenProviders();

            services.AddControllers().AddOData(options =>
                options.AddRouteComponents("odata", GetEdmModel()).Filter().Select().Expand().OrderBy().Count()

            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("CoffeeShop", new OpenApiInfo() { Title = "CoffeeShop", Version = "v1" });
            });
            services.AddOdataSwaggerSupport();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
                .AddJwtBearer( options => 
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWTKey:ValidAudience"],
                        ValidIssuer = Configuration["JWTKey:ValidIssuer"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["JWTKey:Secret"]))
                    };                
                });

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddScoped<IAccountRegistration, AccountRegistration>();
            services.AddScoped<IUserRegistrationUseCase, UserRegistrationUseCase>();

            services.AddScoped<ICoffeeRepository, CoffeeRepository>();
            services.AddScoped<ICoffeeService, CoffeeService>();

        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<CoffeeModel>("Coffees");
            return builder.GetEdmModel();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoffeeShop"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            
          

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();               
            });
        }
    }
}
