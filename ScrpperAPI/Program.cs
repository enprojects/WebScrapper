using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace ScrpperAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
          var host =CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });



        private static void uploadCahceData(Infrastructure.WebScrpperContext ctx, IMemoryCache cache)
        {


            var data = ctx.QueryResults;

            foreach (var item in data)
            {
                var key = $"{item.SearchEngine}_{item.TermSearch}";
                cache.Set(key, "asasas");
            }
                
            
        }
    }
}
