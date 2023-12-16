using EcommerceStore.Models;

namespace EcommerceStore.Services.Iservices
{
    public interface Iproducts
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProduct(Guid id);
        Task<string> CreateProduct(Product product);
        Task<string> UpdateProduct();
        Task<string> DeleteProduct(Guid id);
    }
}
