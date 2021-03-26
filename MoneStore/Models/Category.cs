using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength (3, ErrorMessage ="Category name must be at least 3 characyers and at max 50 characters.", MinimumLength =50)]
        public string Name { get; set; }
        [Required]
        public byte[] ImageCategory { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

}
