using System.ComponentModel.DataAnnotations;

namespace IdentityFramework.ViewModels.Roles
{
    public class RoleClaimsEditViewModel
    {
        [Required(ErrorMessage = "Invalid Role.")]
        public Guid RoleId { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; } = string.Empty;
        // Only claims from Category = Role or Both (and active)
        public List<RoleClaimCheckboxItem> Claims { get; set; } = new();
    }
}