using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BakuchiApi.Services;
using BakuchiApi.Services.Interfaces;


namespace BakuchiApi
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            runEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment runEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            if (runEnvironment.IsDevelopment())
            {
                var builder = new SqlConnectionStringBuilder();

                var server = Configuration["Development:Database:Server"];
                var password = Configuration["Development:Database:Password"];
                var port = Configuration["Development:Database:Port"];
                var user = Configuration["Development:Database:UserID"];
                var database = 
                    Configuration["Development:Database:InitialCatalog"];

                var connectionString = ConnectionString(
                    server,
                    user,
                    database,
                    port,
                    password
                );

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
            services.AddScoped<IEconomyService, EconomyService>();

            // Add controllers
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private string ConnectionString(string host, string user, 
            string database, 
            string port,
            string password)
        {
            return
                String.Format(
                    "Server={0};Username={1};Database={2};Port={3};" +
                    "Password={4};SSLMode=Prefer",
                    host,
                    user,
                    database,
                    port,
                    password);
        }
    }
}
