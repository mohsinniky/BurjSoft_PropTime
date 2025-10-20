using Microsoft.AspNetCore.Identity;
namespace IdentityFramework.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        // Extended property
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        //Audit Columns
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
