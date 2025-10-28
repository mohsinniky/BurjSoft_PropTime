using Shared.Dtos;
using Shared.Dtos.Users;
using Microsoft.AspNetCore.Identity;

namespace APIPractice.Services
{
    public interface IUserService
    {
        Task<PagedResult<UserListItemResponseDto>> GetUsersAsync(UserListFilterResponseDto filter);
        Task<(IdentityResult Result, Guid? UserId)> CreateAsync(UserCreateResponseDto model);
        Task<UserEditResponseDto?> GetForEditAsync(Guid id);
        Task<IdentityResult> UpdateAsync(UserEditResponseDto model);
        Task<UserDetailsResponseDto?> GetDetailsAsync(Guid id);
        Task<IdentityResult> DeleteAsync(Guid id);

    }
}
