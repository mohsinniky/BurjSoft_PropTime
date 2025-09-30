using System.ComponentModel.DataAnnotations;
using static CoreMVCTutorial.Models.Grade;

namespace CoreMVCTutorial.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime CreatedDate { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }

        // One to One Relation
        public StudentCountry StudentCountry { get; set; }

    }
}
