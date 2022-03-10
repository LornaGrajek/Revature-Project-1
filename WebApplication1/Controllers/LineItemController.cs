using Microsoft.AspNetCore.Mvc;
using Models;
using StoreBL;
using CustomExceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineItemController : ControllerBase
    {
        private IBL _bl;
        public LineItemController(IBL bl)
        {
            _bl = bl;
        }
        // GET: api/<LineItemController>
        [HttpGet]
        public void Get()
        {

        }

        // POST api/<LineItemController>
        [HttpPost]
        public void PostNewLineItem(int prodId, int quantity, int orderId, int storeId, int customerId)
        {
            DateTime date = DateTime.Now;
            LineItem lineitem = new LineItem
            {
                OrderId = orderId,
                Quantity = quantity,
                ProductID = prodId
            };
            Order order = new Order
            {
                OrderNumber = orderId,
                StoreId = storeId,
                OrderDate = date,
                CustomerId = customerId
            };
            List<LineItem> cart = new List<LineItem>();
            cart.Add(lineitem);

            if (prodId != 0)
            {
                _bl.AddOrder(order);
                foreach (LineItem item in cart)
                {
                    _bl.AddLineItem(item, orderId);
                }
            }
            else
            {
                throw new InputInvalidException("Please choose a valid product");
            }
        }
    }
}

