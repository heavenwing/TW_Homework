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
using AdminConsole;
using System;

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
        public async Task ComputeTwoPromotionUsingSampleData()
        {
            await _fixture.DoDbActionInScopedAsync(async (db) =>
            {
                var input = new Dictionary<string, decimal>
                {
                    ["ITEM000001"] = 6, //羽毛球
                    ["ITEM000003"] = 2, //苹果
                    ["ITEM000005"] = 3 //可口可乐
                };

                var computer = new DefaultMoneyComputer(db);
                var output = await computer.ComputeAsync(input);

                Assert.NotNull(output);
                Assert.Equal(3, output.Products.Count);

                Assert.Equal(4m, output.Products[0].SubTotal);
                Assert.Equal(2m, output.Products[0].SavingCount);
                Assert.Equal(1, output.Products[0].Promotions.Count);
                Assert.Equal(new Guid(PromotionConsts.PromotionId_BuyTwo), output.Products[0].Promotions[0]);

                Assert.Equal(10.45m, output.Products[1].SubTotal);
                Assert.Equal(0.55m, output.Products[1].SavingMoney);
                Assert.Equal(1, output.Products[1].Promotions.Count);
                Assert.Equal(new Guid(PromotionConsts.PromotionId_95Off), output.Products[1].Promotions[0]);

                Assert.Equal(6m, output.Products[2].SubTotal);
                Assert.Equal(1m, output.Products[2].SavingCount);
                Assert.Equal(1, output.Products[2].Promotions.Count);
                Assert.Equal(new Guid(PromotionConsts.PromotionId_BuyTwo), output.Products[2].Promotions[0]);

                Assert.Equal(20.45m, output.Total);
                Assert.Equal(5.55m, output.Saving);
            });
        }
    }
}
