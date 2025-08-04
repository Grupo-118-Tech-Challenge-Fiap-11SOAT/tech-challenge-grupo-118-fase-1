using Common.Dto.Employee;

namespace Common.Interfaces.Employee.Presenter;

public interface IEmployeePresenter
{
    List<EmployeeResponseDto> Convert(List<TechChallengeFastFood.CleanArch.Domain.Entities.Employee.Entities.Employee> employees);
    EmployeeResponseDto Convert(TechChallengeFastFood.CleanArch.Domain.Entities.Employee.Entities.Employee employee);
}
