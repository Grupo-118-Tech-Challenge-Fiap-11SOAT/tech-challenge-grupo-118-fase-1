namespace TechChallengeFastFood.CleanArch.Application.UseCases.Customer;

public class GetCustomerByIdUseCase
{
    private readonly Common.Interfaces.Customer.Gateway.ICustomerGateway _customerGateway;
    public GetCustomerByIdUseCase(Common.Interfaces.Customer.Gateway.ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    public static GetCustomerByIdUseCase Create(Common.Interfaces.Customer.Gateway.ICustomerGateway customerGateway)
    {
        return new GetCustomerByIdUseCase(customerGateway);
    }

    public async Task<Domain.Entities.Customer.Entities.Customer?> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        return await _customerGateway.GetByIdAsync(id, cancellationToken);
    }
}
