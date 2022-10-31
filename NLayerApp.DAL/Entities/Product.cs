using System.Drawing;

namespace NLayerApp.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Color { get; set; }
        public bool IsDeleted { get; set; }

        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
