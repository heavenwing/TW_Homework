using System.Collections.Generic;
using System.Threading.Tasks;
using AdminConsole.Dtos;

namespace AdminConsole.Logic
{
    public interface IMoneyComputer
    {
        Task<ComputeResultDto> ComputeAsync(Dictionary<string, decimal> input);
    }
}