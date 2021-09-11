using CustomPaymentFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CustomPaymentFlow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewModel model = new ViewModel();

            return View(model);
        }
        [HttpPost]
        public IActionResult Checkout(ViewModel indexModel)
        {
            var model = from i in new ViewModel().Items
                       where indexModel.Items.Where(i => i.Selected).Select(j => j.Id).Contains(i.Id)
                       select i;

            ViewBag.TotalCost = model.Sum(i => Convert.ToInt64(i.Price));            
            return View(new ViewModel { Items = model.ToList() });
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
    }
}
