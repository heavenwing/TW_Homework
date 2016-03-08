using System;
using AdminConsole;
using AdminConsole.Dtos;
using AdminConsole.Models;
using AdminConsole.ViewModels;
using AutoMapper;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using AdminConsole.Extensions;

namespace AdminConsoleTest
{
    public class TestFixture
    {
        public TestFixture()
        {
            var services = new ServiceCollection();

            services
                .AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<MarketDbContext>(options => options.UseInMemoryDatabase());

            services.AddSingleton(sp =>
            {
                return new MapperConfiguration(cfg =>
                {
                    VmMapper.Config(cfg);
                    DtoMapper.Config(cfg);
                });
            });
            services.AddSingleton(sp => 
                sp.GetRequiredService<MapperConfiguration>().CreateMapper());

            ServiceProvider = services.BuildServiceProvider();

            ServiceProvider.CreateDb<MarketDbContext>(
                SampleData.Create);
        }

        public IServiceProvider ServiceProvider { get; set; }

    }

    [CollectionDefinition("Fixture")]
    public class FixtureCollection : ICollectionFixture<TestFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
