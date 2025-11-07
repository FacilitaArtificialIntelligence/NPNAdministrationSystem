using AdministrationSystem.Application.Common.Interfaces;
using AdministrationSystem.Application.Common.Models;

using Throw;

namespace AdministrationSystem.Api.Services;

public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        _httpContextAccessor.HttpContext.ThrowIfNull();

        var id = GetClaimValues("id")
            .Select(Guid.Parse)
            .First();

        var role = GetClaimValues("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
            .Select(int.Parse)
            .First();

        return new CurrentUser(UserId: id, Role: role);
    }

    private IReadOnlyList<string> GetClaimValues(string claimType)
    {
        return _httpContextAccessor.HttpContext!.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();
    }
}
