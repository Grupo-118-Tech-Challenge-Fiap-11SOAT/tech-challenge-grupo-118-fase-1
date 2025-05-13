using Application.Employee.Dtos;
using static Application.Reponse;

namespace Application.Employee.Responses;

public class EmployeeResponse : Response
{
    public EmployeeDto Data { get; set; }
}

