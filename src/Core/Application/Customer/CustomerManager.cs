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
            var customer = new Domain.Customer.Entities.Customer(
                customerDto.Cpf,
                customerDto.Name,
                customerDto.Surname,
                customerDto.Email,
                customerDto.BirthDay,
                customerDto.IsActive
                );

            var createdCustomer = await _customerRepository.CreateAsync(customer, cancellationToken);

            return new CustomerDto(createdCustomer);
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

            return new CustomerDto(customer);
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

            return new CustomerDto(customer);
        }

        public async Task<CustomerDto?> UpdateAsync(CustomerDto customerDto, CancellationToken cancellationToken)
        {
            var customer = new Domain.Customer.Entities.Customer(
                customerDto.Cpf,
                customerDto.Name,
                customerDto.Surname,
                customerDto.Email,
                customerDto.BirthDay,
                customerDto.IsActive
            );
            var updatedCustomer = await _customerRepository.UpdateAsync(customer, cancellationToken);
            
            if(updatedCustomer is null)
                return null;
            
            return new CustomerDto(updatedCustomer);
        }
    }
}