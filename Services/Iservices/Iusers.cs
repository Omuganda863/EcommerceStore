using EcommerceStore.Models;

namespace EcommerceStore.Services.Iservices
{
    public interface Iusers
    {
        Task<string> CreateUserAsync(User user);
        Task<User> GetUserByEmail(string email);
    }
}
