﻿namespace Application.Base.Dtos;

public abstract class PersonDto : BaseDto
{
    public string Cpf { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDay { get; set; }
}

