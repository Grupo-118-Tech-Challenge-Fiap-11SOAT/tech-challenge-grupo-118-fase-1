using TechChallengeFastFood.CleanArch.Domain.Entities.Base.Exceptions;

namespace TechChallengeFastFood.CleanArch.Domain.Entities.Employee.Exceptions;

/// <summary>
/// Exception thrown when a CPF (Cadastro de Pessoas Físicas) is null or empty.
/// </summary>
public class CpfNullOrEmptyException : DomainException
{
    /// <summary>
    /// Gets the error message indicating that the CPF was null or empty.
    /// </summary>
    public override string Message => "CPF was null or empty.";
}