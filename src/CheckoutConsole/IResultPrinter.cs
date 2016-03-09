using System.IO;
using AdminConsole.Dtos;
using Newtonsoft.Json;

namespace CheckoutConsole
{
    public interface IResultPrinter
    {
        void Print(ComputeResultDto result,TextWriter writer);
    }

    public class RawResultPrinter : IResultPrinter
    {
        public void Print(ComputeResultDto result, TextWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(result));
        }
    }
}