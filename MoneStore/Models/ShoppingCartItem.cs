using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
