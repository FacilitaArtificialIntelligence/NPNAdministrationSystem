using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using AdministrationSystem.Contracts.Users;
using AdministrationSystem.Application.Users.Commands.Register;
using AdministrationSystem.Application.Common.Models;
using AdministrationSystem.Application.Authentication.Queries.LoginUser;

namespace AdministrationSystem.Api.Controllers;

[Route("api/[controller]")]
public class UsersController : ApiController
{
    private readonly ISender _mediator;

    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var command = new RegisterUserCommand(
            request.UserName,
            request.FullName,
            request.Email,
            request.Password,
            request.Role,
            request.DataNascimento,
            request.CPF,
            request.PhoneNumber,
            request.CEP,
            request.Address);

        var result = await _mediator.Send(command);

        return result.Match(
            userResult => Ok(MapToUserResponse(userResult)),
            Problem);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        var query = new LoginUserQuery(request.Email, request.Password);

        var result = await _mediator.Send(query);

        if (result.IsError && result.FirstError.Type == ErrorType.Unauthorized)
        {
            return Problem(
                detail: result.FirstError.Description,
                statusCode: StatusCodes.Status401Unauthorized);
        }

        return result.Match(
            userResult => Ok(MapToUserResponse(userResult)),
            Problem);
    }

    private static UserResponse MapToUserResponse(AuthenticationResult result)
    {
        return new UserResponse(
            result.User.UserId,
            result.User.UserName,
            result.User.FullName,
            result.User.Email,
            result.User.Role,
            result.User.DataNascimento,
            result.User.CPF,
            result.User.PhoneNumber,
            result.User.CEP,
            result.User.Address,
            result.Token
        );
    }
}
