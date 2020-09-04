namespace Catman.Blogger.API
{
    using System;
    using AutoMapper;
    using Catman.Blogger.API.Auth;
    using Catman.Blogger.API.Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("BLOGGER_DB_CONNECTION");
            services.AddDbContext<BloggerDbContext>(options => options.UseNpgsql(connectionString));
            
            var authOptions = new AuthOptions()
            {
                Issuer = Environment.GetEnvironmentVariable("BLOGGER_AUTH_ISSUER"),
                Audience = Environment.GetEnvironmentVariable("BLOGGER_AUTH_AUDIENCE"),
                Lifetime = int.Parse(Environment.GetEnvironmentVariable("BLOGGER_AUTH_LIFETIME")),
                Key = Environment.GetEnvironmentVariable("BLOGGER_AUTH_KEY")
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
            
            services.AddAutoMapper(typeof(Startup));
            
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
