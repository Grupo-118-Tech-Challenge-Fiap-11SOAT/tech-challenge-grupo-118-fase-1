using Common.Dto.Customers;
using Common.Interfaces.Customer.Presenter;

namespace TechChallengeFastFood.CleanArch.Presentation.Presenters.Customer;

public class CustomerPresenter : ICustomerPresenter
{
    public static ICustomerPresenter Create()
    {
        return new CustomerPresenter();
    }

    public List<CustomerResponseDto> Convert(List<Domain.Entities.Customer.Entities.Customer> customers)
    {
        if (customers is null || customers.Count == 0)
            return null;

        var customerDtos = new List<CustomerResponseDto>();

        customers.ToList().ForEach(customer => { customerDtos.Add(Convert(customer)); });

        return customerDtos;
    }

    public CustomerResponseDto Convert(Domain.Entities.Customer.Entities.Customer customer)
    {
        if (customer is null)
            return null;

        return new CustomerResponseDto(
            customer.Id,
            customer.Cpf,
            customer.Name,
            customer.Surname,
            customer.Email,
            customer.BirthDay,
            customer.IsActive);
    }
}