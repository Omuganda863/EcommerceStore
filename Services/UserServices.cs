using EcommerceStore.Data;
using EcommerceStore.Models;
using EcommerceStore.Services.Iservices;
using Microsoft.EntityFrameworkCore;

namespace EcommerceStore.Services
{
    public class UserServices : Iusers
    {
        private readonly DataContext _datacontext;
        public UserServices(DataContext datacontext)
        {
            _datacontext = datacontext;
        }
        public async Task<string> CreateUserAsync(User user)
        {
            await _datacontext.AddAsync(user);
            await _datacontext.SaveChangesAsync();
            return "User Created Successfully";
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var usr = await _datacontext.Users.Where(user => user.Email == email).FirstOrDefaultAsync();
            return usr;

        }

    }
}
