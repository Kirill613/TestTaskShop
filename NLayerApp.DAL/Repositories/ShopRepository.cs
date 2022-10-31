using Microsoft.EntityFrameworkCore;
using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;

namespace NLayerApp.DAL.Repositories
{
    public class ShopRepository : GenericRepository<Shop>, IShopRepository
    {
        public ShopRepository(AppDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Shop>> GetAll()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception)
            {
                return new List<Shop>();
            }
        }
    }
}
