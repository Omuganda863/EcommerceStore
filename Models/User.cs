using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public string Roles { get; set; } = "User";
        List <Order> Orders = new List<Order> ();
    }
}
