
namespace CoreMVCTutorial.Models
{
        public class Grade
        {
            public int GradeID { get; set; }
            public string GradeName { get; set; }

            public List<Students> Students { get; set; }
        }
}
