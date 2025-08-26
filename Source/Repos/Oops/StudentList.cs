using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oops
{
    public class Students
    {
        public List<int> studentId = new List<int>();
        public List<string> studentName = new List<string>();
        public List<int> studentAge = new List<int>();
        public List<bool> studentIsActive = new List<bool>();

    }

    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
    }
}
