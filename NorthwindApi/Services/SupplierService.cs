using NorthwindApi.Data.Repositories;
using NorthwindApi.Models;
using NorthwindAPI.Services;

namespace NorthwindApi.Services
{
   public class SupplierService : NorthwindService<Supplier>
    {
        public SupplierService(ILogger<INorthwindService<Supplier>> logger, INorthwindRepository<Supplier> repository) : base(logger, repository)
        {
        }
    }
}
