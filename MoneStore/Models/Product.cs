using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }
        public byte[] PreviewIage { get; set; }
        public int inStock { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
