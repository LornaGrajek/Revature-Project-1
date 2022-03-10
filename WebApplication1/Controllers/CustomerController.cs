using Microsoft.AspNetCore.Mvc;
using StoreBL;
using Models;
using Serilog;
using CustomExceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IBL _bl;
        public CustomerController(IBL bl)
        {
            _bl = bl;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public List<Customer> Get()
        {
            return _bl.GetAllCustomers();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            Customer foundCustomer = _bl.GetCustomerById(id);
            if(foundCustomer != null)
            {
                return Ok(foundCustomer);
            }
            else 
            {
                return NoContent();
            }
        }

        [HttpGet("search/{username},{password}")]
        public List<Customer> SearchCustomer(string username, string password)
        {

            List<Customer> found = _bl.SearchCustomer(username, password);
            bool ifUsername = found.Exists(x => x.UserName == username);
            bool ifPassword = found.Exists(x => x.Password == password);
            if (ifUsername && ifPassword)
            {
                return _bl.SearchCustomer(username, password);
            }
            else
            {
                throw new InputInvalidException("That username or password does not exist");
            }
            Serilog.Log.Information("A user logged in");
        }

        // POST api/<CustomerController>
        [HttpPost]
        public ActionResult Post([FromBody] Customer customerToAdd)
        {
            if(customerToAdd != null)
            {
                _bl.AddCustomer(customerToAdd);
                Serilog.Log.Information("A new customer was added");
                return Created($"Thank you {customerToAdd} for signing up!", customerToAdd);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
