using System;
using AdminConsole.Models;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminConsoleTest
{
    public class DatabaseFixture
    {
        public DatabaseFixture()
        {
            var services = new ServiceCollection();

            services
                .AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<MarketDbContext>(options => options.UseInMemoryDatabase());

            ServiceProvider = services.BuildServiceProvider();
        }

        public IServiceProvider ServiceProvider { get; set; }

    }

    [CollectionDefinition("DbCollection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
