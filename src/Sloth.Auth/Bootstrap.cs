﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Sloth.Auth.AuthProviders;
using Sloth.Auth.Models;
using Sloth.Auth.TokenProvider;
using Sloth.DB.Models;
using System;

namespace Sloth.Auth
{
    public static class Bootstrap
    {
        public static IServiceCollection AddSlothAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authoptions = new SlothAuthenticationOptions();
            configuration.Bind("AuthenticationConfig", authoptions);
            services.TryAddSingleton(authoptions);
            services.Configure<SlothAuthenticationOptions>(configuration.GetSection("AuthenticationConfig"));
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })

                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = authoptions.GetSymmetricSecurityKey(),

                        ValidateIssuer = true,
                        ValidIssuer = authoptions.ApiName,

                        ValidateAudience = true,
                        ValidAudiences = new[] { authoptions.ClientId, authoptions.SwaggerClientId },

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.FromSeconds(5)
                    };
                });

            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<IAuthProvider, NativeAuthProvider>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenProvider, JwtTokenProvider>();
            return services;
        }
    }
}
