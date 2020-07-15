using Core.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class WebScrpperContext : DbContext
    {
        public WebScrpperContext(DbContextOptions<WebScrpperContext> options) 
            :base(options)
        {

        }


       
        public DbSet<QueryResultModel> QueryResults { get; set; }
    }
}
