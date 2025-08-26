

using Oops;
using System.Text.RegularExpressions;

namespace Day9_onwards
{
    public static class Program
    {
        static int calculateSum(int x, int y)
        {
            return x + y;
        }
        static void printMsg(string message)
        {
            Console.WriteLine(message);
        }

        // Factorial Method Recursive
        static int Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n - 1);
            }
        }



        // define a delegate
        public delegate int myDelegate(int num1, int num2);
        public delegate void printDelegate(string message);


        static void Main(string[] args)
        {
            //List<Employee?> employees;

            //employees = new List<Employee?>()
            //{
            //    new Employee() { Id = 1, Name =  "John", Age = 12, IsActive= true },
            //    null,
            //    new Employee() { Id = 3, Name =  "Michel", Age = 67, IsActive= true },
            //    new Employee() { Id = 5, Name =  null, Age = 67, IsActive= true },

            //};


            //var secondEmployees = employees.FirstOrDefault(x => x?.Id.Equals(3) ?? false);
            //var johnEmployees = employees.Where(x => x?.Name?.Contains("John") ?? false);
            //var agedEmployees = employees.Where(x => x?.Age >= 2 && x.Age < 20);
            //var activeEmployees = employees.Where(x => x?.IsActive ?? false).Select(x => x.Name);


            //List<string> FrutisList = new List<string>() { "Apple1", "Apple2", "Apple3", "Apple4" };

            //IEnumerable<string> iEnumerableFruitsList = FrutisList;
            //foreach (var item in iEnumerableFruitsList)
            //{
            //    Console.WriteLine(item);
            //}

            //IEnumerator<string> iEnumeratorFruitsList = iEnumerableFruitsList.GetEnumerator();

            //while (iEnumeratorFruitsList.MoveNext())
            //{
            //    Console.WriteLine(iEnumeratorFruitsList.Current);
            //}


            //Delegate
            myDelegate delegateVariableForSumMethod = new myDelegate(calculateSum);

            int result = delegateVariableForSumMethod(2, 3);
            Console.WriteLine(result);

            printDelegate displayPrint = new printDelegate(printMsg);
            displayPrint("Hello WOrld");

            //For a Delegate 3 things Must match with its pointed Method, i. Return Type, ii. Parameters type/Sequence iii. Parameter Modifiers if any
            //Used For: Callback Methods, Events Handling, Passing Method as parameter, Deligates can be chained together


            //Recursion Practice
            displayPrint("Enter Your Number for finding its Factorial: ");
            int numberInput = Convert.ToInt32(Console.ReadLine());

            int resultFactorial = Factorial(numberInput);
            displayPrint($"Factorial of {numberInput} is: {resultFactorial}");


            ////RegularExpression
            //string pattern = "[a-zA-Z0-9]+@[a-zA-Z0-9]+.[a-zA-Z0-9]+";
            //displayPrint("Enter Your Email: ");
            //string? email = Console.ReadLine();
            //Regex regexPattern = new Regex(pattern);
            ////regexPattern.

            //bool isValidEmail = regexPattern.IsMatch(email);
            //displayPrint($"Email is Valid: {isValidEmail}");


            //File Handling
            string writeText = "Hello World!";
            File.WriteAllText("filename.txt", writeText);
            string readText = File.ReadAllText("filename.txt");
            displayPrint(readText);


        }
    }
}