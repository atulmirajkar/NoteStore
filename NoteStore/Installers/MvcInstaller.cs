using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using NoteStore.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NoteStore.Services.V1;
using NoteStore.Services;
using Swashbuckle.Swagger;
using Microsoft.OpenApi.Models;

namespace NoteStore.Installers
{
    public class MvcInstaller : IInstaller
    {
        public MvcInstaller()
        {
        }

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //JWT
            var jwtSetting = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSetting);
            services.AddSingleton(jwtSetting);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSetting.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            //swagger
            services.AddSwaggerGen(x =>
            {
                var swaggerInfo = new OpenApiInfo();
                swaggerInfo.Description = "Note Store API";
                swaggerInfo.Version = "v1";
                x.SwaggerDoc("v1", swaggerInfo);


                var apiScheme = new OpenApiSecurityScheme();
                apiScheme.Description = "JWT authorization header using bearer scheme";
                apiScheme.Name = "authorization";
                apiScheme.In = ParameterLocation.Header;
                apiScheme.Type = SecuritySchemeType.ApiKey;

                x.AddSecurityDefinition(name: "Bearer", apiScheme);


                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });

            //identity service
            services.AddScoped<IIdentityService, IdentityService>();

            //origin URLS
            var originURLSettings = configuration.GetSection("OriginNames").Get<string[]>();

            //add cors
            services.AddCors(x => x.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins(originURLSettings)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
            }));
        }
    }
}
