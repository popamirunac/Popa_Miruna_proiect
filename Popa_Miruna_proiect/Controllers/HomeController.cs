using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Popa_Miruna_proiect.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Popa_Miruna_proiect.Data;
using Popa_Miruna_proiect.Models.SpotifyViewModels;

namespace Popa_Miruna_proiect.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpotifyContext _context; 
        public HomeController(SpotifyContext context) 
        {
            _context = context; 
        }
        public async Task<ActionResult> Statistics() 
        {
            IQueryable<OrderGroup> data = 
                from order in _context.Orders 
                group order by order.OrderDate into dateGroup 
                select new OrderGroup() 
                {
                    OrderDate = dateGroup.Key, 
                    SongCount = dateGroup.Count() 
                }; 
            return View(await data.AsNoTracking().ToListAsync()); 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Chat() 
        {
            return View(); 
        }
    }
}
