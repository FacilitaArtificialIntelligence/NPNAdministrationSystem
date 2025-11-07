using AdministrationSystem.Application.Common.Models;

namespace AdministrationSystem.Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}