using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductDbContext _dbContext;

        public ProductController(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _dbContext.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetById(int productId)
        {
            Product? product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            var result = await _dbContext.SaveChangesAsync();
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            try
            {
                _dbContext.Products.Update(product);
                var result = await _dbContext.SaveChangesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> Delete(int productId)
        {
            Product? product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            if (product == null)
            {
                return BadRequest("this product does not exists!");
            }
            _dbContext.Products.Remove(product);
            var result = await _dbContext.SaveChangesAsync();
            return Ok(result);
        }
    }
}
