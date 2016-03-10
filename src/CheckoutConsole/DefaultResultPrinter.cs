using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminConsole.Dtos;

namespace CheckoutConsole
{
    public class DefaultResultPrinter : IResultPrinter
    {
        public void Print(ComputeResultDto result, TextWriter writer)
        {
            PrintHeader(writer);
            PrintDetails(result, writer);
            PrinterDivider(writer);
            PrintFooter(writer);
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
            writer.WriteLine($"名称：{product.Name}，数量：{product.Count}{product.Unit}，单价：{product.Price.ToString("c")}(元)，小计：{product.SubTotal.ToString("c")}(元)");
            //if (product.SavingMoney)
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
