using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneStore.Models;
using MoneStore.Models.ViewModels;
using MoneStore.Work.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CategoryManager _categoryManager;
        private ProductManager _productManager;

        public HomeController(ILogger<HomeController> logger, 
            CategoryManager categoryManager,
            ProductManager productManager)
        {
            _logger = logger;
            _categoryManager = categoryManager;
            _productManager = productManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel
            {
                Categories = await _categoryManager.GetCategoriesForHomePage(),
                Products =await _productManager.GetProductList()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Products(int? id)
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
