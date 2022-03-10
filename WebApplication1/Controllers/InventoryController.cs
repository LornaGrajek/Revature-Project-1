using Microsoft.AspNetCore.Mvc;
using Models;
using StoreBL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private IBL _bl;
        public InventoryController(IBL bl)
        {
            _bl = bl;
        }
        // GET: api/<InventoryController>
        [HttpGet]
        public List<Inventory> GetAllInventories()
        {
            return _bl.GetAllInventories();
        }


        // GET api/<InventoryController>/5
        // Gets a list of the full inventory for a store based off of store id. Uses a zip method to join product and inventory tables
        [HttpGet("{id}")]
        public ActionResult GetInventoryByStore(int id)
        {
            List<Inventory> currentInventory = _bl.GetInventoryByStoreId(id);
            List<Product> currentProducts = _bl.GetAllProductsByStoreId(id);
            var prodInventory = currentProducts.Zip(currentInventory, (p, i) => new { Product = p, Inventory = i });
            if (prodInventory != null)
            {
                return Ok(prodInventory);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("{prodId}/{storeId}/{quantity}")]
        public ActionResult Post(int prodId, int storeId, int quantity)
        {
            if(storeId != 0)
            {
                _bl.AddProductToInventory(prodId, storeId, quantity);
                return Created($"Your product has been added to the inventory for store #{storeId}!", prodId);
            }
            else
            {
                return NoContent();
            }
        }
        //Add exception and validation here
        // PUT api/<InventoryController>/5
        [HttpPut("{prodId}/{quantity}/{storeId}")]
        public void Put(int prodId, int quantity, int storeId)
        {
            if(storeId != 0)
            {
                _bl.RestockInventory(prodId, quantity, storeId);
            }
        }
        

        // DELETE api/<InventoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bl.RemoveProductFromInventory(id);
        }
    }
}
