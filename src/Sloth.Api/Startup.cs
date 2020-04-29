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
using Microsoft.AspNetCore.Identity;
using Sloth.DB.Models;
using Sloth.Auth.AuthProviders;
using Sloth.Auth;
using Sloth.DB;
using Sloth.DB.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Sloth.Auth.AuthenticationHandler;
using Sloth.Auth.Models;

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
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors();
            services.AddMvc();
            services.AddSlothDbContext(Configuration);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info() { Title = "Sloth API", Version = "v1" });
                // options.DocumentFilter<SwaggerSecurityRequirementsDocumentFilter>(); TODO: enable it after authorization is done

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            services.Configure<SlothAuthenticationOptions>(Configuration.GetSection("AuthenticationConfig"));
            services.AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, BaseAuthHandler>(JwtBearerDefaults.AuthenticationScheme,
                options => { });
            
            /*
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
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        //ValidIssuer = Configuration["JwtIssuer"],
                        //ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Guid.Empty.ToString())),

                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });*/

            AddChatServices(services);
            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<IAuthProvider, NativeAuthProvider>();
            services.AddTransient<IAuthService, AuthServise>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseForwardedHeaders();
            app.UseMiddleware<HttpCustomExceptionMiddleware>();
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
                .AddTransient<IChatService, ChatService>();
        }
    }
}
