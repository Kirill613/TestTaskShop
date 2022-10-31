using NLayerApp.DAL.Entities;

namespace TestTaskShop.Models
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public List<Product> OrderProducts { get; set; }
    }
}
