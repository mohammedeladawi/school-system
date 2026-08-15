namespace SchoolProject.Application.Features.Authorization.Queries.GetUserPermissionClaimsById;

public record GetUserPermissionClaimsByIdQueryResponse
{
    public int UserId { get; set; }
    public IList<PermissionClaims> UserPermissionClaims { get; set; } = new List<PermissionClaims>();
}

public record PermissionClaims
{
    public string Name { get; set; } = null!;
    public bool Value { get; set; } = false;
}