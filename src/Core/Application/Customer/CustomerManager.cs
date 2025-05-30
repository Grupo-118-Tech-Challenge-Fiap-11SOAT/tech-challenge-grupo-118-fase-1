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
                var customer = new Domain.Customer.Entities.Customer(
                    customerDto.Cpf,
                    customerDto.Name,
                    customerDto.IsActive,
                    customerDto.BirthDay,
                    customerDto.Email
                    );

                var createdCustomer = await _customerRepository.CreateAsync(customer, cancellationToken);

                return new CustomerDto(createdCustomer);
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
                    ErrorMessage = "Customer not found.",
                    Error = true
                };
            }

            return CustomerDto.ToDto(customer);
        }
        
        public async Task<CustomerDto?> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByCpfAsync(cpf, cancellationToken);
            if (customer == null)
            {
                return new CustomerDto
                {
                    ErrorMessage = "Customer not found.",
                    Error = true
                };
            }

            return CustomerDto.ToDto(customer);
        }

        public async Task<CustomerDto> UpdateAsync(CustomerDto customerDto, CancellationToken cancellationToken)
        {
            try
            {
                var customer = new Domain.Customer.Entities.Customer(
                    customerDto.Cpf,
                    customerDto.Name,
                    customerDto.IsActive,
                    customerDto.BirthDay,
                    customerDto.Email
                );
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