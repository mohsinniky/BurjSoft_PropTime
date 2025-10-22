using IdentityFramework.ViewModels;
using IdentityFramework.ViewModels.Users;
using Microsoft.AspNetCore.Identity;

namespace IdentityFramework.Services
{
    public interface IUserService
    {
        Task<PagedResult<UserListItemViewModel>> GetUsersAsync(UserListFilterViewModel filter);
        Task<(IdentityResult Result, Guid? UserId)> CreateAsync(UserCreateViewModel model);
        Task<UserEditViewModel?> GetForEditAsync(Guid id);
        Task<IdentityResult> UpdateAsync(UserEditViewModel model);
        Task<UserDetailsViewModel?> GetDetailsAsync(Guid id);
        Task<IdentityResult> DeleteAsync(Guid id);
        Task<UserRolesEditViewModel?> GetRolesForEditAsync(Guid userId);
        Task<IdentityResult> UpdateRolesAsync(Guid userId, IEnumerable<Guid> selectedRoleIds);
    }
}
