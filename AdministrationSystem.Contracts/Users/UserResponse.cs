namespace AdministrationSystem.Contracts.Users;
public record UserResponse(
    Guid UserId,
    string UserName,
    string FullName,
    string Email,
    int Role,
    DateOnly DataNascimento,
    string CPF,
    string Telefone,
    string CEP,
    string Address,
    string Token);
