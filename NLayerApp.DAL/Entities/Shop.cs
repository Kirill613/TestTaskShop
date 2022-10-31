namespace NLayerApp.DAL.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
