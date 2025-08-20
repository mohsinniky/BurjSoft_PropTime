//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Day9_onwards
//{
//    public class Collections
//    {
//        static void Main(string[] args)
//        {
//            ////Creating A list  
//            //List<string> subjects = new List<string>() { "English", "Math" };


//            //Console.WriteLine("Subject 1:  {0}.", subjects[0]);
//            //Console.WriteLine("Subject 2:  {0}.", subjects[1]);
//            //Console.WriteLine("Subject List Items Count:  {0}", subjects.Count);
//            //Console.WriteLine("Subject List Items Count:  {0}", subjects.Capacity);

//            //// Adding Items
//            //subjects.Add("Physics");
//            //subjects.Add("Chemistry");
//            //subjects.Add("Biology");

//            //// Looping thorugh the list
//            //foreach(var subject in subjects)
//            //{
//            //    Console.WriteLine($"Subject Number {subject}");
//            //}

//            //Console.WriteLine("Subject List Items Count Before Removing:  {0}", subjects.Count);


//            //// Removing Item
//            //subjects.Remove("Chemistry");

//            //// remove by specific index
//            //subjects.RemoveAt(2);

//            //// Insert Items
//            //subjects.Insert(2, "Geography");

//            //// Looping thorugh the list
//            //for (int i = 0; i < subjects.Count; i++)
//            //{
//            //    Console.WriteLine("Subject Number {0}:  {1}", i, subjects[i]);
//            //}

//            //                                                                            //Methods
//            //// AddRange
//            //subjects.AddRange(subjects);
//            //// AsReadOnly
//            //IList<string> readlist = subjects.AsReadOnly();
//            ////readlist.Add("testing"); // this causes Exception Error
//            //// Random Integer List
//            //List<int> randomInts = new List<int>() {1,4,51,62,3,2,8,9,11,15,53};
//            //// Sort
//            //randomInts.Sort();
//            ////BinarySearch
//            //Console.WriteLine("Binary Search:  "+randomInts.BinarySearch(51));
//            //// Reverse
//            //randomInts.Reverse();
//            ////Contains
//            //Console.WriteLine("Contains(4):  " + randomInts.Contains(4));
//            //// IndexOf
//            //Console.WriteLine("Index OF:  " + randomInts.IndexOf(4));
//            //// Max
//            //Console.WriteLine("Max:  " + randomInts.Max());
//            //// Min
//            //Console.WriteLine("Min:  " + randomInts.Min());
//            //// Sum
//            //Console.WriteLine("Sum:  " + randomInts.Sum());
//            //// IndexOf
//            //Console.WriteLine("IndexOF:  " + randomInts.IndexOf(51));
//            //// LastIndexOf
//            //Console.WriteLine("LastIndexOF:  " + randomInts.LastIndexOf(2));
//            //// Find
//            //Console.WriteLine("Find:  " + randomInts.Find(x => x > 50));
//            //// FindAll
//            //Console.WriteLine("FindAll:  " + randomInts.FindAll(x => x > 50));
//            //// FindIndex
//            //Console.WriteLine("FindIndex:  " + randomInts.FindIndex(x => x > 50));
//            //// FindLastIndex
//            //Console.WriteLine("FindLastIndex:  " + randomInts.FindLastIndex(x => x > 50));
//            //// Exists
//            //Console.WriteLine("Exists:  " + randomInts.Exists(x => x > 50));
//            //// ToArray
//            //int[] array = randomInts.ToArray();
//            //// ToString
//            //Console.WriteLine("ToString:  " + randomInts.ToString());
//            //// Clear
//            ////randomInts.Clear();

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

//            //// create an ArrayList
//            //ArrayList student = new ArrayList();

//            //// add elements to ArrayList
//            //student.Add("Jackson");
//            //student.Add(5);

//            //// display every element of myList 
//            //for (int i = 0; i < student.Count; i++)
//            //{
//            //    Console.WriteLine(student[i]);
//            //}

//            //// Changing the Value
//            //student[0] = "John";
//            //// Removing
//            //student.RemoveAt(1);
//            //student.Add("John2");
//            //// Methods
//            //// student.Clear();
//            //// student.Contains("John");
//            //// student.IndexOf("John");
//            //// student.Insert(0, "John");
//            //// student.Remove("John");
//            //// student.Reverse();
//            //// student.Sort();


