using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace AdminConsole.Models
{
    public class MarketDbContext : DbContext
    {
        public DbSet<Product > Products { get; set; }

        public DbSet<Promotion> Promotions { get; set; }
    }
}
