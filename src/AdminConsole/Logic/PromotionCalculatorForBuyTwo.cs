using System;
using AdminConsole.Dtos;
using AdminConsole.Models;

namespace AdminConsole.Logic
{
    public class PromotionCalculatorForBuyTwo : IPromotionCalculator
    {
        public void Compute(Product product, ProductDto dto)
        {
            dto.SavingCount = (int)(dto.Count / 3);
            dto.SavingMoney = dto.SavingCount.Value * dto.Price;
            dto.SubTotal = dto.SubTotal - dto.SavingMoney;
        }
    }
}