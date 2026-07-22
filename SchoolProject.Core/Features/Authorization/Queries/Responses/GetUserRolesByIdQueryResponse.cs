namespace SchoolProject.Core.Features.Authorization.Queries.Responses;

public record GetUserRolesByIdQueryResponse
{
    public int UserId { get; set; }
    public IList<RoleResponse> Roles { get; set; } = new List<RoleResponse>();
}

public record RoleResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool HasRole { get; set; } = false;
}