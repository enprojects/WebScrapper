using HtmlAgilityPack;
using System.Threading.Tasks;

namespace Services.Scrappers
{
    public interface IHtmlDocumentHandler
    {
        Task<HtmlDocument> GetDocument(string url);
    }
}