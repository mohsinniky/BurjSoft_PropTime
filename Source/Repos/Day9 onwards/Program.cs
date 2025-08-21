

using Oops;

namespace Day9_onwards
{
    public static class Program
    {
        static int calculateSum(int x, int y)
        {
            return x + y;
        }

        // define a delegate
        public delegate int myDelegate(int num1, int num2);

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




        }
    }
}