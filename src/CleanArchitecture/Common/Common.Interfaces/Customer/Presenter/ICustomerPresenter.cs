using Common.Dto.Customers;

namespace Common.Interfaces.Customer.Presenter;

public interface ICustomerPresenter
{
    List<CustomerResponseDto> Convert(List<TechChallengeFastFood.CleanArch.Domain.Entities.Customer.Entities.Customer> customers);
    CustomerResponseDto Convert(TechChallengeFastFood.CleanArch.Domain.Entities.Customer.Entities.Customer customer);
}
