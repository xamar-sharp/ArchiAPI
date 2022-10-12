using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ArchiAPI.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ArchiAPI.Services;
using Microsoft.Extensions.Logging;
using ArchiAPI.Commands;
using System.Text;
using ArchiAPI.Models;
using Microsoft.IdentityModel.Tokens;
namespace ArchiAPI
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
            services.AddScoped<CreateUserCommand>();
            services.AddScoped<CreateMessageCommand>();
            services.AddScoped<CreateStoreCommand>();
            services.AddScoped<CreateTokenCommand>();
            services.AddScoped<RemoveUserCommand>();
            services.AddScoped<RemoveStoreCommand>();
            services.AddScoped<RemoveMessageCommand>();
            services.AddScoped<UpdateStoreCommand>();
            services.AddScoped<UpdateTokenCommand>();
            services.AddScoped<UpdateUserCommand>();
            services.AddScoped<GenericUserQuery>();
            services.AddScoped<GenericStoreQuery>();
            services.AddScoped<GenericMessageQuery>();
            services.AddSingleton<IJwtBuilder, JwtBuilder>();
            services.AddSingleton<ILoggerProvider, TextFileLoggerProvider>(sp=>new TextFileLoggerProvider(Configuration["LogPath"]));
            services.AddSingleton<ILoggerWrapper, LoggingWrapper>();
            services.AddSingleton<IRefreshBuilder, RefreshGenerator>();
            services.AddSingleton<IPathProvider, PathProvider>();
            services.AddMemoryCache();
            services.AddAuthentication("Bearer").AddJwtBearer((opt) =>
            {
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["Issuer"],
                    ValidAudience = Configuration["Audience"],
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Key").Value)),
                    ValidateIssuerSigningKey = true
                };
            });
            services.AddDbContext<Repository>(opt => opt.UseSqlServer(Configuration.GetSection("NetString").Value));
            services.Configure<Config>(Configuration);
            services.AddResponseCompression(opt =>
            {
                opt.EnableForHttps = false;
                opt.Providers.Add<BrotliCompressionProvider>();
                opt.Providers.Add<GzipCompressionProvider>();
            });
            services.AddControllers().AddXmlSerializerFormatters().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseResponseCompression();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
