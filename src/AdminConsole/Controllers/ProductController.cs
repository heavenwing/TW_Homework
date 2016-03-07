using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminConsole.Models;
using AdminConsole.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminConsole.Controllers
{
    public class ProductController : Controller
    {
        private readonly MarketDbContext _db;
        private readonly IMapper _mapper;

        public ProductController(MarketDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var products = await _db.Products
                .Include(o => o.Promotions).ThenInclude(o => o.Promotion)
                .ToListAsync();

            var model = _mapper.Map<List<ProductVm>>(products);

            return View(model);
        }

        public IActionResult Edit()
        {
            throw new NotImplementedException();
        }

        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}
