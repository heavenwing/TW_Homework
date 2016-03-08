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
    public class VmMapperTest
    {
        private TestFixture _fixture;

        public VmMapperTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ProductToViewModelNameShouldMap()
        {
            var source = new Product
            {
                Id = "item123",
                Name = "abc",
                Price = (decimal)345.67,
                Unit = "pice",
                Promotions = new List<ProductPromotion>()
            };

            var mapper = _fixture.ServiceProvider.GetRequiredService<IMapper>();
            var dest = mapper.Map<ProductVm>(source);

            Assert.NotNull(dest);
            Assert.Equal("abc", dest.Name);
            Assert.NotNull(dest.PromotionNames);
            Assert.Equal(0, dest.PromotionNames?.Count);
        }

        [Fact]
        public void ProductToViewModelPromotionNamesShouldMap()
        {
            var source = new Product
            {
                Promotions = new List<ProductPromotion>
                {
                    new ProductPromotion
                    {
                        Promotion=new Promotion {Name="95%off" }
                    }
                }
            };

            var mapper = _fixture.ServiceProvider.GetRequiredService<IMapper>();
            var dest = mapper.Map<ProductVm>(source);

            Assert.NotNull(dest);
            Assert.NotNull(dest.PromotionNames);
            Assert.Equal(1, dest.PromotionNames?.Count);
            Assert.Equal("95%off", dest.PromotionNames[0]);
        }

        [Fact]
        public void PromotionToViewModelNameShouldMap()
        {
            var source = new Promotion
            {
                Id = Guid.NewGuid(),
                Name = "95%off",
            };

            var mapper = _fixture.ServiceProvider.GetRequiredService<IMapper>();
            var dest = mapper.Map<PromotionVm>(source);

            Assert.NotNull(dest);
            Assert.Equal("95%off", dest.Name);
        }
    }
}
