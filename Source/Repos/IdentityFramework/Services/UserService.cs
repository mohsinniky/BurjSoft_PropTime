using IdentityFramework.Data;
using IdentityFramework.Models;
using IdentityFramework.ViewModels;
using IdentityFramework.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityFramework.Services
{
    public class UserService : IUserService
    {
        private const int MaxPageSize = 100;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public UserService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        // Returns a paged list of users with filter/search.
        // Uses normalized columns (index-friendly) where possible for best performance.
        public async Task<PagedResult<UserListItemViewModel>> GetUsersAsync(UserListFilterViewModel filter)
        {
            var pageNumber = filter.PageNumber < 1 ? 1 : filter.PageNumber; // less than 1 not allowed 
            var pageSize = filter.PageSize < 1 ? 10 : (filter.PageSize > MaxPageSize ? MaxPageSize : filter.PageSize); // limit users per page and also prevent invalid values
            var query = _userManager.Users.AsNoTracking(); 

            //for the three filter criteria, we be using Iquery till we get the final query
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

            var total = await query.CountAsync(); //total count after applying filters
            var items = await query 
            .OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ThenBy(u => u.Email)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)

            .Select(u => new UserListItemViewModel
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
            })
            .ToListAsync(); //execute the query and get the list


            return new PagedResult<UserListItemViewModel>
            {
                Items = items,
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        // Creates a new user with password.
        // We rely on Identity's built-in uniqueness/validation (avoid extra pre-check round trip).
        public async Task<(IdentityResult Result, Guid? UserId)> CreateAsync(UserCreateViewModel model)
        {
            // builtin Retry Mechanism in EF
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync<(IdentityResult, Guid?)>(async () => //running the enclosed code
            {
                // Start an explicit transaction
                await using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    // Prepare a new ApplicationUser (keep UserName = Email for simplicity/consistency)
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
                    // Let Identity enforce password policy + unique constraints (inside the transaction)
                    var create = await _userManager.CreateAsync(user, model.Password);
                    if (!create.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return (create, null);
                    }
                    await transaction.CommitAsync();
                    return (IdentityResult.Success, user.Id);
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw; // re hrow the exception to be handled by upper layers like logs
                }
            });
        }
        // Loads user data for the Edit form (read-only).
        public async Task<UserEditViewModel?> GetForEditAsync(Guid id)
        {
            // AsNoTracking -> we don't need change tracking for display
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return null;
            return new UserEditViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive,
                EmailConfirmed = user.EmailConfirmed,
                ConcurrencyStamp = user.ConcurrencyStamp // used for optimistic concurrency in Update
            };
        }
        // Updates a user with optimistic concurrency check via ConcurrencyStamp.
        public async Task<IdentityResult> UpdateAsync(UserEditViewModel model)
        {
            // ExecutionStrategy adds resiliency (automatic retries for transient SQL errors)
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync<IdentityResult>(async () =>
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    var user = await _userManager.FindByIdAsync(model.Id.ToString());
                    if (user == null)
                    {
                        await transaction.RollbackAsync();
                        return IdentityResult.Failed(new IdentityError { Code = "NotFound", Description = "User not found." });
                    }

                    if (!string.Equals(user.ConcurrencyStamp, model.ConcurrencyStamp, StringComparison.Ordinal))
                    {
                        await transaction.RollbackAsync();
                        return IdentityResult.Failed(new IdentityError
                        {
                            Code = "ConcurrencyFailure",
                            Description = "This user was modified by another admin. Please reload and try again."
                        });
                    }
                    // If email changed, update both Email & UserName (Identity will SaveChanges inside the transaction)
                    if (!string.Equals(user.Email, model.Email, StringComparison.OrdinalIgnoreCase))
                    {
                        var emailResult = await _userManager.SetEmailAsync(user, model.Email.Trim());
                        if (!emailResult.Succeeded)
                        {
                            await transaction.RollbackAsync();
                            return emailResult;
                        }
                        var usernameResult = await _userManager.SetUserNameAsync(user, model.Email.Trim()); // updating the user name also as email is also username
                        if (!usernameResult.Succeeded)
                        {
                            await transaction.RollbackAsync();
                            return usernameResult;
                        }
                    }

                    // Update profile fields
                    user.FirstName = model.FirstName.Trim();
                    user.LastName = model.LastName?.Trim();
                    user.PhoneNumber = model.PhoneNumber;
                    user.DateOfBirth = model.DateOfBirth;
                    user.IsActive = model.IsActive;
                    user.EmailConfirmed = model.EmailConfirmed;
                    user.ModifiedOn = DateTime.UtcNow;
                    var update = await _userManager.UpdateAsync(user);
                    if (!update.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return update;
                    }
                    await transaction.CommitAsync();
                    return IdentityResult.Success;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }
        // Returns detailed view model including assigned roles.
        // Returns detailed view model including assigned roles.
        public async Task<UserDetailsViewModel?> GetDetailsAsync(Guid id)
        {
            // Read-only entity for display
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return null;

            // Identity API requires the user entity for role lookup
            var roles = await _userManager.GetRolesAsync(user);

            var claims = await _userManager.GetClaimsAsync(user);
            var claimTexts = claims
                .OrderBy(c => c.Type).ThenBy(c => c.Value)
                .Select(c => $"{c.Type}: {c.Value}")
                .ToList();

            return new UserDetailsViewModel
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
                Claims = claimTexts
            };
        }
        // Deletes a user with a guard to prevent removing the last Admin.
        public async Task<IdentityResult> DeleteAsync(Guid id)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync<IdentityResult>(async () =>
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    var user = await _userManager.FindByIdAsync(id.ToString());
                    if (user == null)
                    {
                        await transaction.RollbackAsync();
                        return IdentityResult.Failed(new IdentityError { Code = "NotFound", Description = "User not found." });
                    }
                    // Safety: block deleting the last "Admin"
                    var adminRole = await _roleManager.FindByNameAsync("Admin");
                    if (adminRole != null)
                    {
                        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin"); // Checking if the user to be deleted is an Admin
                        if (isAdmin)
                        {
                            var anotherAdminExists = await _dbContext.Set<IdentityUserRole<Guid>>()
                            .AnyAsync(ur => ur.RoleId == adminRole.Id && ur.UserId != user.Id); //Comparing with other users in the same role

                            if (!anotherAdminExists)
                            {
                                await transaction.RollbackAsync();
                                return IdentityResult.Failed(new IdentityError
                                {
                                    Code = "LastAdmin",
                                    Description = "You cannot delete the last user in the 'Admin' role."
                                });
                            }
                        }
                    }
                    var delete = await _userManager.DeleteAsync(user);
                    if (!delete.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return delete;
                    }
                    await transaction.CommitAsync();
                    return IdentityResult.Success;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }
        // Builds the roles editor (checkbox list) with pre-checked assignments.
        public async Task<UserRolesEditViewModel?> GetRolesForEditAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()); //getting the user
            if (user == null)
                return null;

            var allRoles = await _roleManager.Roles //getting all the roles
            .AsNoTracking()
            .OrderBy(r => r.Name)
            .Where(r => r.IsActive)
            .ToListAsync();

            var assignedRoles = await _userManager.GetRolesAsync(user); //getting the roles assigned to the user

            var userRolesEditViewModel = new UserRolesEditViewModel
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Roles = allRoles.Select(role => new RoleCheckboxItem
                {
                    RoleId = role.Id,
                    RoleName = role.Name!,
                    Description = role.Description,
                    IsSelected = assignedRoles.Contains(role.Name!, StringComparer.OrdinalIgnoreCase)

                }).ToList()
            };
            return userRolesEditViewModel;
        }
        // Updates a user's roles using batched operations
        public async Task<IdentityResult> UpdateRolesAsync(Guid userId, IEnumerable<Guid> selectedRoleIds)
        {  
            var strategy = _dbContext.Database.CreateExecutionStrategy(); //Execution strategy Creation
            return await strategy.ExecuteAsync<IdentityResult>(async () => //Executing the enclosed code
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync(); //Starting a transaction
                try
                {
                    var user = await _userManager.FindByIdAsync(userId.ToString());
                    if (user == null)
                    {
                        await transaction.RollbackAsync();
                        return IdentityResult.Failed(new IdentityError { Code = "NotFound", Description = "User not found." });
                    }
                    // Normalize and de-duplicate incoming IDs
                    var ids = (selectedRoleIds ?? Enumerable.Empty<Guid>()).Distinct().ToList(); //getting the ids from the selectedRoleIds, else just an empty list

                    var selectedRoleNames = /* IF */ (ids.Count == 0) ? new List<string>() :/* ELSE */ await _roleManager.Roles
                    .AsNoTracking()
                    .Where(r => ids.Contains(r.Id))
                    .Select(r => r.Name!)
                    .ToListAsync();
                    if (selectedRoleNames.Count != ids.Count)
                    {
                        await transaction.RollbackAsync();
                        return IdentityResult.Failed(new IdentityError
                        {
                            Code = "RoleNotFound",
                            Description = "One or more selected roles do not exist."
                        });
                    }

                    // Current roles
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    // Computing differences here 
                    var current = new HashSet<string>(currentRoles, StringComparer.OrdinalIgnoreCase);
                    var target = new HashSet<string>(selectedRoleNames, StringComparer.OrdinalIgnoreCase);

                    var toAdd = target.Except(current, StringComparer.OrdinalIgnoreCase).ToList();
                    //toAdd = CustomerSupport Vendor

                    var toRemove = current.Except(target, StringComparer.OrdinalIgnoreCase).ToList();
                    //toRemove = User

                    if (toAdd.Count() == 0 && toRemove.Count() == 0)
                    {
                        await transaction.CommitAsync(); // nothing to do
                        return IdentityResult.Success;
                    }
                    if (toAdd.Count() > 0)
                    {
                        var add = await _userManager.AddToRolesAsync(user, toAdd);
                        if (!add.Succeeded)
                        {
                            await transaction.RollbackAsync();
                            return add;
                        }
                    }
                    if (toRemove.Count() > 0)
                    {
                        var rem = await _userManager.RemoveFromRolesAsync(user, toRemove);
                        if (!rem.Succeeded)
                        {
                            await transaction.RollbackAsync();
                            return rem;
                        }
                    }
                    await transaction.CommitAsync();
                    return IdentityResult.Success;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<UserClaimsEditViewModel?> GetClaimsForEditAsync(Guid userId)
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null;

            // Get all active claims that can be assigned to Users or Both
            var allClaims = await _dbContext.ClaimMasters
                .AsNoTracking()
                .Where(c => c.IsActive && (c.Category == "User" || c.Category == "Both"))
                .OrderBy(c => c.ClaimType).ThenBy(c => c.ClaimValue)
                .ToListAsync();

            // Read current user claims from Identity
            var currentClaims = await _userManager.GetClaimsAsync(user);

            var vm = new UserClaimsEditViewModel
            {
                UserId = user.Id,
                UserName = user.UserName!,
                Claims = allClaims.Select(c => new UserClaimCheckboxItem
                {
                    ClaimId = c.Id,
                    ClaimType = c.ClaimType,
                    ClaimValue = c.ClaimValue,
                    Category = c.Category,
                    Description = c.Description,
                    IsSelected = currentClaims.Any(uc => uc.Type == c.ClaimType && uc.Value == c.ClaimValue)
                }).ToList()
            };

            return vm;
        }

        public async Task<IdentityResult> UpdateClaimsAsync(Guid userId, IEnumerable<Guid> selectedClaimIds)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Code = "UserNotFound", Description = "User not found." });

            // Only allow choosing from active ClaimMasters in Category = User or Both
            var allowedClaims = await _dbContext.ClaimMasters
                .Where(c => c.IsActive && (c.Category == "User" || c.Category == "Both"))
                .ToListAsync();

            // Selected Claims
            var selected = allowedClaims.Where(c => selectedClaimIds.Contains(c.Id))
                .Select(c => new Claim(c.ClaimType, c.ClaimValue))
                .ToList();

            // Current Claims
            var currentClaims = await _userManager.GetClaimsAsync(user);

            // Claims to remove (exist in current but not in selected)
            var claimsToRemove = currentClaims
                .Where(current => !selected.Any(s =>
                    s.Type == current.Type && s.Value == current.Value))
                .ToList();

            // Claims to add (exist in selected but not in current)
            var claimsToAdd = selected
                .Where(s => !currentClaims.Any(current =>
                    current.Type == s.Type && current.Value == s.Value))
                .ToList();

            // Remove only claims that need to be removed
            foreach (var claim in claimsToRemove)
            {
                await _userManager.RemoveClaimAsync(user, claim);
            }

            // Add only claims that need to be added
            foreach (var claim in claimsToAdd)
            {
                await _userManager.AddClaimAsync(user, claim);
            }

            return IdentityResult.Success;
        }
    }
}
