using NLayerApp.DAL.Entities;

namespace TestTaskShop.Models
{
    public class AddProductViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Color { get; set; }
        public bool IsDeleted { get; set; }

        public int ShopId { get; set; }
    }
}
