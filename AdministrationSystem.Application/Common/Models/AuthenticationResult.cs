using AdministrationSystem.Domain.Users;

namespace AdministrationSystem.Application.Common.Models;
public record AuthenticationResult(
    User User,
    string Token
);