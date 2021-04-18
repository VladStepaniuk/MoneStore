using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneStore.Models;
using MoneStore.Models.ViewModels;
using MoneStore.Work.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(ShoppingCart shoppingCart, IProductRepository productRepository)
        {
            _shoppingCart = shoppingCart;
            _productRepository = productRepository;
        }

        public IActionResult Index(bool isValidAmount=true, string returnUrl="/")
        {
            _shoppingCart.GetShoppingCartItems();
            var model = new ShoppingCartIndexViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
                ReturnUrl = returnUrl
            };

            if (!isValidAmount)
            {
                ViewBag.InvalidAmountText = "*There were not enough items in stock to add*";
            }
            return View(model);
        }

        [HttpGet]
        [Route("/ShoppingCart/Add/{id}/{returnUrl?}")]
        public async Task<IActionResult> Add(int id, int? amount=1, string returnUrl=null)
        {
            Product product = await _productRepository.GetById(id);

            returnUrl = returnUrl.Replace("%2F", "/");
            bool isValidAmount = false;

            if(product != null)
            {
                isValidAmount = _shoppingCart.AddToCart(product, amount.Value);
            }

            return Index(isValidAmount, returnUrl);
        }

        public async Task<IActionResult> Remove(int prodId)
        {
            Product product = await _productRepository.GetById(prodId);

            if (product != null)
            {
                _shoppingCart.RemoveFromCart(product);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Back(string returnUrl = "/")
        {
            return Redirect(returnUrl);
        }
    }
}
