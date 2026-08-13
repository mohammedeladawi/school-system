namespace SchoolProject.Core.Features.ApplicationRole.Queries.GetAllRoles;

public record GetAllRolesQueryResponse
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}