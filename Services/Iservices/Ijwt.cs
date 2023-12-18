using EcommerceStore.Models;

namespace EcommerceStore.Services.Iservices
{
    public interface Ijwt
    {
        string GenerateJwtToken(User user);
    }
}
