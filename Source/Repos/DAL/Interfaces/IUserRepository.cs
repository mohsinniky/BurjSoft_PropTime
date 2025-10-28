using System;
using System.Collections.Generic;
using Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace DAL.Interfaces
{
    internal interface IUserRepository
    {
        IQueryable<ApplicationUser> GetQueryable();
        Task<ApplicationUser?> GetByIdAsync(Guid id);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
        Task<List<string>> GetUserRolesAsync(ApplicationUser user);
        Task<IdentityResult> UpdateUserRolesAsync(ApplicationUser user, IEnumerable<string> rolesToAdd, IEnumerable<string> rolesToRemove);
    }
}
