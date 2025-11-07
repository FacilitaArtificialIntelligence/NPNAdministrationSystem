using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Models;

namespace AdministrationSystem.Application.Users.Commands.Register;

public record RegisterUserCommand(
    string UserName,
    string FullName,
    string Email,
    string Password,
    int Role,
    DateOnly DataNascimento,
    string CPF,
    string Telefone,
    string CEP,
    string Address
) : IRequest<ErrorOr<AuthenticationResult>>;
