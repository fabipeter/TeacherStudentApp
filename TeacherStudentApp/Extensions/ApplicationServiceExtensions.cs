using System;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using FluentValidation.AspNetCore;
using MediatR;
using System.Collections;
using Application.Account;
using FluentValidation;

namespace TeacherStudentApp.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string connStr;

                // Depending on if in development or production, use either FlyIO
                // connection string, or development connection string from env var.
                if (env == "Development")
                {
                    // Use connection string from file.
                    connStr = config.GetConnectionString("DefaultConnection");
                }
                else
                {
                    // Use connection string provided at runtime by Flyio.
                    //var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                    // Parse connection URL to connection string for Npgsql
                    //connUrl = connUrl.Replace("postgres://", string.Empty);
                    //var pgUserPass = connUrl.Split("@")[0];
                    //var pgHostPortDb = connUrl.Split("@")[1];
                    //var pgHostPort = pgHostPortDb.Split("/")[0];
                    //var pgDb = pgHostPortDb.Split("/")[1];
                    //var pgUser = pgUserPass.Split(":")[0];
                    //var pgPass = pgUserPass.Split(":")[1];
                    //var pgHost = pgHostPort.Split(":")[0];
                    //var pgPort = pgHostPort.Split(":")[1];
                    //var updatedHost = pgHost.Replace("flycast", "internal");

                    //connStr = $"Server={updatedHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};";
                    connStr = config.GetConnectionString("DefaultConnection");
                }

                // Whether the connection string came from the local development configuration file
                // or from the environment variable from FlyIO, use it to set up your DbContext.
                options.UseNpgsql(connStr);
            });
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                
            }).AddEntityFrameworkStores<ApplicationDbContext>().
           AddDefaultTokenProviders();
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:3000");
                });
            });
            //services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddMediatR(typeof(Register));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Register>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}

