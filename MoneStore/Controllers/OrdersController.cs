using Microsoft.AspNetCore.Mvc;
using MoneStore.Models;
using MoneStore.Work.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly ProductManager _productManager;
        public IActionResult Checkout()
        {
            var items = _shoppingCart.
            return View();
        }
    }
}
