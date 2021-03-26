using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
