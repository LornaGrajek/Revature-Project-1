using Microsoft.AspNetCore.Mvc;
using Models;
using StoreBL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreOrderController : ControllerBase
    {
        private IBL _bl;
        public StoreOrderController(IBL bl)
        {
            _bl = bl;
        }

        // GET api/<OrderController>/5
        [HttpGet("{storeId}")]
        public List<Order> Get(int storeId)
        {
            return _bl.GetAllStoreOrders(storeId);
        }

        // POST api/<OrderController>
        [HttpPost]
        public ActionResult Post([FromBody] Order orderToAdd)
        {
            if (orderToAdd != null)
            {
                _bl.AddOrder(orderToAdd);
                return Created($"Thank you for placing an order!", orderToAdd);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
