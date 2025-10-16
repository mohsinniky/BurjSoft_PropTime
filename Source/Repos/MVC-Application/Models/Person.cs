namespace MVC_Application.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }

        public Person()
        {
            // Default constructor for serialization
        }

        public Person(int id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Age = DateTime.Now.Year - birthDate.Year;
        }
    }
}
