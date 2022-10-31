namespace NLayerApp.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
