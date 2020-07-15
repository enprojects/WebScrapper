using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class QueryResponse
    {
        public int Id { get; set; }
        public string SearchEngine { get; set; }
        public DateTime EnteredDate { get; set; }
        public string TermSearch { get; set; }
        public string Title { get; set; }
    }
}
