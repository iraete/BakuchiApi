using System.Data.SqlClient;
using System.Reflection;
using BakuchiApi.Middleware;
using BakuchiApi.Services;
using BakuchiApi.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BakuchiApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment runEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            runEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (runEnvironment.IsDevelopment())
            {
                var connectionString = GetConnectionString();
                services.AddDbContext<BakuchiContext>(options =>
                    options.UseNpgsql(connectionString));
            }

            // Service-layer Model services
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IPoolService, PoolService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWagerService, WagerService>();
            services.AddScoped<IOutcomeService, OutcomeService>();
            services.AddScoped<IResultService, ResultService>();

            // Other service-layer services
            // services.AddScoped<IEconomyService, EconomyService>();

            // Add controllers and validation
            services.AddControllers()
                .AddFluentValidation(
                    fv =>
                        fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddNewtonsoftJson();
            
            // AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthorization();

            app.UseExceptionHandler(
                errorApp => errorApp.UseCustomExceptionHandler());

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder();
            var server = Configuration["Development:Database:Server"];
            var password = Configuration["Development:Database:Password"];
            var port = Configuration["Development:Database:Port"];
            var user = Configuration["Development:Database:UserID"];
            var database =
                Configuration["Development:Database:InitialCatalog"];

            return
                string.Format(
                    "Server={0};Username={1};Database={2};Port={3};" +
                    "Password={4};SSLMode=Prefer",
                    server,
                    user,
                    database,
                    port,
                    password);
        }
    }
}