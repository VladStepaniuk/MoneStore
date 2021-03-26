using Microsoft.AspNetCore.Mvc;
using MoneStore.Models;
using MoneStore.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Controllers
{
    public class CategoryController : Controller
    {
        private UnitOfWork _unitOfWork;

        public CategoryController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new CategoryIndexModel
            {
                Categories = _unitOfWork.CategoryRepository.Get(includeProperties: "Products")
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            return RedirectToAction("Index");
        }
    }
}
