using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindApi.Data;
using NorthwindApi.Models;
using NorthwindApi.Models.DTO;
using NorthwindAPI.Services;

namespace NorthwindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly INorthwindService<Product> _productService;

        public ProductsController(INorthwindService<Product> productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            if (await _productService.GetAllAsync() != null)
            {


                return (await _productService.GetAllAsync())
                    .Select(s => Utiles.ProductToDTO(s))
                    .ToList();
            }
            return NotFound();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
           
            var product = await _productService.GetAsync(id);

            if (product == null)
            {

                return NotFound();
            }

            return Utiles.ProductToDTO(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [Bind("ProductId, ProductName, SupplierId, CategoryId, UnitPrice")] Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

                var updateStatus = await _productService.UpdateAsync(id, product);
            
            if (!updateStatus)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct([Bind("ProductName, SupplierId, CategoryId, UnitPrice")] Product product)

        {
            var createStatus = await _productService.CreateAsync(product);

            if (!createStatus)
            {
                return Problem("_repository Is Null");
            }


            return CreatedAtAction("GetProduct", new { id = product.ProductId }, Utiles.ProductToDTO(product)); //"GetSupplier" <--- method name
            //https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.createdataction?view=aspnetcore-7.0

        }

        // DELETE: api/Products/5
/*        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            

            var product = await _productService.GetAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteAsync(id);

            return NoContent();
        }*/

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool deleted = await _productService.DeleteAsync(id);
            if (!deleted) return Problem($"Error deleting Product with ID {id}");
            return NoContent();
        }



    }
}
