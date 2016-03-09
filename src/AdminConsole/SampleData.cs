using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminConsole.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AdminConsole
{
    public class SampleData
    {
        public const string PromotionId_BuyTwo = "a02cacfb-3c66-4c6c-a32c-3cfd6b4f80dd";
        public const string PromotionId_95Off = "6f6717c4-bf7c-4521-a791-ebb6992b9f63";
        public const string PromotionCalculatorType_BuyTwo = "AdminConsole.Logic.PromotionCalculatorForBuyTwo";
        public const string PromotionCalculatorType_95Off = "AdminConsole.Logic.PromotionCalculatorFor95Off";

        public static void Create(MarketDbContext db)
        {
            db.Promotions.Add(new Promotion
            {
                Id = new Guid(PromotionId_BuyTwo),
                Name = "买二赠一",
                CalculatorType = PromotionCalculatorType_BuyTwo,
                IsOverride = true
            });
            db.Promotions.Add(new Promotion
            {
                Id = new Guid(PromotionId_95Off),
                Name = "95折",
                CalculatorType = PromotionCalculatorType_95Off,
                IsOverride = false
            });
            db.SaveChanges();

            db.Products.Add(new Product
            {
                Id = "ITEM000001",
                Name = "羽毛球",
                Price = 1.00m,
                Unit = "个",
                Promotions = new List<ProductPromotion>
                {
                    new ProductPromotion
                    {
                        Id=Guid.NewGuid(),
                        ProductId="ITEM000001",
                        PromotionId=new Guid(PromotionId_BuyTwo)
                    },
                    new ProductPromotion
                    {
                        Id=Guid.NewGuid(),
                        ProductId="ITEM000001",
                        PromotionId=new Guid(PromotionId_95Off)
                    }
                }
            });
            db.Products.Add(new Product
            {
                Id = "ITEM000003",
                Name = "苹果",
                Price = 5.50m,
                Unit = "斤",
                Promotions = new List<ProductPromotion>
                {
                    new ProductPromotion
                    {
                        Id=Guid.NewGuid(),
                        ProductId="ITEM000003",
                        PromotionId=new Guid(PromotionId_95Off)
                    }
                }
            });
            db.Products.Add(new Product
            {
                Id = "ITEM000005",
                Name = "可口可乐",
                Price = 3.00m,
                Unit = "瓶",
                Promotions = new List<ProductPromotion>
                {
                    new ProductPromotion
                    {
                        Id=Guid.NewGuid(),
                        ProductId="ITEM000005",
                        PromotionId=new Guid(PromotionId_BuyTwo)
                    }
                }
            });

            db.SaveChanges();
        }
    }
}
