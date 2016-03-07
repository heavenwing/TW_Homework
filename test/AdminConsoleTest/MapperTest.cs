using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminConsole.Models;
using AdminConsole.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Xunit;

namespace AdminConsoleTest
{
    [Collection("Fixture")]
    public class MapperTest
    {
        private TestFixture _fixture;

        public MapperTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ProductToViewModel()
        {
            var source = new Product
            {
                Id = "item123",
                Name = "abc",
                Price = (decimal) 345.67,
                Unit = "pice",
                Promotions = new List<ProductPromotion>()
            };

            var mapper = _fixture.ServiceProvider.GetRequiredService<IMapper>();
            var dest = mapper.Map<ProductVm>(source);

            Assert.NotNull(dest);
            Assert.Equal("abc", dest.Name);
            Assert.Equal(0,dest.PromotionNames?.Count);
        }
    }
}
