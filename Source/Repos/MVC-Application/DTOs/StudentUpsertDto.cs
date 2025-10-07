using System.ComponentModel.DataAnnotations;

namespace MVC_Application.DTOs
{
    public class StudentUpsertDto
    {
        public int StudentId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<int> SelectedCourseIds { get; set; } = new List<int>();
    }
}
