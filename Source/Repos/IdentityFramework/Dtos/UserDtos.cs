using System.ComponentModel.DataAnnotations;

namespace IdentityFramework.Models
{
    // For GET /api/users (list with pagination)
    public class UserListDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    // For POST /api/users (create)
    public class UserCreateDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, StringLength(100)]
        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; } = null!;

        public bool IsActive { get; set; } = true;
        public bool MarkEmailConfirmed { get; set; }
    }

    // For PUT /api/users/{id} (update)
    public class UserUpdateDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, StringLength(100)]
        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool IsActive { get; set; }
        public bool EmailConfirmed { get; set; }

        [Required]
        public string ConcurrencyStamp { get; set; } = null!;
    }

    // For GET /api/users/{id} (details)
    public class UserDetailsDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public List<string> Roles { get; set; } = new();
        public List<string> Claims { get; set; } = new();
    }

    // Pagination
    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class UserFilterDto
    {
        public string? Search { get; set; }
        public bool? IsActive { get; set; }
        public bool? EmailConfirmed { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}