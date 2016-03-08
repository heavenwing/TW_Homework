using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminConsole.Logic
{
    public interface IPreProcessor
    {
        Dictionary<string, decimal> Process(string[] rawItems);
    }

    public class DefaultPreProcessor : IPreProcessor
    {
        public Dictionary<string, decimal> Process(string[] rawItems)
        {
            var dict = new Dictionary<string, decimal>();
            Action<string, decimal> setDictValue = (key,value) =>
            {
                if (dict.ContainsKey(key))
                    dict[key] += value;
                else
                    dict[key] = value;
            };
            foreach (var item in rawItems )
            {
                var splitStrArray = item.Split('-');

                setDictValue(splitStrArray[0], splitStrArray.Length > 1 ? decimal.Parse(splitStrArray[1]) : 1);
            }

            return dict;
        }
    }
}
