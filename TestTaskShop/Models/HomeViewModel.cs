using NLayerApp.DAL.Entities;

namespace TestTaskShop.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
