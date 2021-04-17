using AspnetCoreSession8.Core.DomainModels.Cache;
using AspnetCoreSession8.Infra.Caching;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreSession8.Endpoints.WebUi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            CacheConfiguration cacheConfig = new CacheConfiguration();
            Configuration.GetSection("cache").Bind(cacheConfig);
            services.AddSingleton(cacheConfig);
            
            services.Configure<RouteOptions>(routeOptions =>
            {
                routeOptions.ConstraintMap.Add("nationalCode", typeof(NationalCodeConstraint));
            });
            services.AddCachingStrategy(cacheConfig);
            services.AddTransient(c => CacheFactory.CreateCache(c));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Register/{nationalCode:nationalCode}", async (context) =>
                {
                    string cacheKey = "nationalCodeList";
                    var cache = context.RequestServices.GetService<ICacheAdapter>();
                    string NationalCode = context.GetRouteValue("nationalCode").ToString();
                    var NationalCodes = await cache.GetAsync<List<string>>(cacheKey);
                    if (NationalCodes!=null && NationalCodes.Contains(NationalCode))
                    {
                        await context.Response.WriteAsync("you have registered with this code before");
                    }
                    else
                    {
                        if (NationalCodes != null)
                        { 
                            NationalCodes.Add(NationalCode);
                            await cache.SetAsync<List<string>>(cacheKey, NationalCodes);
                        }
                        else
                        {
                            await cache.SetAsync<List<string>>(cacheKey, new List<string>() { NationalCode });
                        }
                        await context.Response.WriteAsync($"your registeration with Code {NationalCode} completed sucessfully");
                    }   
                });

                endpoints.MapGet("/RegisteredList", async (context) =>
                {
                    var cache = context.RequestServices.GetService<ICacheAdapter>();
                    var NationalCodes = await cache.GetAsync<List<string>>("nationalCodeList");
                    if (!NationalCodes.Any())
                    {
                        await context.Response.WriteAsync("no one Registerd yet!");
                    }
                    else
                    {
                        foreach (var item in NationalCodes.Select((value, index) => new { index, value }))
                        {
                            await context.Response.WriteAsync($"<b>{item.index + 1}:</b> <span>{item.value}</span><br/>");
                        }

                    }
                });
            });

        }
    }
}
