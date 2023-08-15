using Microsoft.EntityFrameworkCore;
using NorthwindApi.Models;

namespace NorthwindApi.Data.Repositories
{
    public class ProductsRepository : NorthwindRepository<Product>

    {
        public ProductsRepository(NorthwindContext context) : base(context)
        {

        }

        public override async Task<Product?> FindAsync(int id)
        {

            return await _dbSet
                .Where(s => s.ProductId == id)
                .FirstOrDefaultAsync();
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbSet
                .ToListAsync();
        }



    }
}
