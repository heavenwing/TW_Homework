using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminConsole.Models;
using AdminConsole.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace AdminConsole.Controllers
{
    public class PromotionController:Controller
    {
        private readonly MarketDbContext _db;
        private readonly IMapper _mapper;

        public PromotionController(MarketDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var promotions = await _db.Promotions
                .ToListAsync();

            var model = _mapper.Map<List<PromotionVm>>(promotions);

            return View(model);
        }
    }
}
