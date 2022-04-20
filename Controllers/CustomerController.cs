using CustomersAPI.Dtos;
using CustomersAPI.Repositories;
using CustomersAPI.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace CustomersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomerDatabaseContext _context;
        private readonly IUpdateCustomerUseCase _updateCustomer;
        public CustomerController(CustomerDatabaseContext context, IUpdateCustomerUseCase updateCustomer)
        {
            _context = context;
            _updateCustomer = updateCustomer;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]
        public async Task<IActionResult> GetCustomers()
        {
            var result = _context.Customer.Select(c => c.ToDto()).ToList();
            return new OkObjectResult(result);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        public async Task<IActionResult> GetCustomer(long Id)
        {
            CustomerEntity result = await _context.Get(Id);
            return new OkObjectResult(result.ToDto());
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteCustomer(long Id)
        {
            var result = await _context.Delete(Id);
            return new OkObjectResult(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer) 
        {
            CustomerEntity result = await _context.Add(customer);
            return new CreatedResult($"https://localhost:7280/api/customer/{ result.Id }", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customer)
        {
            var result = await _updateCustomer.Execute(customer);

            if (result == null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }
    }
}
