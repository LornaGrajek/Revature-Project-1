using Microsoft.AspNetCore.Mvc;
using Models;
using StoreBL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController1 : ControllerBase
    {
        private IBL _bl;
        public CustomerOrderController1(IBL bl)
        {
            _bl = bl;
        }

        // GET api/<CustomerOrderController1>/5
        [HttpGet("{id}")]
        public List<Order> Get(int id)
        {
            return _bl.GetAllCustomerOrders(id);   
        }
      
    }
}
