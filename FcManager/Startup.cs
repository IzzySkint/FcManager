using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FcManager.Data;
using FcManager.Repositories;
using FcManager.Services;
using FcManager.Validators;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FcManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<FcManagerDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("FcManagerConnection")), ServiceLifetime.Transient);

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<FcManagerDbContext>()
                .AddDefaultTokenProviders();
            
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SymmetricSecurityKey"]));
            var tokenValidationParameter = new TokenValidationParameters()
            {
                IssuerSigningKey = signingKey,
                ValidateIssuer =false,
                ValidateAudience=false,
                //ValidateLifetime=false,
                //ClockSkew=TimeSpan.Zero
            };
            
            services.AddAuthentication(x => x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwt => 
                    {
                        //jwt.SaveToken = true;
                        //jwt.RequireHttpsMetadata = true;
                        jwt.TokenValidationParameters = tokenValidationParameter;
                    }
                );
            services.AddSingleton<IConfiguration>(this.Configuration);
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped<IActionValidatorFactory, ActionValidatorFactory>();
            services.AddSwaggerDocument(configure => configure.Title = "Football Club Manager API");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseExceptionHandler(
                options =>
                {
                    options.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync($"An unexpected error occurred.");
                    });
                }
            );

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
