using Common.Dto.Customers;

namespace Common.Interfaces.Customer.Controller;

public interface ICustomerController
{
    Task<CustomerResponseDto> CreateAsync(CustomerRequestDto customer, CancellationToken cancellationToken = default);
    Task<CustomerResponseDto?> GetCustomerByCpf(string cpf, CancellationToken cancellationToken = default);
    Task<CustomerResponseDto?> GetCustomerById(int id, CancellationToken cancellationToken = default);
    Task<CustomerResponseDto?> UpdateAsync(CustomerUpdateDto customer, CancellationToken cancellationToken = default);
}
