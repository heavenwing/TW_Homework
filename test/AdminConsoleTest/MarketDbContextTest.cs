using System.Threading.Tasks;
using AdminConsole;
using AdminConsole.Models;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Entity;
using AdminConsole.Extensions;
using System.Linq;

namespace AdminConsoleTest
{
    [Collection("Fixture")]
    public class MarketDbContextTest
    {
        private TestFixture _fixture;

        public MarketDbContextTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task DataModelShouldValidAndSampleDataShouldCreated()
        {
            using (var db = _fixture.ServiceProvider.GetService<MarketDbContext>())
            {
                var products = await db.Products.ToListAsync();

                Assert.Equal(3, products.Count);

                var promotions = await db.Promotions.ToListAsync();

                Assert.Equal(2, promotions.Count);
            }
        }
    }
}
