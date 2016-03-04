using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminConsole.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AdminConsole
{
    public class SampleData
    {
        public static async Task CreateDemoDataAsync(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<MarketDbContext>();

            await db.SaveChangesAsync();
        }
    }
}
