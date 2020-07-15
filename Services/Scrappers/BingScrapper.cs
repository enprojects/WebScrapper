using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using HtmlAgilityPack;
using System.IO;
using System.Web;
using Core.DbEntities;

namespace Services.Scrappers
{
    public class BingScrapper : IScrapper
    {
        private readonly IHtmlDocumentHandler _docHandler;
        private const string URL = "https://www.bing.com/search?q=";
        public BingScrapper(IHtmlDocumentHandler docHandler)
        {
             _docHandler=docHandler;
        }

        public async Task<IEnumerable<QueryResultModel>> GetSerachResult(string query)
        {
            IEnumerable<QueryResultModel> searchResults = null;
            var document = await _docHandler.GetDocument($"{URL}{query}");

            var nodes = document.DocumentNode.SelectNodes("//*[@id='b_results']//li//h2//a");
            if (nodes != null && nodes.Count > 0)
            {
                searchResults = nodes.Select(n => new QueryResultModel
                {
                    Title = n.InnerText,
                    EnteredDate = DateTime.Now,
                    SearchEngine = Core.SearchEngine.Bing.ToString(),
                    TermSearch = query
                });
            }

            return searchResults;
        }
    }
}
