//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Day9_onwards
//{
//    public class LambdaExpressionPractice
//    {
//        static void Main(string[] args)
//        {
//            // list  
//            List<string> subjects = new List<string>() { "English", "Math" };

//            // Adding Items
//            subjects.Add("Physics");
//            subjects.Add("Chemistry");
//            subjects.Add("Biology");

//            // Insert Items
//            subjects.Insert(2, "Geography");

//            //ForEach Loop
//            subjects.ForEach(x => Console.WriteLine(x));

//            // Random Integer List
//            List<int> randomInts = new List<int>() { 1, 4, 51, 62, 3, 4, 4, 9, 11, 15, 53 };
//            // Sort
//            randomInts.Sort();
//            //LambdaExpression Methods
//            // Find
//            Console.WriteLine("Find:  " + randomInts.Find(x => x > 50));
//            // FindAll
//            Console.WriteLine("FindAll:  " + randomInts.FindAll(x => x > 50));
//            // FindIndex
//            Console.WriteLine("FindIndex:  " + randomInts.FindIndex(x => x > 50));
//            // FindLastIndex
//            Console.WriteLine("FindLastIndex:  " + randomInts.FindLastIndex(x => x > 50));
//            // Exists
//            Console.WriteLine("Exists:  " + randomInts.Exists(x => x > 50));
//            // Select
//            List<int> list = new List<int>(randomInts.Select(x => x * 2));
//            Console.WriteLine("Select: ");
//            list.ForEach(x => Console.WriteLine(x));
//            //FirstAndDefault
//            Console.WriteLine("First:  " + randomInts.First(x => x > 50));
//            Console.WriteLine("FirstOrDefault:  " + randomInts.FirstOrDefault(x => x > 50));
//            Console.WriteLine("FirstOrDefault For > 100:  " + randomInts.FirstOrDefault(x => x > 100));
//            //LastAndDefault
//            Console.WriteLine("Last:  " + randomInts.Last(x => x > 50));
//            Console.WriteLine("LastOrDefault:  " + randomInts.LastOrDefault(x => x > 50));
//            // DistinctBy
//            List<int> distinctList = new List<int>(randomInts.DistinctBy(x => x));
//            Console.WriteLine("DistinctBy: ");
//            distinctList.ForEach(x => Console.WriteLine(x));
//            // ExceptBy
//            List<int> excludedNumbers = new List<int> { 4 };
//            List<int> exceptList = new List<int>(randomInts.ExceptBy(excludedNumbers, x => x));
//            Console.WriteLine("ExceptBy: ");
//            exceptList.ForEach(x => Console.WriteLine(x));






//            ////NestedList
//            //List<List<int>> nestedList = new List<List<int>>();
//            //// Create inner lists
//            //List<int> innerList1 = new List<int> { 1, 2, 3 };
//            //List<int> innerList2 = new List<int> { 4, 5 };
//            //// Add inner lists to the nested list
//            //nestedList.Add(innerList1);
//            //nestedList.Add(innerList2);
//            //// ForEach
//            //foreach (List<int> innerList in nestedList)
//            //{
//            //    foreach (int item in innerList)
//            //    {
//            //        Console.WriteLine("NestedListItem:  " + item);
//            //    }
//            //}


//            ////Delegate
//            //static int calculateSum(int x, int y)
//            //{
//            //    return x + y;
//            //}
//            //public delegate int myDelegate(int x, int y);
//            //{

//            //}



//        }
//    }
//}
