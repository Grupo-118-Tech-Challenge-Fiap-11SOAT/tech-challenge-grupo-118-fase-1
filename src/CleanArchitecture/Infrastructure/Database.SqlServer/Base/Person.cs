namespace TechChallengeFastFood.CleanArch.Infrastructure.Database.Base;

public abstract class Person : BaseEntity
{
    public string Cpf { get; protected set; }
    public string Name { get; protected set; }
    public string Surname { get; protected set; }
    public string Email { get; protected set; }
    public DateOnly BirthDay { get; protected set; }
}