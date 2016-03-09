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
                    var dto = new ProductDto
                    {
                        Name = product.Name,
                        Count = kvp.Value,
                        Price = product.Price,
                        Unit = product.Unit,
                        SubTotal = product.Price * kvp.Value
                    };

                    ComputePromotion(product, dto);

                    result.Products.Add(dto);

                    result.Total += dto.SubTotal;
                    result.Saving += dto.SavingMoney;
                }
            }

            return result;
        }

        private void ComputePromotion(Product product, ProductDto dto)
        {
            if (product.Promotions.Count == 0) return;

            var overridedPromotion = product.Promotions.Find(o => o.Promotion.IsOverride);
            if (overridedPromotion != null && overridedPromotion.Promotion != null)
            {
                RealPromotionCalculate(product, dto, overridedPromotion);
            }
            else
            {
                foreach (var pro in product.Promotions)
                {
                    RealPromotionCalculate(product, dto, pro);
                }
            }
        }

        private static void RealPromotionCalculate(Product product, ProductDto dto,
            ProductPromotion pro)
        {
            var calculator = (IPromotionCalculator)Activator.CreateInstance(
                Type.GetType(pro.Promotion.CalculatorType));
            calculator.Compute(product, dto);
        }
    }
}
