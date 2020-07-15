using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Services;
using Microsoft.Extensions.Caching.Memory;
using Services.Scrappers;

namespace ScrpperAPI
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
            
            services.AddCors();

            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddTransient<WebScrpperContext>();

            services.AddDbContext<WebScrpperContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),ServiceLifetime.Transient, ServiceLifetime.Transient

            );
            services.AddTransient<IQuryResultRepository, QuryResultRepository>();
           
            services.AddScoped<IScrapperServiceManager, ScrapperServiceManager>();
            services.AddScoped<IScrapper, GoogleScrapper>(); 
            services.AddScoped<IScrapper,BingScrapper>();
            services.AddHttpClient<IHtmlDocumentHandler, HtmlDocumentHandler>();

            services.AddControllers();
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ICacheService cache, WebScrpperContext ctx)
        {

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var keyPairValue = ctx.QueryResults.ToList().GroupBy(g=> new { g.SearchEngine, g.TermSearch});
         
            foreach (var item in keyPairValue)
            {
              cache.GetOrCreateItemAsync($"{item.Key.SearchEngine}_{item.Key.TermSearch}", item.ToList());
            }

        }
    }
}
