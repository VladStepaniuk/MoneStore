using MoneStore.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Models.ViewModels
{
    public class CategoryIndexViewModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}
