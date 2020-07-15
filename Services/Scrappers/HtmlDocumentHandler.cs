using Core.DbEntities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Scrappers
{
    public class HtmlDocumentHandler : IHtmlDocumentHandler
    {
        private readonly HttpClient _httpClient;
        public HtmlDocumentHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HtmlDocument> GetDocument(string url)
        {
            var document = new HtmlDocument();
            var writer = new StringWriter();

            var result = await _httpClient.GetAsync(url);
            var pageContents = await result.Content.ReadAsStringAsync();


            HttpUtility.HtmlDecode(pageContents, writer);
            string decodedContetnt = writer.ToString();
            document.LoadHtml(decodedContetnt);

            return document;
        }
    }
}

