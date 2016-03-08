using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class PromotionControllerTest
    {
        private TestFixture _fixture;

        public PromotionControllerTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task IndexShouldGetPromotionInfo()
        {
            using (var db = _fixture.ServiceProvider.GetRequiredService<MarketDbContext>())
            {
                var mapper = _fixture.ServiceProvider.GetRequiredService<IMapper>();

                var ctrl = new PromotionController(db, mapper);
                var result = await ctrl.Index();

                Assert.NotNull(result);

                var viewResult = Assert.IsType<ViewResult>(result);

                Assert.NotNull(viewResult.ViewData.Model);

                var promotions = Assert.IsType<List<PromotionVm>>(viewResult.ViewData.Model);
                Assert.Equal(2, promotions.Count);
            }
        }
    }
}
