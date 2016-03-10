using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminConsole.Dtos;
using AdminConsole;

namespace CheckoutConsole
{
    public class DefaultResultPrinter : IResultPrinter
    {
        public void Print(ComputeResultDto result, TextWriter writer)
        {
            PrintHeader(writer);
            PrintDetails(result, writer);
            PrinterDivider(writer);
            if (result.Products.Any(o => o.Promotions.Contains(PromotionConsts.PromotionId_BuyTwo)))
            {
                PrintBuyTwo(result, writer);
                PrinterDivider(writer);
            }
            PrintTotal(result, writer);
            PrintFooter(writer);
        }

        private static void PrintTotal(ComputeResultDto result, TextWriter writer)
        {
            writer.WriteLine($"总计：{result.Total.ToString("c")}(元)");
            if (!result.Saving.Equals(0m))
                writer.WriteLine($"节省：{result.Saving.ToString("c")}(元)");
        }

        private static void PrintBuyTwo(ComputeResultDto result, TextWriter writer)
        {
            writer.WriteLine("买二赠一商品：");
            foreach (var product in result.Products.Where(
                o => o.Promotions.Contains(PromotionConsts.PromotionId_BuyTwo)))
            {
                writer.WriteLine($"名称：{product.Name}\t数量：{product.SavingCount.ToString("n0")}{product.Unit}");
            }
        }

        private static void PrintDetails(ComputeResultDto result, TextWriter writer)
        {
            foreach (var product in result.Products)
            {
                PrintProduct(product, writer);
            }
        }

        private static void PrintProduct(ProductDto product, TextWriter writer)
        {
            var txt = $"名称：{product.Name}\t数量：{product.Count}{product.Unit}\t单价：{product.Price.ToString("c")}(元)\t小计：{product.SubTotal.ToString("c")}(元)";
            if (product.Promotions.Contains(PromotionConsts.PromotionId_95Off))
            {
                txt += $"\t节省{product.SavingMoney.ToString("c")}(元)";
            }
            writer.WriteLine(txt);
        }

        private static void PrinterDivider(TextWriter writer)
        {
            writer.WriteLine("----------------------");
        }

        private static void PrintFooter(TextWriter writer)
        {
            writer.WriteLine("**********************");
        }

        private static void PrintHeader(TextWriter writer)
        {
            writer.WriteLine("***<狂挣钱超市>购物清单***");
        }
    }
}
