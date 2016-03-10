using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckoutConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminConsole.Dtos;
using AdminConsole;
using System.IO;

namespace CheckoutConsole.Tests
{
    [TestClass()]
    public class DefaultResultPrinterTests
    {
        [TestMethod()]
        public void PrintWhenOnlyBuyTwo()
        {
            var input = new ComputeResultDto
            {
                Products =
                {
                    new ProductDto
                    {
                        Name="可口可乐",
                        Count=3,
                        Unit="瓶",
                        Price=3,
                        SubTotal=6,
                        SavingCount=1,
                        SavingMoney=3,
                        Promotions=new List<Guid> {PromotionConsts.PromotionId_BuyTwo }
                    },
                    new ProductDto
                    {
                        Name="羽毛球",
                        Count=5,
                        Unit="个",
                        Price=1,
                        SubTotal=4,
                        SavingCount=1,
                        SavingMoney=1,
                        Promotions=new List<Guid> {PromotionConsts.PromotionId_BuyTwo }
                    },
                    new ProductDto
                    {
                        Name="苹果",
                        Count=2,
                        Unit="斤",
                        Price=5.5m,
                        SubTotal=11
                    }
                },
                Total = 21,
                Saving = 4
            };

            var printer = new DefaultResultPrinter();
            var writer = new StringWriter();
            printer.Print(input, writer);
            var output = writer.ToString();

            Assert.IsTrue(output.IndexOf("买二赠一") > 0);
            Assert.IsTrue(output.IndexOf("节省") > 0);
            Assert.IsTrue(output.IndexOf("节省") == output.LastIndexOf("节省"));
        }

        [TestMethod()]
        public void PrintNonPromotion()
        {
            var input = new ComputeResultDto
            {
                Products =
                {
                    new ProductDto
                    {
                        Name="可口可乐",
                        Count=3,
                        Unit="瓶",
                        Price=3,
                        SubTotal=9
                    },
                    new ProductDto
                    {
                        Name="羽毛球",
                        Count=5,
                        Unit="个",
                        Price=1,
                        SubTotal=5
                    },
                    new ProductDto
                    {
                        Name="苹果",
                        Count=2,
                        Unit="斤",
                        Price=5.5m,
                        SubTotal=11
                    }
                },
                Total = 25
            };

            var printer = new DefaultResultPrinter();
            var writer = new StringWriter();
            printer.Print(input, writer);
            var output = writer.ToString();

            Assert.AreEqual(-1, output.IndexOf("买二赠一"));
            Assert.AreEqual(-1, output.IndexOf("节省"));
        }

        [TestMethod()]
        public void PrintWhenOnly95Off()
        {
            var input = new ComputeResultDto
            {
                Products =
                {
                    new ProductDto
                    {
                        Name="可口可乐",
                        Count=3,
                        Unit="瓶",
                        Price=3,
                        SubTotal=9,
                    },
                    new ProductDto
                    {
                        Name="羽毛球",
                        Count=5,
                        Unit="个",
                        Price=1,
                        SubTotal=5,
                    },
                    new ProductDto
                    {
                        Name="苹果",
                        Count=2,
                        Unit="斤",
                        Price=5.5m,
                        SubTotal=10.45m,
                        SavingMoney=0.55m,
                        Promotions=new List<Guid> {PromotionConsts.PromotionId_95Off}
                    }
                },
                Total = 24.45m,
                Saving = 0.55m
            };

            var printer = new DefaultResultPrinter();
            var writer = new StringWriter();
            printer.Print(input, writer);
            var output = writer.ToString();

            Assert.AreEqual(-1, output.IndexOf("买二赠一"));
            Assert.IsTrue(output.IndexOf("节省") != output.LastIndexOf("节省"));
        }

        [TestMethod()]
        public void PrintWhenBothPromotion()
        {
            var input = new ComputeResultDto
            {
                Products =
                {
                    new ProductDto
                    {
                        Name="可口可乐",
                        Count=3,
                        Unit="瓶",
                        Price=3,
                        SubTotal=6,
                        SavingCount=1,
                        SavingMoney=3,
                        Promotions=new List<Guid> {PromotionConsts.PromotionId_BuyTwo }
                    },
                    new ProductDto
                    {
                        Name="羽毛球",
                        Count=6,
                        Unit="个",
                        Price=1,
                        SubTotal=4,
                        SavingCount=2,
                        SavingMoney=2,
                        Promotions=new List<Guid> {PromotionConsts.PromotionId_BuyTwo }
                    },
                    new ProductDto
                    {
                        Name="苹果",
                        Count=2,
                        Unit="斤",
                        Price=5.5m,
                        SubTotal=10.45m,
                        SavingMoney=0.55m,
                        Promotions=new List<Guid> {PromotionConsts.PromotionId_95Off}
                    }
                },
                Total = 20.45m,
                Saving = 5.55m
            };

            var printer = new DefaultResultPrinter();
            var writer = new StringWriter();
            printer.Print(input, writer);
            var output = writer.ToString();

            Assert.IsTrue(output.IndexOf("买二赠一") > 0);
            Assert.IsTrue(output.IndexOf("节省") > 0);
            Assert.IsTrue(output.IndexOf("节省") != output.LastIndexOf("节省"));
        }
    }
}