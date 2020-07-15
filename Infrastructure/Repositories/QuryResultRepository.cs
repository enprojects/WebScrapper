using Core.DbEntities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class QuryResultRepository : IQuryResultRepository
    {
        private readonly DbContextOptions<WebScrpperContext> _options;

        //  private readonly WebScrpperContext _context;


        public QuryResultRepository( DbContextOptions<WebScrpperContext> options)
        {
            _options = options;
        }

        public async Task<IEnumerable<QueryResultModel>> GetQueryResultResult()
        {

            using (var ctx = new WebScrpperContext(_options))
            {
                return await ctx.QueryResults.ToListAsync();
            }
        }
        private async Task<bool> checkIfRecordsExist(string term, string searchEngine)
        {
            using (var ctx = new WebScrpperContext(_options))
            {
                var result = await ctx.QueryResults.ToListAsync();

                if (result != null && result.Count > 0)
                {
                    return result.Any(qr => qr.TermSearch == term && qr.SearchEngine == searchEngine);
                }

                return false;
            }            
        }

        public async Task AddSearchResult(IEnumerable<QueryResultModel> searchResults, string searchEngine, string term)
        {
            using (var ctx = new WebScrpperContext(_options))
            {
                var isRecoedsExist = await checkIfRecordsExist(term, searchEngine);
                if (!isRecoedsExist && searchResults!=null && searchResults.Count()>0)
                {                   
                    ctx.QueryResults.AddRange(searchResults);
                    await ctx.SaveChangesAsync();
                }
            }
            
        }

    }
}
