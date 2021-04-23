using Microsoft.EntityFrameworkCore;
using MoneStore.Data;
using MoneStore.Enums;
using MoneStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Work.Repository
{
    public class OrderRepositoryImpl : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepositoryImpl(ApplicationDbContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        public async Task CreateOrder(Order order)
        {
            order.CreateDate = DateTime.Now;
            await _context.Orders.AddAsync(order);

            var orderDetails = new List<OrderDetail>(_shoppingCart.ShoppingCartItems.Count());

            foreach(var item in _shoppingCart.ShoppingCartItems)
            {
                orderDetails.Add(
                    new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = item.Product.Id,
                        Amount = Math.Min(item.Amount, item.Product.inStock),
                        Price = item.Product.Price,
                        Product = item.Product
                    });
                _context.Update(item.Product);
                item.Product.inStock = Math.Max(item.Product.inStock - item.Amount, 0);
            }

            await _context.OrderDetails.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetById(int orderId)
        {
            return await _context.Orders.Where(order => order.Id == orderId).FirstOrDefaultAsync();
        }

        public async Task<IList<Order>> GetByUserId(string userId)
        {
            return await _context.Orders.Where(order => order.User.Id == userId).ToListAsync();
        }

        private void SetOrderBy(IEnumerable<Order> orders, OrderBy orderBy)
        {
            switch (orderBy)
            {
                case OrderBy.DateDesc:
                    orders = orders.OrderByDescending(order => order.CreateDate);
                    break;
                case OrderBy.DateAsc:
                    orders = orders.OrderBy(order => order.CreateDate);
                    break;
                case OrderBy.PriceAsc:
                    orders = orders.OrderBy(order => order.OrderTotal);
                    break;
                case OrderBy.PriceDesc:
                    orders = orders.OrderByDescending(order => order.OrderTotal);
                    break;
            }
        }

        public async Task<IList<Order>> GetFilteredOrders(string userId = null,
            OrderBy orderBy = OrderBy.None,
            int offset = 0, int limit = 10, 
            decimal? minimalPrice = null, 
            decimal? maximalPrice = null, 
            DateTime? minDate = null, 
            DateTime? maxDate = null, 
            string zipCode = null)
        {
            var orders = string.IsNullOrEmpty(userId) ? await _context.Orders.ToListAsync() : await _context.Orders.Where(order => order.User.Id == userId).ToListAsync();

            if (orderBy != OrderBy.None)
            {
                SetOrderBy(orders, orderBy);
            }

            if (minimalPrice.HasValue)
            {
                orders = orders.Where(order => order.OrderTotal > minimalPrice).ToList();
            }

            if (maximalPrice.HasValue)
            {
                orders = orders.Where(order => order.OrderTotal > maximalPrice).ToList();
            }

            if (minDate.HasValue)
            {
                orders = orders.Where(order => order.CreateDate > minDate.Value).ToList();
            }

            if (maxDate.HasValue)
            {
                orders = orders.Where(order => order.CreateDate < maxDate.Value).ToList();
            }

            if (!string.IsNullOrEmpty(zipCode))
            {
                orders = orders.Where(order => order.ZipCode == zipCode).ToList();
            }

            return orders.Skip(offset).Take(limit).ToList();
        }

        public async Task<IList<Order>> GetUserLatestOrders(int count, string userId)
        {
            return await _context.Orders.Where(order => order.User.Id == userId)
               .OrderByDescending(order => order.CreateDate)
               .Take(count).ToListAsync();
        }

        public async Task<IList<Product>> GetUserMostPopularFoods(string id)
        {
            Dictionary<Product, int> products = new Dictionary<Product, int>();

            var a = await _context.Orders.Where(order => order.User.Id == id).ToListAsync();
            foreach (var order in a)
            {
                foreach (var line in order.OrderDetails)
                {
                    if (products.ContainsKey(line.Product))
                    {
                        products[line.Product] += line.Amount;
                    }
                    else
                    {
                        products[line.Product] = line.Amount;
                    }
                }
            }

            return (IList<Product>)products.Take(10).ToList();
        }
    }
}
