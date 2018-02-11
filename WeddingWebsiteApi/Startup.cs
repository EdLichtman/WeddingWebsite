using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeddingWebsiteApi.Configuration;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using AutoMapper;
using FluentValidation.AspNetCore;
using WeddingWebsiteApi.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WeddingWebsiteApi
{
    public class Startup
    {
        private readonly string _secretKey;
        private readonly SymmetricSecurityKey _signingKey;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _secretKey = Configuration.GetSection("SecretKey").ToString();
            _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));
        }

        public IConfigurationRoot Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabaseServices(services);
            ConfigureClaimsServices(services);
            ConfigureCorsServices(services);
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());



            services.AddSingleton(Configuration);
            services.AddTransient<IDatabaseConfiguration, DefaultDatabaseConfiguration>();
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IDatabaseContainer, DatabaseContainer>();
            services.AddSingleton<IConfiguration>((config) => Configuration);


            services.AddAutoMapper();
        }
        private void ConfigureDatabaseServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("Root"),
                b => b.MigrationsAssembly("WeddingWebsiteApi")));
        }
        private void ConfigureClaimsServices(IServiceCollection services)
        {
            services.AddSingleton<IJwtFactory, JwtFactory>();

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(JwtConstants.ClaimIdentifiers.Rol, JwtConstants.Claims.ApiAccess));
            });
            services.AddIdentity<AppUser, IdentityRole>
                (o =>
                {
                    // configure identity options
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
        private void ConfigureCorsServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();

            }));

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAll");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }

    }
}
