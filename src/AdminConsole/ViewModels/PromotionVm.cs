using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminConsole.ViewModels
{
    public class PromotionVm
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsOverride { get; set; }
    }
}
