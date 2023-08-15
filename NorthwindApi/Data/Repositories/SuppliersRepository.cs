using Microsoft.EntityFrameworkCore;
using NorthwindApi.Models;

namespace NorthwindApi.Data.Repositories
{
    public class SuppliersRepository : NorthwindRepository<Supplier>

    {
        public SuppliersRepository(NorthwindContext context) : base(context)
        {

        }

        public override async Task<Supplier?> FindAsync(int id)
        {

            return await _dbSet.Where(s => s.SupplierId == id)
                .Include(s => s.Products)             
                .FirstOrDefaultAsync();
        }

        public override async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _dbSet.Include(s => s.Products).ToListAsync();
        }

    }
}
