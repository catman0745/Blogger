namespace Catman.Blogger.API.Extensions
{
    using System;
    using AutoMapper;
    using Catman.Blogger.API.Auth;
    using Catman.Blogger.API.Data;
    using Catman.Blogger.API.Exceptions;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services)
        {
            var connectionString = GetEnvironmentVariable("BLOGGER_DB_CONNECTION");
            services.AddDbContext<BloggerDbContext>(options => options.UseNpgsql(connectionString));
            
            return services;
        }

        public static IServiceCollection ConfigureAuth(this IServiceCollection services)
        {
            var authOptions = new AuthOptions()
            {
                Issuer = GetEnvironmentVariable("BLOGGER_AUTH_ISSUER"),
                Audience = GetEnvironmentVariable("BLOGGER_AUTH_AUDIENCE"),
                Lifetime = int.Parse(GetEnvironmentVariable("BLOGGER_AUTH_LIFETIME")),
                Key = GetEnvironmentVariable("BLOGGER_AUTH_KEY")
            };
            services
                .AddSingleton(authOptions)
                .AddSingleton<TokenHelper>()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = authOptions.SymmetricSecurityKey,
                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }

        public static IServiceCollection ConfigureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            
            return services;
        }

        private static string GetEnvironmentVariable(string variableName)
        {
            var variable = Environment.GetEnvironmentVariable(variableName);
            if (string.IsNullOrWhiteSpace(variable))
            {
                throw new EnvironmentVariableDoesNotExist(variableName);
            }

            return variable;
        }
    }
}
