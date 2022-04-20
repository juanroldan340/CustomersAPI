using CustomersAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomersAPI.Repositories
{
    public class CustomerDatabaseContext : DbContext
    {
        public CustomerDatabaseContext(DbContextOptions<CustomerDatabaseContext> options) : base(options)
        { 
        
        }

        public DbSet<CustomerEntity> Customer { get; set; } //se referencia a las tablas

        public async Task<CustomerEntity> Get(long Id) 
        {
            return await Customer.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<CustomerEntity> Add(CreateCustomerDto customerDto)
        {
            CustomerEntity entity = new CustomerEntity 
            { 
                Id = null,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                Address = customerDto.Address,
            };
            EntityEntry<CustomerEntity> response = await Customer.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("No se ha podido guardar el cliente."));
        }

        public async Task<bool> Delete(long Id) 
        {
            CustomerEntity entity = await Get(Id);
            Customer.Remove(entity);
            SaveChanges();
            return true;
        }

        public async Task<bool> Update(CustomerEntity customerEntity) 
        {
            Customer.Update(customerEntity);
            await SaveChangesAsync();
            return true;
        }
    }
    public class CustomerEntity
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public CustomerDto ToDto()
        {
            return new CustomerDto()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                Address = Address,
                Id = Id ?? throw new Exception("El Id no puede ser null.")
            };
        }
    }
}
