using Application.Base.Dtos;

namespace Domain.Customer.Dtos
{
    public class CustomerDto : PersonDto
    {
        public CustomerDto()
        {
        }
        public CustomerDto(Entities.Customer customer)
        {
            Id = customer.Id;
            CreatedAt = customer.CreatedAt;
            UpdatedAt = customer.UpdatedAt;
            IsActive = customer.IsActive;
            Cpf = customer.Cpf;
            Name = customer.Name;
            Email = customer.Email;
            BirthDay = customer.BirthDay;
        }

        public static CustomerDto ToDto(Entities.Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt,
                IsActive = customer.IsActive,
                Cpf = customer.Cpf,
                Name = customer.Name,
                Email = customer.Email,
                BirthDay = customer.BirthDay,
            };
        }
    }
}
