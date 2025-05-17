namespace Domain.Base.Entities;

public abstract class Person : BaseEntity
{
    public string Cpf { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDay { get; set; }
}
