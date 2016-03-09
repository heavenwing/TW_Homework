using System.Collections.Generic;
using System.Threading.Tasks;
using AdminConsole;
using AdminConsole.Controllers;
using AdminConsole.Models;
using AdminConsole.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminConsoleTest.Controllers
{
    [Collection("Fixture")]
    public class ProductControllerTest
    {
        private TestFixture _fixture;

        public ProductControllerTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task IndexShouldGetCompleteProductInfo()
        {
            await _fixture.DoDbActionInScopedAsync(async (db) =>
            {
                var mapper = _fixture.ServiceProvider.GetRequiredService<IMapper>();

                var ctrl = new ProductController(db, mapper);
                var result = await ctrl.Index();

                Assert.NotNull(result);

                var viewResult = Assert.IsType<ViewResult>(result);

                Assert.NotNull(viewResult.ViewData.Model);

                var products = Assert.IsType<List<ProductVm>>(viewResult.ViewData.Model);
                Assert.Equal(3, products.Count);
                Assert.Equal(2, products.Find(o => o.Id == "ITEM000001").PromotionNames.Count);
            });
        }
    }
}
