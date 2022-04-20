using CustomersAPI.Dtos;
using CustomersAPI.Repositories;

namespace CustomersAPI.UseCases
{
    public interface IUpdateCustomerUseCase 
    {
        Task<CustomerDto?> Execute(CustomerDto customer);
    }
    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        
        private readonly CustomerDatabaseContext _context;
        public UpdateCustomerUseCase(CustomerDatabaseContext context)
        {
            _context = context;
        }
        public async Task<CustomerDto?> Execute(CustomerDto customer) 
        {
            CustomerEntity entity = await _context.Get(customer.Id);
            if (entity == null)
                return null;

            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Email = customer.Email;
            entity.Phone = customer.Phone;
            entity.Address = customer.Address;

            await _context.Update(entity);
            return entity.ToDto();

        }

    }
}
