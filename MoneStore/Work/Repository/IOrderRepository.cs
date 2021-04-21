using MoneStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Work.Repository
{
    public interface IOrderRepository
    {
        Task CreateOrder();
        Task<Order> GetById(int id);
        Task<IEnumerable<Order>> GetUsersLatestOrders();
        Task<IEnumerable<Product>> GetUsersMostPopularProducts();
        Task<IEnumerable<Order>> GetAll();
    }
}
