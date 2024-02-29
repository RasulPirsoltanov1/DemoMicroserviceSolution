using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using OrderWebApi.Models;

namespace OrderWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IMongoCollection<Order> _orderCollection;
        public OrderController()
        {
            string dbName = Environment.GetEnvironmentVariable("DB_NAME");
            string hostName = Environment.GetEnvironmentVariable("DB_HOST");
            string connectionString = $"mongodb://{hostName}:27017/{dbName}";
            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(dbName);
            _orderCollection = database.GetCollection<Order>("order");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Order> orders = await _orderCollection.Find(new BsonDocument()).ToListAsync();
            return Ok(orders);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(string orderId)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(x => x.OrderId, orderId);
            var order = await _orderCollection.Find(filter).SingleOrDefaultAsync();
            return Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            await _orderCollection.InsertOneAsync(order);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> AddToPlaylist(Order order)
        {
            var filterDefinition = Builders<Order>.Filter.Eq(x=>x.OrderId, order.OrderId);
            await _orderCollection.ReplaceOneAsync(filterDefinition,order);
            return NoContent();
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> Delete(string orderId)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(x=>x.OrderId, orderId);
            await _orderCollection.DeleteOneAsync(filter);
            return NoContent();
        }
    }
}
