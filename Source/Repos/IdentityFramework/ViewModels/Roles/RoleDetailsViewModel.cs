namespace IdentityFramework.ViewModels.Roles
{
    public class RoleDetailsViewModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public bool IsActive { get; init; }
        public DateTime? CreatedOn { get; init; }
        public DateTime? ModifiedOn { get; init; }
        public PagedResult<UserInRoleViewModel> Users { get; init; } = new();
        public List<string> Claims { get; init; } = new();
    }
}
