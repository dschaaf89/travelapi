using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelApi.Helpers;
using TravelApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using TravelApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelApi
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
            
            services.AddDbContext<TravelApiContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // services.AddSwaggerDocument(config =>
            // {
            //     config.PostProcess = document =>
            //     {
            //         document.Info.Version = "v1";
            //         document.Info.Title = "ToDo API";
            //         document.Info.Description = "A simple ASP.NET Core web API";
            //         document.Info.TermsOfService = "None";
            //         document.Info.Contact = new NSwag.OpenApiContact
            //         {
            //             Name = "Shayne Boyer",
            //             Email = string.Empty,
            //             Url = "https://twitter.com/spboyer"
            //         };
            //         document.Info.License = new NSwag.OpenApiLicense
            //         {
            //             Name = "Use under LICX",
            //             Url = "localhost:4000"
            //         };
            //     };
            // });

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // services.AddApiVersioning(o => {
            // o.ReportApiVersions = true;
            // o.AssumeDefaultVersionWhenUnspecified = true;
            // o.DefaultApiVersion = new ApiVersion(1, 0);
            // });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}