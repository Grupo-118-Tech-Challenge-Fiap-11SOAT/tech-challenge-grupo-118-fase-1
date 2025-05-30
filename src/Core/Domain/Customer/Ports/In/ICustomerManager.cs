using Domain.Customer.Dtos;

namespace Domain.Customer.Ports.In
{
    public interface ICustomerManager
    {
        Task<CustomerDto> CreateAsync(CustomerDto customerDto, CancellationToken cancellationToken);
        Task<CustomerDto> UpdateAsync(CustomerDto customerDto, CancellationToken cancellationToken);
        Task<CustomerDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<CustomerDto?>  GetByCpfAsync(string Cpf, CancellationToken cancellationToken);
    }
}
