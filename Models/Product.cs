using System.ComponentModel.DataAnnotations;

namespace EcommerceStore.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public int Price { get; set; }

        List <Order> Orders = new List <Order>();

    }
}
