using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoneStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Models
{
    public class ShoppingCart
    {
		private readonly ApplicationDbContext _context;

		public ShoppingCart(ApplicationDbContext context)
		{
			_context = context;
		}
		public string Id { get; set; }
		public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

		public static ShoppingCart GetCart(IServiceProvider services)
		{
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			var context = services.GetService<ApplicationDbContext>();

			string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

			return new ShoppingCart(context) { Id = cartId };
		}
		public bool AddToCart(Product product, int amount)
		{
			if (product.inStock == 0 || amount == 0)
			{
				return false;
			}

			var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
				s => s.Product.Id == product.Id && s.ShoppingCartId == Id);
			var isValidAmount = true;
			if (shoppingCartItem == null)
			{
				if (amount > product.inStock)
				{
					isValidAmount = false;
				}
				shoppingCartItem = new ShoppingCartItem
				{
					ShoppingCartId = Id,
					Product = product,
					Amount = Math.Min(product.inStock, amount)
				};
				_context.ShoppingCartItems.Add(shoppingCartItem);
			}
			else
			{
				if (product.inStock - shoppingCartItem.Amount - amount >= 0)
				{
					shoppingCartItem.Amount += amount;
				}
				else
				{
					shoppingCartItem.Amount += (product.inStock - shoppingCartItem.Amount);
					isValidAmount = false;
				}
			}

			_context.SaveChanges();
			return isValidAmount;
		}

		public int RemoveFromCart(Product product)
		{
			var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
				s => s.Product.Id == product.Id && s.ShoppingCartId == Id);
			int localAmount = 0;
			if (shoppingCartItem != null)
			{
				if (shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount--;
					localAmount = shoppingCartItem.Amount;
				}
				else
				{
					_context.ShoppingCartItems.Remove(shoppingCartItem);
				}
			}

			_context.SaveChanges();
			return localAmount;
		}

		public IEnumerable<ShoppingCartItem> GetShoppingCartItems()
		{
			return ShoppingCartItems ??
				   (ShoppingCartItems = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == Id)
					   .Include(s => s.Product));
		}

		public void ClearCart()
		{
			var cartItems = _context
				.ShoppingCartItems
				.Where(cart => cart.ShoppingCartId == Id);

			_context.ShoppingCartItems.RemoveRange(cartItems);
			_context.SaveChanges();
		}

		public decimal GetShoppingCartTotal()
		{
			return _context.ShoppingCartItems.Where(c => c.ShoppingCartId == Id)
				.Select(c => c.Product.Price * c.Amount).Sum();
		}
	}
}
