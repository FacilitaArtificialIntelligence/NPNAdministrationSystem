using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Users;
using AdministrationSystem.Infrastructure.Common.Persistance;

using Microsoft.EntityFrameworkCore;

namespace AdministrationSystem.Infrastructure.Users;

public class UsersRepository : IUsersRepository
{
    private readonly AdministrationSystemDBContext _context;

    public UsersRepository(AdministrationSystemDBContext context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }
}
