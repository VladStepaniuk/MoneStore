using Microsoft.AspNetCore.Mvc;
using MoneStore.Models;
using MoneStore.Models.ViewModels;
using MoneStore.Work;
using MoneStore.Work.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Controllers
{
    public class CategoryController : Controller
    {
        //TODO: Make project use unit of work and repository pattern
        //private UnitOfWork _unitOfWork;
        private CategoryManager _categoryManager;

        public CategoryController(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //var viewModel = new CategoryIndexViewModel
            //{
            //    Category = null,
            //    Categories = await _categoryManager.GetCategories()
            //};
            var model = _categoryManager.GetCategories();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            return RedirectToAction("Index");
        }
    }
}
