using Core;
using Core.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Core.DbEntities;

namespace Services
{
    public class ScrapperServiceManager : IScrapperServiceManager
    {

        private readonly IQuryResultRepository _repo;   
        private readonly IServiceProvider _serviceProvider;
        private readonly ICacheService _cache;

        public ScrapperServiceManager(IQuryResultRepository repo, 
                               IServiceProvider serviceProvider,
                               ICacheService cache)
        {
            _repo = repo;
            _serviceProvider = serviceProvider;
            _cache = cache;
        }

        public async Task<IEnumerable<QueryResponse>> GetQueryResult(string term)
        {
            var response = new List<QueryResponse>();

            try
            {
                var bingTask = getQueryResultFromEngine(SearchEngine.Bing, term);
                var googleTask = getQueryResultFromEngine(SearchEngine.Google, term);

                var result = await Task.WhenAll(bingTask, googleTask);
                if (result.All(x => x != null))
                {

                    var keyValuePair = result.ToList().SelectMany(s => s).GroupBy(x => x.SearchEngine);
                    foreach (var item in keyValuePair)
                    {
                        response.AddRange(item.Take(5).ToList());
                    }
                }

                return response.AsEnumerable();
            }
            catch (Exception ex)
            {

                return response;
            }
        }

        private async Task<IEnumerable<QueryResponse>> getQueryResultFromEngine(SearchEngine engine, string term)
        {
            term = term.ToLower();
            var key = $"{engine}_{term}";

            var result =  await _cache.GetOrCreateItemAsync<IEnumerable<QueryResultModel>>(key,  () => {
                {
                    var service = _serviceProvider.GetServices<IScrapper>().First(s => GetService(s, engine));
                    return  service.GetSerachResult(term);
                }
            });

          
            if (result != null)
            { 
                 await _repo.AddSearchResult(result, engine.ToString(), term);

            
                return result.Select(r => new QueryResponse
                {
                    Id = r.Id,
                    EnteredDate = r.EnteredDate,
                    SearchEngine = r.SearchEngine,
                    TermSearch = r.TermSearch,
                    Title = r.Title
                });
            }
            return null;
        }

        private bool GetService(IScrapper serv, SearchEngine engine)
        {
            return serv.GetType().Name.StartsWith(engine.ToString());
        }
    }
}
