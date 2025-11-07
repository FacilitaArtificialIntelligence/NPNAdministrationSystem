namespace AdministrationSystem.Application.Common.Models;
public record CurrentUser(
    Guid UserId,
    int Role);