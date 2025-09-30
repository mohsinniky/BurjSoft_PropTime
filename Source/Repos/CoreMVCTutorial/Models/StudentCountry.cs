namespace CoreMVCTutorial.Models
{
    public class StudentCountry
    {
        public int Id { get; set; }

        public string CountryName { get; set; }
        // Foreign Key for Students
        public int StudentId { get; set; }
        // Navigation property back to Student
        public Students Student { get; set; }


    }
}
