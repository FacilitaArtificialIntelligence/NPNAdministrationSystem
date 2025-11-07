using ErrorOr;
using MediatR;
using AdministrationSystem.Application.Common.Models;

namespace AdministrationSystem.Application.Authentication.Queries.LoginUser;

public record LoginUserQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;