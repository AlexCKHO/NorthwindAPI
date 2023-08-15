
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindApi.Data.Repositories;
using NorthwindApi.Models;
using NorthwindApi.Models.DTO;
using NorthwindAPI.Services;


namespace NorthwindApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly INorthwindService<Supplier> _supplierService;

        public SuppliersController(

            INorthwindService<Supplier> supplierService)
        {

            _supplierService = supplierService;
        }

        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetSuppliers()
        {
            if (_context.Suppliers == null)
            {
                return NotFound();
            }
            return await _context.Suppliers.Include(s => s.Products)
                .Select(s => Utiles.SupplierToDTO(s))
                .ToListAsync();
        }*/

        // GET: api/Suppliers

        /*
         * [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetSuppliers()
        {
          if (_supplierRepository.IsNull) 
          {
              return NotFound();
          }
            return (await _supplierRepository.GetAllAsync())
                .Select(s => Utiles.SupplierToDTO(s))
                .ToList();
        }
         * 
         */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetSuppliers()
        {
          if(await _supplierService.GetAllAsync() != null) { 
            return (await _supplierService.GetAllAsync())
                .Select(s => Utiles.SupplierToDTO(s))
                .ToList();
            } return NotFound();
        }


        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDTO>> GetSupplier(int id)
        {

            var supplier = await _supplierService.GetAsync(id);

            if (supplier == null)
            {
                
                return NotFound();
            }

            return Utiles.SupplierToDTO(supplier);
        }


        /*
         *  [HttpGet("{id}/proudcts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsBySupplier(int id)
        {
            if (_supplierRepository.IsNull)
            {
                return NotFound();
            }
            var supplier = await _supplierRepository.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }


            return supplier.Products.Select(p => Utiles.ProductToDTO(p)).ToList();

        }*/

        [HttpGet("{id}/proudcts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsBySupplier(int id)
        {
            var supplier = await _supplierService.GetAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }


            return supplier.Products.Select(p => Utiles.ProductToDTO(p)).ToList();

        }

        // PUT: api/Suppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier(int id,
            [Bind("SupplierId, CompanyName, ContactTitle, Country")]
            Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return BadRequest();
            }

            var updateStatus = await _supplierService.UpdateAsync(id, supplier);

            if (!updateStatus)
            {
                return NotFound();
            }

            return NoContent();
            
        }

        // POST: api/Suppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SupplierDTO>> PostSupplier(
            [Bind("CompnayName", "ContactName", "ContactTitle", "County", "Product")] Supplier supplier)
            
        {
            var createStatus = await _supplierService.CreateAsync(supplier);

            if (!createStatus)
          {
              return Problem("_repository Is Null");
          }
            

            return CreatedAtAction("GetSupplier", new { id = supplier.SupplierId },Utiles.SupplierToDTO(supplier)); //"GetSupplier" <--- method name
            //https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.createdataction?view=aspnetcore-7.0

        }

        // DELETE: api/Suppliers/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            if (_context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .Where(c => c.SupplierId == id)
               .FirstOrDefaultAsync();

            if (supplier == null)
            {
                return NotFound();
            }

            //_context.Suppliers.Remove(supplier);
           _context.Remove(Utiles.SupplierToDTO(supplier));

            await _context.SaveChangesAsync();

            return NoContent();
        }*/


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {


            var supplier = await _supplierService.GetAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            supplier.Products.Select(p => p.SupplierId = null);

            await _supplierService.DeleteAsync(id);

            return NoContent();
        }

    }
}
