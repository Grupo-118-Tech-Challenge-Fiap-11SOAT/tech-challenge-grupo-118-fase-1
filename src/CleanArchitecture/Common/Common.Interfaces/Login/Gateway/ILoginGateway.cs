using Common.Dto.Employee;
using Common.Enums;

namespace Common.Interfaces.Login.Gateway;

public interface ILoginGateway
{
    string Login(int id, string name, EmployeeRole role);
    bool VerifyPassword(string password, string storedPassword);
}
