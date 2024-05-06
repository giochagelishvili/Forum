using Asp.Versioning;
using Forum.Shared.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Forum.API.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomHealthChecks(this IServiceCollection services, IConfiguration config)
        {
            var host = config.GetValue<string>("Constants:Host");
            services.AddCommonHealthChecks(config)
                .AddUrlGroup(new Uri($"{host}/api/topic"), "Topics Endpoint");
        }

        public static void UseConfiguredVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opts =>
            {
                opts.ApiVersionReader = new UrlSegmentApiVersionReader();
                opts.DefaultApiVersion = new(1, 0);
                opts.AssumeDefaultVersionWhenUnspecified = true;
            })
                .AddApiExplorer(opts =>
                {
                    opts.GroupNameFormat = "'v'VVV";
                    opts.SubstituteApiVersionInUrl = true;
                });
        }

        public static void UseSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opts =>
            {
                opts.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Description = "Authorization"
                });

                opts.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearer"
                                }
                            },
                        Array.Empty<string>()
                    }
                });

                opts.SwaggerDoc("v1", new OpenApiInfo() { Title = "Forum API", Version = "1.0" });
                opts.SwaggerDoc("v2", new OpenApiInfo() { Title = "Forum API", Version = "2.0" });
            });
        }

        public static void AddTokenAuthorizaion(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = config["AuthConfiguration:Issuer"],
                    ValidAudience = config["AuthConfiguration:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["AuthConfiguration:SecretKey"]))
                };
            });
        }
    }
}