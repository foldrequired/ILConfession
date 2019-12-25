using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ILConfessions.API.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using ILConfessions.API.SwaggerSettings;
using ILConfessions.API.Repositories.V1;
using ILConfessions.API.Settings.JwtSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using ILConfessions.API.Filters;
using System.Reflection;
using System.IO;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using ILConfessions.API.Helpers;
using ILConfessions.API.Models;

namespace ILConfessions.API
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddCors();

            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));

            services.Configure<IdentityOptions>(options => 
            {
               options.Password.RequireDigit = true;
		       options.Password.RequiredLength = 8;
	    	   options.Password.RequireNonAlphanumeric = false;
		       options.Password.RequireUppercase = true;
		       options.Password.RequireLowercase = false;
            });

            services.AddAutoMapper(typeof(Startup)/*Assembly*/);

            #region Authentication / JWT

            var jwtOptions = new JwtOptions();
            Configuration.Bind(nameof(jwtOptions), jwtOptions);
            services.AddSingleton(jwtOptions);

            // Token validation settings
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.TokenSecret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddSingleton(tokenValidationParams);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwt =>
                {
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = tokenValidationParams;
                });

            #endregion

            #region Authorization

            services.AddAuthorization();

            #endregion

            #region Repos SINGLETON/SCOPED/TRANSIENT

            services.AddScoped<IConfessionRepository, ConfessionRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserActivity>();
            services.AddSingleton<IUriRepository>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");

                return new UriRepository(absoluteUri);
            });

            #endregion

            #region Swagger

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Title = "ILConfessions API",
                    Version = "v1"
                });

                s.ExampleFilters();

                // For Authorization Access
                var securityForSwagger = new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[0] }
                };

                s.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization - Bearer",
                    Name = "Authorization",
                    In = "Header",
                    Type = "apiKey"
                });

                s.AddSecurityRequirement(securityForSwagger);

                // Allow to generate XML from comments above the Actions (make sure to add it to the .csproj)
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var err = context.Features.Get<IExceptionHandlerFeature>();
                        if (err != null)
                        {
                            context.Response.AddApplicationError(err.Error.Message);
                            await context.Response.WriteAsync(err.Error.Message);
                        }
                    });
                });
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region Swagger Options

            var swaggerOpts = new SwaggerOpts();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOpts);

            app.UseSwagger(opt =>
            {
                opt.RouteTemplate = swaggerOpts.JsonRoute;
            });

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint(swaggerOpts.UIEndpoint, swaggerOpts.Title);
            });

            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Pagination"));
            app.UseMvc();
        }
    }
}
