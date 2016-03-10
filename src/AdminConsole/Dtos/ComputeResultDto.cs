using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminConsole.Dtos
{
    public class ComputeResultDto
    {
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();

        public decimal Total { get; set; }

        public decimal Saving { get; set; }
    }

    public class ProductDto
    {
        public string Name { get; set; }

        public string Unit { get; set; }

        public decimal Price { get; set; }

        public decimal Count { get; set; }

        public decimal SubTotal { get; set; }

        public decimal  SavingMoney { get; set; }

        public decimal SavingCount { get; set; }

        public List<Guid> Promotions { get; set; } = new List<Guid>();
    }
}
