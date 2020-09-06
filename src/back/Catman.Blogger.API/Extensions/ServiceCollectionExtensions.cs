namespace Catman.Blogger.API.Extensions
{
    using System;
    using AutoMapper;
    using Catman.Blogger.API.Exceptions;
    using Catman.Blogger.Core.Helpers.Auth;
    using Catman.Blogger.Core.Helpers.Time;
    using Catman.Blogger.Core.Repositories;
    using Catman.Blogger.Core.Services.Blog;
    using Catman.Blogger.Core.Services.Common;
    using Catman.Blogger.Core.Services.Post;
    using Catman.Blogger.Core.Services.User;
    using Catman.Blogger.Data;
    using Catman.Blogger.Data.Repositories;
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

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IBlogRepository, BlogRepository>()
                .AddScoped<IPostRepository, PostRepository>();

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IBlogService, BlogService>()
                .AddScoped<IPostService, PostService>();

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
                .AddSingleton<IAuthOptions>(authOptions)
                .AddSingleton<ITokenHelper, TokenHelper>()
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

        public static IServiceCollection ConfigureTimeHelper(this IServiceCollection services)
        {
            services.AddSingleton<ITimeHelper, TimeHelper>();

            return services;
        }

        public static IServiceCollection ConfigureMappings(this IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(Startup))  // mappings from API
                .AddAutoMapper(typeof(Service)); // mappings from Core
            
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
