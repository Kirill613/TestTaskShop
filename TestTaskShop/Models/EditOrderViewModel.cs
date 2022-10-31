using NLayerApp.DAL.Entities;

namespace TestTaskShop.Models
{
    public class EditOrderViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }
    }
}
