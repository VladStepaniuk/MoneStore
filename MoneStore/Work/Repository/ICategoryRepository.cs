using MoneStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Work.Repository
{
    public interface ICategoryRepository
    {
        Task<Category> GetById(int id);
        Task Add(Category category);
        Task Delete(int categoryId);
        Task<IList<Category>> GetCategoryList();
        Task Update(Category category);
    }
}
