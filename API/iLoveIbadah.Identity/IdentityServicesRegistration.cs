using Microsoft.EntityFrameworkCore.Infrastructure;
// using iLoveIbadah.Infrastructure.iLoveIbadah.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.Options;
using iLoveIbadah.Application.Models.Identity;
using iLoveIbadah.Application.Contracts.Identity;
using iLoveIbadah.Identity.Models;
using iLoveIbadah.Identity.Services;

namespace iLoveIbadah.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            var jwtSettingsKey = configuration.GetSection("jwtsettingskey").Value;
            //TODO don't get the key from here and pass it as parameter to authservice, instead, inside auth service in the symetricsecuritykey method go pass the key by getsection inside the parameter of the method
            // comment ABOVE isn't correct.
            //well previous comment is wrong, i already pass key trough the jwtsettings. but the issue is it has another name in appsettings so it can't bind key with jwtsettings class key property. it will be a headach to change name cause it is in azure key vault and in usersecrets so maybe TODO when migrating from azure key vault to Hashicorp Vault in future or Github Secrets or something else.
            if (string.IsNullOrEmpty(jwtSettingsKey))
            {
                throw new Exception("jwtsettingskey is not configured properly.");
            }

            services.AddDbContext<iLoveIbadahIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetSection("azuresqlserverconnectionstring").Value));
            //b => b.MigrationAssembly(typeof(ILoveIbadahIdentityDbContext).Assembly.FullName))); I don't need migration!

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            // Password Settings
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                //options.Lockout.MaxFailedAccessAttempts = 10;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
                .AddEntityFrameworkStores<iLoveIbadahIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IAuthService, AuthService>(provider =>
            {
                return new AuthService(provider.GetRequiredService<UserManager<ApplicationUser>>(), provider.GetRequiredService<IOptions<JwtSettings>>(), jwtSettingsKey);
            });
            services.AddTransient<IUserService, UserService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettingsKey))
                    };
                });

            return services;
        }
    }
}
