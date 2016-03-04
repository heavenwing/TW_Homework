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
        public static async Task CreateDbAsync<T>(this IServiceProvider serviceProvider, params Func<IServiceProvider, Task>[] moreInitials)
            where T : DbContext
        {
            using (var db = serviceProvider.GetRequiredService<T>())
            {

                if (db.Database != null)
                {
                    try
                    {
                        if (await db.Database.EnsureCreatedAsync())
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
