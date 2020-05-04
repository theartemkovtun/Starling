using System;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Starling.Aes;
using Starling.Application.Contracts;
using Starling.Application.Requests.Files.Queries.GetFile;
using Starling.Application.Requests.Users.Commands.AuthorizeUser;
using Starling.Application.Requests.Users.Commands.RegisterUser;
using Starling.Domain.Repositories;
using Starling.Domain.Repositories.Contracts;
using Starling.Ecdsa;
using Starling.WebApi.ViewModels;

namespace Starling.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        private IAuthorizationOptions AuthorizationOptions = new AuthorizationOptions();
        
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile($"{Environment.CurrentDirectory}/Properties/configuration.json");
            Configuration = builder.Build();
            
            Configuration.GetSection("authorizationOptions").Bind(AuthorizationOptions);
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(GetFileQuery).GetTypeInfo().Assembly);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddCors(options =>
            {
                options.AddPolicy("Default", 
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            services.AddDistributedMemoryCache();
            services.AddSession();
            
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSingleton(AuthorizationOptions);

            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IShareRepository, ShareRepository>();

            services.AddTransient<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
            services.AddTransient<IValidator<AuthorizeUserCommand>, AuthorizeUserCommandValidator>();

            services.AddTransient<IEcdsa, Ecdsa.Ecdsa>();
            services.AddTransient<IAesEncryptor, AesEncryptor>();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = AuthorizationOptions.Issuer,
                        ValidAudience = AuthorizationOptions.Audience,
                        IssuerSigningKey = AuthorizationOptions.SymmetricSecurityKey
                    };
                });
            

            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Starling Api", Version = "v1" });
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors("Default");

            app.UseAuthentication();

            app.Use((context, next) =>
            {
                context.Response.Headers.Add("Access-Control-Expose-Headers", new[] {"*"});
                return next.Invoke();
            });

            app.UseMvcWithDefaultRoute();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Starling Api V1");
            });
        }
    }
}