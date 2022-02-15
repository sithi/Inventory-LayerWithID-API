using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using DatabaseLayer.Dal;
using DatabaseLayer.DBContext;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.DBModel.MongoModel;
using PremiseGlobalLibery;
using Serilog;


namespace AssetInventory
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string m;
        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "",
                        ValidAudience = "",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c)d3fun#!!codemenplaLo"))
                    };
                });

            services.AddHealthChecks();
            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBSettings"));
            services.Configure<DBCollections>(Configuration.GetSection("DBCollections"));

            services.AddLogging(builder =>
            {
                var log = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
                builder.AddSerilog(log, dispose: true);
            });

              m = Configuration.GetConnectionString("Redis");
            services.AddPremiseService(config =>            {
                config.RedisConnectionString = Configuration.GetConnectionString("Redis");      
                config.ConnectionString = Configuration.GetSection("MongoDBSettings").GetValue<string>("ConnectionString");
                config.Database = Configuration.GetSection("MongoDBSettings").GetValue<string>("CoreDatabaseName");
            });
            services.AddSingleton<IDBContext,MongoDBContext>();
            services.AddSingleton<BuildingDal>();
            services.AddSingleton<FloorDal>();
            services.AddSingleton<SuiteDal>();
            services.AddSingleton<ZoneDal>();
            services.AddSingleton<SuiteConvenantDal>();
            services.AddSingleton<LayerDal>();
            services.AddMediatR(typeof(DatabseLayerStartup).Assembly);
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.MapHealthChecks("/healthz");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthz");
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Asset Inventory");
                });
            });
        }
    }
}
