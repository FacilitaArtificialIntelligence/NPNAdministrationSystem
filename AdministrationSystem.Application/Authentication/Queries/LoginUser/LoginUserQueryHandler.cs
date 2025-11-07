using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Models;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Common.Interfaces;

namespace AdministrationSystem.Application.Authentication.Queries.LoginUser;
public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserQueryHandler(
        IUsersRepository usersRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByEmailAsync(query.Email);

        if (user is null || !user.IsCorrectPassword(query.Password, _passwordHasher))
        {
            return Error.Unauthorized(description: "Invalid credentials");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
