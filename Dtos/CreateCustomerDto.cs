using System.ComponentModel.DataAnnotations;

namespace CustomersAPI.Dtos
{
    public class CreateCustomerDto
    {
        [Required(ErrorMessage = "El nombre de su cliente debe ser especificado")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido de su cliente debe ser especificado")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
