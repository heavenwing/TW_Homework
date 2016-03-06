using System.Threading.Tasks;
using AdminConsole.Models;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Entity;

namespace AdminConsoleTest
{
    [Collection("DbCollection")]
    public class MarketDbContextTest
    {
        private DatabaseFixture _fixture;

        public MarketDbContextTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task EnsureDataModelIsValid()
        {
            var db = _fixture.ServiceProvider.GetRequiredService<MarketDbContext>();

            var products =await db.Products.ToListAsync();

            Assert.Equal(0, products.Count);
        }
    }
}
