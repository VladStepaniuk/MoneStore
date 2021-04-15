using MoneStore.Models;
using MoneStore.Work.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneStore.Work.Managers
{
    public class ProductManager
    {
        private IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProduct(Product product)
        {
            await _productRepository.Add(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _productRepository.Update(product);
        }

        public async Task DeleteProduct(Product product)
        {
            await _productRepository.Delete(product);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<IList<Product>> GetProductList()
        {
            return await _productRepository.GetProducts();
        }

    }
}
