using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminConsole.Logic;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminConsole.Controllers
{
    [Route("api/[controller]")]
    public class ComputeApiController : Controller
    {
        private readonly IPreProcessor _preProcessor;

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string[] purchasedItems)
        {
            var dictItemAndCount = _preProcessor.Process(purchasedItems);


        }
    }
}
