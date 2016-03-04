using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminConsole.Models
{
    public class ProductPromotion
    {
        public Guid Id { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public Guid PromotionId { get; set; }

        public Promotion Promotion { get; set; }
    }
}
