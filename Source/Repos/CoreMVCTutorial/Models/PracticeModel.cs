using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreMVCTutorial.Models
{
    public class PracticeModel
    {
        public string UserName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public List<string> SelectedRoles { get; set; } = new List<string>();
        public int UserId { get; set; }
        public string Password { get; set; }

        // For dropdown options
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> Genders { get; set; }
    }
}
