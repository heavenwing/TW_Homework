using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminConsole.ViewModels
{
    public class ProductVm
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Unit { get; set; }

        public decimal Price { get; set; }

        public List<string> PromotionNames { get; set; } = new List<string>();
    }
}
