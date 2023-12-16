using EcommerceStore.Data;
using EcommerceStore.Models;
using EcommerceStore.Services.Iservices;
using Microsoft.EntityFrameworkCore;

namespace EcommerceStore.Services
{
    public class ProductServices : Iproducts
    {
        private readonly DataContext _datacontext;

        public ProductServices(DataContext dataContext)
        {
           _datacontext = dataContext;
        }
        public async Task<string> CreateProduct(Product product)
        {
          await _datacontext.Products.AddAsync(product);
            await _datacontext.SaveChangesAsync();
            return "Product Created";

        }

        public async Task<string> DeleteProduct(Guid id)
        {
            var product = await _datacontext.Products.Where(product =>product.Id== id).FirstOrDefaultAsync();
            if(product != null)
            {
                _datacontext.Products.Remove(product);
                return "Product deleted successfully";
            }
            return "Product not found";
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _datacontext.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(Guid id)
        {
           var product = await _datacontext.Products.Where(product=> product.Id==id).FirstOrDefaultAsync();
            return product;
        }

        public async Task<string> UpdateProduct()
        {
            await _datacontext.SaveChangesAsync();
            return "Product Updated";
        }

    }
}
