using AdministrationSystem.Domain.Common.Interfaces;

namespace AdministrationSystem.Domain.Users; 

public class User
{
    public Guid UserId { get; private set; }
    public string UserName { get; private set; } = null!;
    public string FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    private readonly string _passwordHash = null!;
    public int Role { get; private set; }
    public DateOnly DataNascimento { get; private set; }
    public string CPF { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public string CEP { get; private set; } = null!;
    public string Address { get; private set; } = null!;

    public User(
        string userName,
        string fullName,
        string email,
        string passwordHash,
        int role,
        DateOnly dataNascimento,
        string cpf,
        string phoneNumber,
        string cep,
        string address,
        Guid? userId = null)
    {
        UserId = userId ?? Guid.NewGuid();
        UserName = userName;
        FullName = fullName;
        Email = email;
        _passwordHash = passwordHash;
        Role = role;
        DataNascimento = dataNascimento;
        CPF = cpf;
        PhoneNumber = phoneNumber;
        CEP = cep;
        Address = address;
    }

    public bool IsCorrectPassword(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, _passwordHash);
    }

    private User() { }
}
