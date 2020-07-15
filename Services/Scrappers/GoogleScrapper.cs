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
    public class GoogleScrapper : IScrapper
    {
     
        private const string URL = "https://www.google.com/search?q=";
        private readonly IHtmlDocumentHandler _docHandler;

        public GoogleScrapper(IHtmlDocumentHandler docHandler) 
        {
            _docHandler = docHandler;
        }

        public async Task<IEnumerable<QueryResultModel>> GetSerachResult(string query)
        {
            IEnumerable<QueryResultModel> searchResults = null;
            var document = await _docHandler.GetDocument($"{URL}{query}");

            var nodes = document.DocumentNode.SelectNodes("(//div[contains(@class,'r')]//a//h3//span)");
            if (nodes !=null && nodes.Count>0)
            {
                searchResults = nodes.Select(n => new QueryResultModel
                {
                    Title = n.InnerText,
                    EnteredDate = DateTime.Now,
                    SearchEngine = Core.SearchEngine.Google.ToString(),
                    TermSearch = query
                });                
            }
                   
            return searchResults;
        }
    }
}
