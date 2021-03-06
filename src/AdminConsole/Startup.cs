﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminConsole.Dtos;
using AdminConsole.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Caching.Memory;
using AdminConsole.Extensions;
using AdminConsole.Logic;
using AdminConsole.ViewModels;
using AutoMapper;

namespace AdminConsole
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            // Add EF services to the services container.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<MarketDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:default:ConnectionString"]));

            //Add ConsoleLog
            services.AddLogging();

            //Add InMemoryCache
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddCaching();

            //Add InMemorySession
            services.AddSession();

            services.AddMvc();

            services.AddSingleton(sp =>
            {
                return new MapperConfiguration(cfg =>
                {
                    VmMapper.Config(cfg);
                    DtoMapper.Config(cfg);
                });
            });
            services.AddSingleton(sp =>
                sp.GetRequiredService<MapperConfiguration>().CreateMapper());

            services.AddTransient<IPreProcessor, DefaultPreProcessor>();
            services.AddTransient<IMoneyComputer, DefaultMoneyComputer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.ApplicationServices.CreateDb<MarketDbContext>(
                SampleData.Create);

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
