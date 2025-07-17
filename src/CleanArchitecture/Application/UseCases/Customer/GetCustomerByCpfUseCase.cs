using Common.Interfaces.Customer.Gateway;

namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

public class GetCustomerByCpfUseCase
{
    private readonly ICustomerGateway _customerGateway;

    public GetCustomerByCpfUseCase(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    public static GetCustomerByCpfUseCase Create(ICustomerGateway customerGateway)
    {
        return new GetCustomerByCpfUseCase(customerGateway);
    }

    public async Task<Domain.Entities.Customer.Entities.Customer?> ExecuteAsync(string cpf, CancellationToken cancellationToken)
    {
        return await _customerGateway.GetCustomerByCpf(cpf, cancellationToken);
    }
}
