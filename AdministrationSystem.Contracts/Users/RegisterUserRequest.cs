namespace AdministrationSystem.Contracts.Users;
public record RegisterUserRequest(
    string UserName,
    string FullName,
    string Email,
    string Password,
    int Role,
    DateOnly DataNascimento,
    string CPF,
    string PhoneNumber,
    string CEP,
    string Address);
