using APIPractice.Models;
using APIPractice.Services;
using Microsoft.AspNetCore.Mvc;
using APIPractice.ViewModels.Users;

namespace APIPracticeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<UserListDto>>> GetUsers([FromQuery] UserFilterDto filter)
        {
            try
            {
                // Convert API filter to service filter
                var serviceFilter = new UserListFilterViewModel
                {
                    Search = filter.Search,
                    IsActive = filter.IsActive,
                    EmailConfirmed = filter.EmailConfirmed,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize
                };

                var result = await _userService.GetUsersAsync(serviceFilter);

                // Convert service result to API DTO
                var apiResult = new PagedResultDto<UserListDto>
                {
                    Items = result.Items.Select(u => new UserListDto
                    {
                        Id = u.Id,
                        Email = u.Email,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        PhoneNumber = u.PhoneNumber,
                        IsActive = u.IsActive,
                        EmailConfirmed = u.EmailConfirmed,
                        CreatedOn = u.CreatedOn
                    }).ToList(),
                    TotalCount = result.TotalCount,
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize
                };

                return Ok(apiResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsDto>> GetUser(Guid id)
        {
            try
            {
                var user = await _userService.GetDetailsAsync(id);
                if (user == null)
                    return NotFound(new { error = "User not found" });

                var userDto = new UserDetailsDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    DateOfBirth = user.DateOfBirth,
                    LastLogin = user.LastLogin,
                    IsActive = user.IsActive,
                    EmailConfirmed = user.EmailConfirmed,
                    CreatedOn = user.CreatedOn,
                    ModifiedOn = user.ModifiedOn,
                    Roles = user.Roles,
                    Claims = user.Claims
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user {UserId}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserCreateDto userDto)
        {
            try
            {
                var createModel = new UserCreateViewModel
                {
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    PhoneNumber = userDto.PhoneNumber,
                    DateOfBirth = userDto.DateOfBirth,
                    Password = userDto.Password,
                    IsActive = userDto.IsActive,
                    MarkEmailConfirmed = userDto.MarkEmailConfirmed
                };

                var (result, userId) = await _userService.CreateAsync(createModel);

                if (result.Succeeded)
                {
                    return CreatedAtAction(nameof(GetUser), new { id = userId }, new { id = userId });
                }

                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, UserUpdateDto userDto)
        {
            try
            {
                // Convert API DTO to service model
                var updateModel = new UserEditViewModel
                {
                    Id = id,
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    PhoneNumber = userDto.PhoneNumber,
                    DateOfBirth = userDto.DateOfBirth,
                    IsActive = userDto.IsActive,
                    EmailConfirmed = userDto.EmailConfirmed,
                    //ConcurrencyStamp = userDto.ConcurrencyStamp
                };

                var result = await _userService.UpdateAsync(updateModel);

                if (result.Succeeded)
                {
                    return Ok(new { message = "User updated successfully" });
                }

                if (result.Errors.Any(e => e.Code == "ConcurrencyFailure"))
                {
                    return Conflict(new { error = "Concurrency conflict - user was modified by another user" });
                }

                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user {UserId}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await _userService.DeleteAsync(id);

                if (result.Succeeded)
                {
                    return Ok(new { message = "User deleted successfully" });
                }

                if (result.Errors.Any(e => e.Code == "LastAdmin"))
                {
                    return BadRequest(new { error = "Cannot delete the last admin user" });
                }

                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user {UserId}", id);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}