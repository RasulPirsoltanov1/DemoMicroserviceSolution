using CustomerWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        CustomerDbContext _customerDbContext;

        public CustomerController(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _customerDbContext.Customers.ToListAsync();
            return Ok(customers);
        }
        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetById(int customerId)
        {
            var customer = await _customerDbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            return Ok(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            var result = await _customerDbContext.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            _customerDbContext.Customers.Update(customer);
            var result = await _customerDbContext.SaveChangesAsync();
            return Ok(result);
        }
        [HttpDelete("{customerId:int}")]
        public async Task<IActionResult> Delete(int customerId)
        {
            var customer = await _customerDbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            if(customer == null)
            {
                return BadRequest("customer does not exists!");
            }
            _customerDbContext.Customers.Remove(customer);
            var result = await _customerDbContext.SaveChangesAsync();
            return Ok(result);
        }
    }
}
