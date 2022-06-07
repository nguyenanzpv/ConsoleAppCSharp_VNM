using Microsoft.AspNetCore.Mvc;//[Route], [ApiController], ControllerBase
using SolidEdu.Shared;//Customer Entity
using Ecommerce.WebApi.Repositories;//ICustomerRepository

namespace Ecommerce.WebApi.Controllers
{
    //url base: api/Customers => json
    [Route("api/controller")] //api/Customers
    [ApiController]//using http verse
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository repo;
        //constructor inject repository in startup
        public CustomersController(ICustomerRepository repo)
        {
            this.repo = repo;//tiem instance CustomerRepository
        }

        //Get: api/customers
        //Get: api/customers/?country = [country]
        [HttpGet]
        [ProducesResponseType(200, Type=typeof(IEnumerable<Customer>))]//thanh cong tra ve 200, kieu object Customer -> convert to json
        [ProducesResponseType(404)]
        public async Task<IEnumerable<Customer>> GetCustomers(string? country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                return await repo.RetrieveAllAsync();
            }
            else
            {
                //using linq to return 
                return (await repo.RetrieveAllAsync()).Where(c => c.Country == country);
            }
        }

        //Get: api/customers/[id]
        //return 1 phan tu nen dung IActionResult
        [HttpGet("{id}", Name = nameof(GetCustomer))]
        [ProducesResponseType(200, Type=typeof(Customer))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCustomer(string id)
        {
            Customer? c = await  repo.RetrieveAsync(id);
            if(c == null)
            {
                return NotFound();// 404
            }
            else
            {
                return Ok(c);// 200 va customer info
            }
        }

        //Post:api/customers (json or xml)
        [HttpPost]
        [ProducesResponseType(201, Type=typeof(Customer))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Customer c)// get customer from body request
        {
            if (c == null)
            {
                return BadRequest();// code 400
            }
            Customer? addedCustomer = await repo.CreateAsync(c);
            if(addedCustomer == null)
            {
                return BadRequest("Repository failed to create customer" + c.CustomerId);
            }
            else
            {
                //Problem detail class in  [ApiController] of IActionResult return default for 4xx
                return CreatedAtRoute(
                    //GetCustomer/CustomerId -> addedCustomer value
                    routeName: nameof(GetCustomer),
                    routeValues: new {id=addedCustomer.CustomerId.ToLower()}, //anonymus type
                    value:addedCustomer
                );
            }
        }

    }
}
