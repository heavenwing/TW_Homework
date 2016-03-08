using System.Collections.Generic;

namespace AdminConsole.Logic
{
    public interface IPreProcessor
    {
        Dictionary<string, decimal> Process(string[] rawItems);
    }
}