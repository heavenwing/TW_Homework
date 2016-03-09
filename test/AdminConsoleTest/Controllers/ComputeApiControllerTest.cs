using System.Collections.Generic;
using System.Threading.Tasks;
using AdminConsole;
using AdminConsole.Controllers;
using AdminConsole.Dtos;
using AdminConsole.Logic;
using AdminConsole.Models;
using AdminConsole.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminConsoleTest.Controllers
{
    [Collection("Fixture")]
    public class ComputeApiControllerTest
    {
        private TestFixture _fixture;

        public ComputeApiControllerTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task PostShouldGetResult()
        {
            await _fixture.DoDbActionInScopedAsync(async (db) =>
            {
                var preProcessor = new DefaultPreProcessor();
                var moneyComputer = new DefaultMoneyComputer(db);

                var ctrl = new ComputeApiController(preProcessor, moneyComputer);
                var input = new string[]
                {
                    "ITEM000001",
                    "ITEM000001",
                    "ITEM000001",
                    "ITEM000001",
                    "ITEM000001",
                    "ITEM000001",
                    "ITEM000003-2",
                    "ITEM000005",
                    "ITEM000005",
                    "ITEM000005"
                };
                var result = await ctrl.Post(input);

                Assert.NotNull(result);

                var objectResult = Assert.IsType<HttpOkObjectResult>(result);

                Assert.NotNull(objectResult.Value);

                var output = Assert.IsType<ComputeResultDto>(objectResult.Value);

                Assert.NotNull(output);
                Assert.Equal(3, output.Products.Count);

                Assert.Equal(4m, output.Products[0].SubTotal);
                Assert.Equal(2m, output.Products[0].SavingCount);

                Assert.Equal(10.45m, output.Products[1].SubTotal);
                Assert.Equal(0.55m, output.Products[1].SavingMoney);

                Assert.Equal(6m, output.Products[2].SubTotal);
                Assert.Equal(1m, output.Products[2].SavingCount);

                Assert.Equal(20.45m, output.Total);
                Assert.Equal(5.55m, output.Saving);
            });
        }
    }
}
