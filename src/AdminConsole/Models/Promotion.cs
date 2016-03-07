using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminConsole.Models
{
    public class Promotion
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsOverride { get; set; }

        public string  ProcessType { get; set; }

        public List<ProductPromotion> Products { get; set; } = new List<ProductPromotion>();

    }
}
