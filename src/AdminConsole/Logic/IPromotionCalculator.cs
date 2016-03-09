using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminConsole.Dtos;
using AdminConsole.Models;

namespace AdminConsole.Logic
{
    public interface IPromotionCalculator
    {
        void Compute(Product product, ProductDto dto, ComputeResultDto result);
    }
}
