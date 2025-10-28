using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Users
{
    public class UserRolesEditResponseDto
    {
        [Required(ErrorMessage = "Invalid user.")]
        public Guid UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;
        // Roles collection can be empty (no roles selected), so no Required here.
        public List<RoleCheckboxItemResponseDto> Roles { get; set; } = new();
    }
}
