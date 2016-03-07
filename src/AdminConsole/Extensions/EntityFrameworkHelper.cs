using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace AdminConsole.Extensions
{
    public static class EntityFrameworkHelper
    {
        public static void CreateDb<T>(this IServiceProvider serviceProvider, params Action<T>[] moreInitials)
            where T : DbContext
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var db = serviceScope.ServiceProvider.GetRequiredService<T>())
                {
                    if (db.Database != null)
                    {
                        try
                        {
                            if (db.Database.EnsureCreated())
                            {
                                foreach (var init in moreInitials)
                                {
                                    init(db);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError(ex.Message);
                            throw;
                        }
                    }
                }
            }
        }

        public static async Task MigrateDbAsync<T>(this IServiceProvider serviceProvider, params Func<IServiceProvider, Task>[] moreInitials)
            where T : DbContext
        {
            using (var db = serviceProvider.GetRequiredService<T>())
            {

                if (db.Database != null)
                {
                    try
                    {
                        //TODO beta7 ApplyMigrations
                        //db.Database.ApplyMigrations();
                        if (moreInitials != null)
                        {
                            foreach (var init in moreInitials)
                            {
                                await init(serviceProvider);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(ex.Message);
                        throw;
                    }
                }
            }
        }

    }

}
