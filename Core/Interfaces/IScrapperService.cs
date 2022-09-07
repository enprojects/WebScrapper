using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface  IScrapperServiceManager
    {//edited
        Task<IEnumerable<QueryResponse>> GetQueryResult(string term);
    }
}
