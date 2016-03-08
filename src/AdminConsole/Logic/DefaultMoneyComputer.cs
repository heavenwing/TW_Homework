using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminConsole.Dtos;
using AdminConsole.Models;
using Microsoft.Data.Entity;

namespace AdminConsole.Logic
{
    public class DefaultMoneyComputer : IMoneyComputer
    {
        private readonly MarketDbContext _db;

        public DefaultMoneyComputer(MarketDbContext db)
        {
            _db = db;
        }

        public async Task<ComputeResultDto> ComputeAsync(Dictionary<string, decimal> input)
        {
            var result = new ComputeResultDto();

            foreach (var kvp in input)
            {
                var product = await _db.Products
                    .Include(o => o.Promotions).ThenInclude(o => o.Promotion)
                    .SingleOrDefaultAsync(o => o.Id == kvp.Key);
                if (product != null)
                {
                    var dto = CreateDto(kvp, product);

                    ComputePromotion(product, dto);

                    result.Products.Add(dto);
                }
            }

            return result;
        }

        private static ProductDto CreateDto(KeyValuePair<string, decimal> kvp, Product product)
        {
            return new ProductDto
            {
                Name = product.Name,
                Count = kvp.Value,
                Price = product.Price,
                Unit = product.Unit,
                SubTotal = product.Price * kvp.Value
            };
        }

        private void ComputePromotion(Product product, ProductDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
