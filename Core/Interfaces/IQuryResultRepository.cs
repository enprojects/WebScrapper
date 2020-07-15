using Core.DbEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace  Core.Interfaces
{
    public interface IQuryResultRepository
    {
        Task AddSearchResult(IEnumerable<QueryResultModel> searchResults, string searchEngine, string term);
        Task<IEnumerable<QueryResultModel>> GetQueryResultResult();
    }
}