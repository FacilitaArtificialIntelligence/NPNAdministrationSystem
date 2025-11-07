using AdministrationSystem.Domain.Users;

namespace AdministrationSystem.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
