using AdministrationSystem.Domain.Users;

namespace AdministrationSystem.Application.Common.Interfaces;
public interface IUsersRepository
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(Guid userId);
    Task CreateUserAsync(User user);
}
