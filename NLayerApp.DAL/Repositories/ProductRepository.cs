using Microsoft.EntityFrameworkCore;
using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;

namespace NLayerApp.DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                return await dbSet
                    .Where(p => p.IsDeleted == false)
                    .Include(p => p.Shop).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public override async Task<Product?> GetById(int? id)
        {
            try
            {
                return await dbSet
                    .Include(p => p.Shop)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override async Task<bool> Upsert(Product entity)
        {
            try
            {
                var existingProduct = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingProduct == null)
                    return await Add(entity);

                existingProduct.Id = entity.Id;
                existingProduct.Name = entity.Name;
                existingProduct.Color = entity.Color;
                existingProduct.Price = entity.Price;
                existingProduct.IsDeleted = entity.IsDeleted;
                existingProduct.ShopId = entity.ShopId;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
