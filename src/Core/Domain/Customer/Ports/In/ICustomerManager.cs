using Domain.Customer.Dtos;

namespace Domain.Customer.Ports.In
{
    public interface ICustomerManager
    {
        Task<CustomerResponseDto> CreateAsync(CustomerRequestDto customerRequestDto,
            CancellationToken cancellationToken);

        Task<CustomerResponseDto> UpdateAsync(int id, CustomerUpdateDto customerUpdateDto,
            CancellationToken cancellationToken);

        Task<CustomerResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<CustomerResponseDto?> GetByCpfAsync(string Cpf, CancellationToken cancellationToken);
    }
}