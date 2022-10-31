using Microsoft.EntityFrameworkCore;
using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;

namespace NLayerApp.DAL.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Order>> GetAll()
        {
            try
            {
                return await dbSet.Include(o => o.Customer).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Order>();
            }
        }

        public override async Task<Order?> GetById(int? id)
        {
            try
            {
                return await dbSet
                    .Include(o => o.Customer)
                    .Include(o => o.ProductOrders)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override async Task<bool> Upsert(Order entity)
        {
            try
            {
                var existingOrder = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingOrder == null)
                    return await Add(entity);

                existingOrder.Id = entity.Id;
                existingOrder.Number = entity.Number;
                existingOrder.IsActive = entity.IsActive;
                existingOrder.CustomerId = entity.CustomerId;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
