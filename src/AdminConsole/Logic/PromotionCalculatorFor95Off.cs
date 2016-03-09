using AdminConsole.Dtos;
using AdminConsole.Models;

namespace AdminConsole.Logic
{
    public class PromotionCalculatorFor95Off : IPromotionCalculator
    {
        public void Compute(Product product, ProductDto dto)
        {
            dto.SavingMoney = dto.SubTotal * 0.05m;
            dto.SubTotal = dto.SubTotal - dto.SavingMoney;
        }
    }
}