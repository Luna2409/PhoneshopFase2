using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Phoneshop.Business.Data;
using Phoneshop.Business.Repositories;
using Phoneshop.Business.Services;
using Phoneshop.Domain.Interfaces;
using System;
using System.Text;

namespace Phoneshop.Api
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
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("ConnectionString")), ServiceLifetime.Scoped);

            services.AddControllers();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetSection("jwtSettings")["Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration.GetSection("jwtSettings")["SignKey"])),
                    ValidAudience = Configuration.GetSection("jwtSettings")["Audience"],
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

            services.AddScoped<IPhoneService, PhoneService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Phoneshop.Api", Version = "v1" });
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Phoneshop.Api v1"));
            }

            app.UseAuthentication();
            app.UseAuthorization();

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
