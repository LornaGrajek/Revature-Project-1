using Microsoft.AspNetCore.Mvc;
using Models;
using StoreBL;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IBL _bl;
        public ProductController(IBL bl)
        {
            _bl = bl;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public List<Product> GetAllProducts()
        {
            return _bl.GetAllProducts();
        }

        // GET api/<ProductController>/5
        [HttpGet("{name}")]
        public int GetProductId(string name)
        {
            int id = _bl.GetProductIdByName(name);
            return id;
        }

        // POST api/<ProductController>
        // Add a product, add that product to the inventory
        [HttpPost]
        public ActionResult Post([FromBody] Product productToAdd)
        {
            if (productToAdd.ProductName != null)
            {
                _bl.AddProduct(productToAdd);
                Serilog.Log.Information("A new product was added");
                return Created($"{productToAdd.ProductName} has been added to the product list!", productToAdd);
            }
            else
            {
                return NoContent();
            }
        }
      
        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bl.RemoveProduct(id);
        }
    }
}
