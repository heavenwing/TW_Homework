using System.Collections.Generic;
using System.Threading.Tasks;
using AdminConsole.Controllers;
using AdminConsole.Logic;
using AdminConsole.Models;
using AdminConsole.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminConsoleTest.Logic
{
    [Collection("Fixture")]
    public class DefaultMoneyComputerTest
    {
        private TestFixture _fixture;

        public DefaultMoneyComputerTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ComputeTwoPromotion()
        {
            using (var db = _fixture.ServiceProvider.GetRequiredService<MarketDbContext>())
            {
                var input = new Dictionary<string, decimal>
                {
                    ["ITEM000001"] = 5,
                    ["ITEM000003"] = 2,
                    ["ITEM000005"] = 3
                };

                var computer = new DefaultMoneyComputer(db);
                var output = await computer.ComputeAsync(input);

                Assert.NotNull(output);
                Assert.Equal(3, output.Products.Count);
            }
        }
    }
}
