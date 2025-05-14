namespace Application.Base.Dtos;

public class PersonDto : BaseDto
{
    public string Cpf { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDay { get; set; }
}

