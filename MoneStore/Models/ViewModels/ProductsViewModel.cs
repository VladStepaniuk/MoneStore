using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Models.ViewModels
{
    public class ProductsViewModel
    {
        public Category Category { get; set; }
        public IList<Product> Products { get; set; }
    }
}
