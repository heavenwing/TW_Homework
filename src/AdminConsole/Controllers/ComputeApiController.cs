using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminConsole.Dtos;
using AdminConsole.Logic;
using AdminConsole.Models;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminConsole.Controllers
{
    [Route("api/[controller]")]
    public class ComputeApiController : Controller
    {
        private readonly IMoneyComputer _moneyComputer;
        private readonly IPreProcessor _preProcessor;

        public ComputeApiController(IPreProcessor preProcessor, IMoneyComputer moneyComputer)
        {
            _preProcessor = preProcessor;
            _moneyComputer = moneyComputer;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string[] purchasedItems)
        {
            var dictItemAndCount = _preProcessor.Process(purchasedItems);

            var dto = await _moneyComputer.ComputeAsync(dictItemAndCount);

            return Ok(dto);
        }
    }
}
