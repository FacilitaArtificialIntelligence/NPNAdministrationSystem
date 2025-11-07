using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Models;
using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Domain.Common.Interfaces;
using AdministrationSystem.Domain.Users;

namespace AdministrationSystem.Application.Users.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IUsersRepository usersRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await _usersRepository.ExistsByEmailAsync(command.Email))
        {
            return Error.Conflict("User.Email", "User already exists");
        }

        var passwordHashResult = _passwordHasher.HashPassword(command.Password);
        if (passwordHashResult.IsError)
        {
            return passwordHashResult.Errors;
        }

        var user = new User(
            command.UserName,
            command.FullName,
            command.Email,
            passwordHashResult.Value,
            command.Role,
            command.DataNascimento,
            command.CPF,
            command.Telefone,
            command.CEP,
            command.Address
        );

        await _usersRepository.CreateUserAsync(user);
        await _unitOfWork.CommitChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
