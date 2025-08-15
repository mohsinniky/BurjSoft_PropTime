using Oops;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Enumeration;

namespace Day9_onwards
{
    /* C# Collections:
     * Collections are classes that provide an easy way to work with a g
     * System.Collections.Generic:
     *      classes provide the implementation of strongly typed entities like lists, stacks etc
     *      to create a generic collection.
     *      in this we store , Type Compatible Data elements
     *      Storing Different type of elements not allowed here 
     *      Classes Under this: List Stack SortedList Queue
     * System.Collections: 
     *      To create a non Generic Colloection
     *      Using this we can create classes where we can add data elements of multiple data types
     *      Classes Under this: ArrayList Hashtable
     * System.Collections.Concurrent:
     *      Classes that help to achieve thread-safe code
     *      Thread-Safe Code:
     *          There can be situations when multiple threads are trying to execute the same piece of code
     *          The code is said to be "thread-safe" if it can be executed correctly irrespective of multiple threads accessing concurrently.
     *      These are the classes to be used when multiple threads are accessing the collection
     *      Classes Under This: ConcurrentStack<T> ConcurrentQueue<T> ConcurrentDictionary<TKey,TValue>
     */



    public static class Program
    {

        static void Main(string[] args)
        {
            //Creating A list  
            List<string> subjects = new List<string>() { "English", "Math" };


            Console.WriteLine("Subject 1:  {0}.", subjects[0]);
            Console.WriteLine("Subject 2:  {0}.", subjects[1]);
            Console.WriteLine("Subject List Items Count:  {0}", subjects.Count);
            Console.WriteLine("Subject List Items Count:  {0}", subjects.Capacity);

            // Adding Items
            subjects.Add("Physics");
            subjects.Add("Chemistry");
            subjects.Add("Biology");

            // Looping thorugh the list
            foreach(var subject in subjects)
            {
                Console.WriteLine($"Subject Number {subject}");
            }

            Console.WriteLine("Subject List Items Count Before Removing:  {0}", subjects.Count);


            // Removing Item
            subjects.Remove("Chemistry");

            // remove by specific index
            subjects.RemoveAt(2);

            // Insert Items
            subjects.Insert(2, "Geography");

            // Looping thorugh the list
            for (int i = 0; i < subjects.Count; i++)
            {
                Console.WriteLine("Subject Number {0}:  {1}", i, subjects[i]);
            }

                                                                                        //Methods
            // AddRange
            subjects.AddRange(subjects);
            // AsReadOnly
            IList<string> readlist = subjects.AsReadOnly();
            //readlist.Add("testing"); // this causes Exception Error
            // Random Integer List
            List<int> randomInts = new List<int>() {1,4,51,62,3,2,8,9,11,15,53};
            // Sort
            randomInts.Sort();
            //BinarySearch
            Console.WriteLine("Binary Search:  "+randomInts.BinarySearch(51));
            // Reverse
            randomInts.Reverse();
            //Contains
            Console.WriteLine("Contains(4):  " + randomInts.Contains(4));
            // IndexOf
            Console.WriteLine("Index OF:  " + randomInts.IndexOf(4));
            // Max
            Console.WriteLine("Max:  " + randomInts.Max());
            // Min
            Console.WriteLine("Min:  " + randomInts.Min());
            // Sum
            Console.WriteLine("Sum:  " + randomInts.Sum());
            // IndexOf
            Console.WriteLine("IndexOF:  " + randomInts.IndexOf(51));
            // LastIndexOf
            Console.WriteLine("LastIndexOF:  " + randomInts.LastIndexOf(2));
            // Find
            Console.WriteLine("Find:  " + randomInts.Find(x => x > 50));
            // FindAll
            Console.WriteLine("FindAll:  " + randomInts.FindAll(x => x > 50));
            // FindIndex
            Console.WriteLine("FindIndex:  " + randomInts.FindIndex(x => x > 50));
            // FindLastIndex
            Console.WriteLine("FindLastIndex:  " + randomInts.FindLastIndex(x => x > 50));
            // Exists
            Console.WriteLine("Exists:  " + randomInts.Exists(x => x > 50));
            // ToArray
            int[] array = randomInts.ToArray();
            // ToString
            Console.WriteLine("ToString:  " + randomInts.ToString());
            // Clear
            //randomInts.Clear();

            //NestedList
            List<List<int>> nestedList = new List<List<int>>();
            // Create inner lists
            List<int> innerList1 = new List<int> { 1, 2, 3 };
            List<int> innerList2 = new List<int> { 4, 5 };
            // Add inner lists to the nested list
            nestedList.Add(innerList1);
            nestedList.Add(innerList2);
            // ForEach
            foreach (List<int> innerList in nestedList)
            {
                foreach (int item in innerList)
                {
                    Console.WriteLine("NestedListItem:  " + item);
                }
            }



            




        }
    }
}