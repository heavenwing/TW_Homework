using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminConsole;
using AdminConsole.Controllers;
using AdminConsole.Models;
using AdminConsole.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Xunit;
using AdminConsole.Extensions;

namespace AdminConsoleTest
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
            using (var db = _fixture.ServiceProvider.GetRequiredService<MarketDbContext>())
            {
                var mapper = _fixture.ServiceProvider.GetRequiredService<IMapper>();

                var ctrl = new ProductController(db, mapper);
                var result = await ctrl.Index();

                Assert.NotNull(result);

                var viewResult = Assert.IsType<ViewResult>(result);

                Assert.NotNull(viewResult.ViewData.Model);

                var products = Assert.IsType<List<ProductVm>>(viewResult.ViewData.Model);
                Assert.Equal(3, products.Count);
                Assert.Equal(2, products[0].PromotionNames.Count);
            }
        }
    }
}
