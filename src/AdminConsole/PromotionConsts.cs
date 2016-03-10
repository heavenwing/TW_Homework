using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminConsole
{
    public class PromotionConsts
    {
        public readonly static Guid PromotionId_BuyTwo = new Guid("a02cacfb-3c66-4c6c-a32c-3cfd6b4f80dd");
        public readonly static Guid PromotionId_95Off = new Guid ("6f6717c4-bf7c-4521-a791-ebb6992b9f63");
        public const string PromotionCalculatorType_BuyTwo = "AdminConsole.Logic.PromotionCalculatorForBuyTwo";
        public const string PromotionCalculatorType_95Off = "AdminConsole.Logic.PromotionCalculatorFor95Off";
    }
}