//            ////// create a dictionary
//            //// Dictionary<dataType1, dataType2> dictionaryName = new Dictionary<dataType1, dataType2>();
//            //Dictionary<string, int> studentDictionary = new Dictionary<string, int>();

//            //// add elements to dictionary
//            //studentDictionary.Add("Jackson", 5);
//            //studentDictionary.Add("John", 6);

//            //// display every element of dictionary
//            //foreach (KeyValuePair<string, int> studentItem in studentDictionary)
//            //{
//            //    Console.WriteLine(studentItem.Key + " " + studentItem.Value);
//            //}
//            //// Here the elements of a dictionary are to be accessed by using the key
//            //// Changing the value
//            //studentDictionary["Jackson"] = 6;
//            //// Removing
//            //studentDictionary.Remove("Jackson");
//            //// studentDictionary.Clear();
//            //// ContainsKey, ContainsValue Method
//            //Console.WriteLine(studentDictionary.ContainsKey("John"));
//            //Console.WriteLine(studentDictionary.ContainsValue(5));
//            //// Keys, Values
//            //Console.WriteLine(studentDictionary.Keys);
//            //Console.WriteLine(studentDictionary.Values);


//            // Practice Program 1
//            List<string> studentNames = new List<string>() { "Mohsin", "Ahmed", "Ali" };
//            List<int> studentGrades = new List<int>() { 60, 70, 80 };
//            double averageGrade = 0;
//            foreach (int grade in studentGrades)
//            {
//                averageGrade += grade;
//            }
//            averageGrade /= studentGrades.Count;
//            for (int i = 0; i < studentNames.Count; i++)
//            {
//                Console.WriteLine("Student Name: {0} and his Grade: {1}", studentNames[i], studentGrades[i]);
//            }
//            Console.WriteLine("Average Grade: " + averageGrade);

//            Console.WriteLine("Student With the Highest Grades is: {0}", studentNames[studentGrades.IndexOf(studentGrades.Max())]);



//            ////Practice Program 2
//            //ArrayList taskArrayList = new ArrayList() { 10, "Hello", 3.14, "World" };
//            //List<string> stringListForP2 = new List<string>();
//            //foreach (object item in taskArrayList)
//            //{
//            //        //Console.WriteLine(item.GetType());
//            //    if (item.GetType().Equals(typeof(string)))
//            //    {
//            //        // Adding string elements to stringList
//            //        stringListForP2.Add((string)item);
//            //    }
//            //}

//            //foreach (string values in stringListForP2)
//            //{
//            //    Console.WriteLine(values); 
//            //}

//            ////Practice Program 3
//            //Dictionary<string, List<string>> instituteDictionary = new Dictionary<string, List<string>>((StringComparer.OrdinalIgnoreCase));
//            ////Adding Key And values in the Dictionary
//            //instituteDictionary.Add("IT", new List<string>() { "Mohsin", "Raza", "Ahmed" });
//            //instituteDictionary.Add("CS", new List<string>() { "Hamza", "Hafiz", "Ali" });
//            //Console.WriteLine("Enter the Department Name");
//            //string? userInput = Console.ReadLine();
//            //if (instituteDictionary.ContainsKey(userInput))
//            //{
//            //    foreach (string name in instituteDictionary[userInput])
//            //    {
//            //        Console.WriteLine(name);
//            //    }
//            //}
//            //else
//            //{
//            //    Console.WriteLine("Entered Department Not Found");
//            //}

//            //Practice Program 4
//            List<string> products = new List<string> { "Apple", "Milk", "Bread" };
//            Dictionary<string, double> prices = new Dictionary<string, double>() { { "Apple", 0.50 }, { "Milk", 1.20 } };

//            for (int i = 0; i < products.Count; i++)
//            {
//                Console.WriteLine($"{i + 1}. {products[i]}");
//            }
//            //Choosing
//            Console.WriteLine("Choose The Item You want to Check");
//            int choice = Convert.ToInt32(Console.ReadLine()); // Assume user chose 1 (Apple)
//            string selectedProduct = products[choice - 1];

//            // Check if it's in the price dictionary
//            if (prices.ContainsKey(selectedProduct))
//            {
//                Console.WriteLine($"Price of {selectedProduct}: ${prices[selectedProduct]}");
//            }
//            else
//            {
//                Console.WriteLine($"Sorry, {selectedProduct} is not priced yet.");
//            }

//        }
//    }
//}
