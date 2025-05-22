using Domain.Customer.Dtos;
using Domain.Customer.Ports.In;
using Domain.Customer.Ports.Out;

namespace Application.Customer
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto customerDto, CancellationToken cancellationToken)
        {
            try
            {
                var customer = CustomerDto.ToEntity(customerDto);

                await _customerRepository.CreateAsync(customer, cancellationToken);

                customerDto.Id = customer.Id;

                return customerDto;
            }
            catch (Exception ex) 
            {
                return new CustomerDto
                {
                    ErrorMessage = $"Message: {ex.Message}",
                    Error = true
                };
            }
        }

        public async Task<CustomerDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);
            if (customer == null)
            {
                return new CustomerDto
                {
                    ErrorMessage = "Employee not found.",
                    Error = true
                };
            }

            return CustomerDto.ToDto(customer);
        }

        public async Task<CustomerDto> UpdateAsync(CustomerDto customerDto, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(customerDto.Id, cancellationToken);

                if (customer == null)
                {
                    return new CustomerDto
                    {
                        ErrorMessage = "Employee not found.",
                        Error = true
                    };
                }

                customer.Cpf = customerDto.Cpf;
                customer.Name = customerDto.Name;
                customer.Surname = customerDto.Surname;
                customer.Email = customerDto.Email;
                customer.BirthDay = customerDto.BirthDay;
                customer.IsActive = customerDto.IsActive;
                customer.UpdatedAt = DateTime.UtcNow;

                var updatedCustomer = await _customerRepository.UpdateAsync(customer, cancellationToken);

                return CustomerDto.ToDto(updatedCustomer);
            }
            catch (Exception ex) 
            {
                return new CustomerDto
                {
                    ErrorMessage = $"Message: {ex.Message}",
                    Error = true
                };
            }
        }
    }
}
