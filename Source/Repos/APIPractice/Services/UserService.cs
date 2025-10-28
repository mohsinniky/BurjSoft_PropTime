// Services/UserService.cs
using APIPractice.Data;
using APIPractice.DAL.Interfaces;

using APIPractice.Dtos;
using APIPractice.Dtos.Users;
using APIPractice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APIPractice.Services
{
    public class UserService : IUserService
    {
        private const int MaxPageSize = 100;
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public UserService(
            IUserRepository userRepository,
            RoleManager<IdentityRole<Guid>> roleManager,
            ApplicationDbContext dbContext)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task<PagedResult<UserListItemResponseDto>> GetUsersAsync(UserListFilterResponseDto filter)
        {
            var pageNumber = filter.PageNumber < 1 ? 1 : filter.PageNumber;
            var pageSize = filter.PageSize < 1 ? 10 : (filter.PageSize > MaxPageSize ? MaxPageSize : filter.PageSize);

            var query = _userRepository.GetQueryable();

            // Apply filters (same logic)
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var search = filter.Search.Trim();
                var searchUpper = search.ToUpperInvariant();
                if (search.Contains('@'))
                {
                    query = query.Where(u => u.NormalizedEmail!.StartsWith(searchUpper));
                }
                else if (search.All(char.IsDigit))
                {
                    query = query.Where(u => (u.PhoneNumber ?? "").StartsWith(search));
                }
                else
                {
                    query = query.Where(u =>
                        (u.NormalizedUserName!.StartsWith(searchUpper))
                        || (u.FirstName ?? "").StartsWith(search)
                        || (u.LastName ?? "").StartsWith(search));
                }
            }

            if (filter.IsActive.HasValue)
                query = query.Where(u => u.IsActive == filter.IsActive.Value);

            if (filter.EmailConfirmed.HasValue)
                query = query.Where(u => u.EmailConfirmed == filter.EmailConfirmed.Value);

            var total = await query.CountAsync();
            var users = await query
                .OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ThenBy(u => u.Email)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = users.Select(u => new UserListItemResponseDto
            {
                Id = u.Id,
                Email = u.Email!,
                UserName = u.UserName!,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                IsActive = u.IsActive,
                EmailConfirmed = u.EmailConfirmed,
                CreatedOn = u.CreatedOn
            }).ToList();

            return new PagedResult<UserListItemResponseDto>
            {
                Items = items,
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<(IdentityResult Result, Guid? UserId)> CreateAsync(UserCreateResponseDto model)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName?.Trim(),
                Email = model.Email.Trim(),
                UserName = model.Email.Trim(),
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                IsActive = model.IsActive,
                EmailConfirmed = model.MarkEmailConfirmed,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };

            var result = await _userRepository.CreateUserAsync(user, model.Password);
            return (result, result.Succeeded ? user.Id : null);
        }

        public async Task<UserEditResponseDto?> GetForEditAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserEditResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive,
                EmailConfirmed = user.EmailConfirmed,
                ConcurrencyStamp = user.ConcurrencyStamp
            };
        }

        public async Task<IdentityResult> UpdateAsync(UserEditResponseDto model)
        {
            var user = await _userRepository.GetByIdAsync(model.Id);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Code = "NotFound", Description = "User not found." });

            // Update fields
            user.FirstName = model.FirstName.Trim();
            user.LastName = model.LastName?.Trim();
            user.PhoneNumber = model.PhoneNumber;
            user.DateOfBirth = model.DateOfBirth;
            user.IsActive = model.IsActive;
            user.EmailConfirmed = model.EmailConfirmed;
            user.ModifiedOn = DateTime.UtcNow;

            // Update email if changed
            if (!string.Equals(user.Email, model.Email, StringComparison.OrdinalIgnoreCase))
            {
                user.Email = model.Email.Trim();
                user.UserName = model.Email.Trim();
                user.NormalizedEmail = model.Email.Trim().ToUpperInvariant();
                user.NormalizedUserName = model.Email.Trim().ToUpperInvariant();
            }

            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<UserDetailsResponseDto?> GetDetailsAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            var roles = await _userRepository.GetUserRolesAsync(user);

            return new UserDetailsResponseDto
            {
                Id = user.Id,
                Email = user.Email!,
                UserName = user.UserName!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                LastLogin = user.LastLogin,
                IsActive = user.IsActive,
                EmailConfirmed = user.EmailConfirmed,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn,
                Roles = roles.OrderBy(r => r).ToList(),
                ConcurrencyStamp = user.ConcurrencyStamp!
            };
        }

        public async Task<IdentityResult> DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Code = "NotFound", Description = "User not found." });

            return await _userRepository.DeleteUserAsync(user);
        }
    }
}