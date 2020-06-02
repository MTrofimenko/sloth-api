using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sloth.Api.Middlewares;
using Sloth.Api.Services;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using Sloth.Api.Configuration.AutoMapper;
using Sloth.Auth;
using Sloth.DB;
using Sloth.Api.Filters;

namespace Sloth.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AddAutoMapper(services);

            services.AddCors();
            services.AddMvc();
            services.AddSlothDbContext(Configuration);
            services.AddSlothRepositories();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info() { Title = "Sloth API", Version = "v1" });
                options.DocumentFilter<SwaggerSecurityRequirementsDocumentFilter>();
                options.AddSecurityDefinition("oauth2", new ApiKeyScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            services.AddTransient<IUserService, UserService>();
            services.AddSlothAuthentication(Configuration);
            AddChatServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseMiddleware<HttpCustomExceptionMiddleware>();
            app.UseForwardedHeaders();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sloth API");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }

        private static void AddChatServices(IServiceCollection services)
        {
            services
                .AddTransient<IChatService, ChatService>()
                .AddTransient<IChatNameResolver, ChatNameResolver>();
        }
        private void AddAutoMapper(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
