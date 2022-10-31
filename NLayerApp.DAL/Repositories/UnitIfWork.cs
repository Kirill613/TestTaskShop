using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;

namespace NLayerApp.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IShopRepository Shops { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Products = new ProductRepository(context);
            Orders = new OrderRepository(context);
            Shops = new ShopRepository(context);
        }       

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
