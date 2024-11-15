using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task2API.Data;
using task2API.Data.Models;
using task2API.DTOs.ProductsDto_s;

namespace task2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var products = await context.Products.ToListAsync();
            var response = products.Adapt<IEnumerable<DtoProductsGet>>();
            return Ok(response);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            var productDto = product.Adapt<DtoProductsGet>();
            return Ok(productDto);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] DtoProductsCreateUpdate itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // تحقق من أن الاسم فريد
            if (await context.Products.AnyAsync(p => p.Name == itemDto.Name))
            {
                return BadRequest(new { error = $"The name ({itemDto.Name}) is already in use by another product." });
            }
            var product = itemDto.Adapt<Product>();
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return Ok("Product added successfully");
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] DtoProductsCreateUpdate itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var current = await context.Products.FindAsync(id);
            if (current == null)
            {
                return NotFound("Product not found");
            }

            // تحقق من أن الاسم فريد مع تجاهل العنصر المراد تحديثه
            if (await context.Products.AnyAsync(p => p.Name == itemDto.Name && p.Id != id))
            {
                return BadRequest(new { error = $"The name ({itemDto.Name}) is already in use by another product." });
            }

            itemDto.Adapt(current);
            await context.SaveChangesAsync();
            var updatedProductDto = current.Adapt<DtoProductsGet>();

            return Ok(updatedProductDto);
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return Ok("Product removed successfully");
        }



    }
}
