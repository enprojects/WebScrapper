using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
 

namespace Core.Interfaces
{
    public interface IScrapper
    {
        Task<IEnumerable<DbEntities.QueryResultModel>> GetSerachResult(string query);
    }
}
