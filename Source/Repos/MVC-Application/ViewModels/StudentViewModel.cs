namespace MVC_Application.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string CoursesDisplay { get; set; }


        // For course enrollment in forms
        public List<int> SelectedCourseIds { get; set; } = new List<int>();
    }
}
