using Microsoft.AspNetCore.Identity;
using NLayerApp.DAL.Entities;

namespace NLayerApp.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IShopRepository Shops { get; }
        Task CompleteAsync();
    }
}
