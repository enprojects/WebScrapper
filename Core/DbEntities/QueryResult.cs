using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DbEntities
{
    public class QueryResultModel
    {
        public int Id { get; set; }
        public string SearchEngine { get; set; }
        public DateTime EnteredDate { get; set; }
        public string TermSearch { get; set; }
        public string  Title { get; set; }
    }
}
