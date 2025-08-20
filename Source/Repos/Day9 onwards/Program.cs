using Oops;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Enumeration;
using System.Linq.Expressions;

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
            // list  
            List<string> subjects = new List<string>() { "English", "Math" };

            // Adding Items
            subjects.Add("Physics");
            subjects.Add("Chemistry");
            subjects.Add("Biology");

            // Insert Items
            subjects.Insert(2, "Geography");

            //ForEach Loop
            subjects.ForEach(x => Console.WriteLine(x));

            // Random Integer List
            List<int> randomInts = new List<int>() { 1, 4, 51, 62, 3, 2, 8, 9, 11, 15, 53 };
            // Sort
            randomInts.Sort();
            //LambdaExpression Methods
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
            // Select
            List<int> list = new List<int>(randomInts.Select(x => x * 2));
            Console.WriteLine("Select: ");
            list.ForEach(x => Console.WriteLine(x));
            //FirstAndDefault
            Console.WriteLine("First:  " + randomInts.First(x => x > 50));
            Console.WriteLine("FirstOrDefault:  " + randomInts.FirstOrDefault(x => x > 50));
            Console.WriteLine("FirstOrDefault For > 100:  " + randomInts.FirstOrDefault(x => x > 100));
            //LastAndDefault
            Console.WriteLine("Last:  " + randomInts.Last(x => x > 50));
            Console.WriteLine("LastOrDefault:  " + randomInts.LastOrDefault(x => x > 50));






            ////NestedList
            //List<List<int>> nestedList = new List<List<int>>();
            //// Create inner lists
            //List<int> innerList1 = new List<int> { 1, 2, 3 };
            //List<int> innerList2 = new List<int> { 4, 5 };
            //// Add inner lists to the nested list
            //nestedList.Add(innerList1);
            //nestedList.Add(innerList2);
            //// ForEach
            //foreach (List<int> innerList in nestedList)
            //{
            //    foreach (int item in innerList)
            //    {
            //        Console.WriteLine("NestedListItem:  " + item);
            //    }
            //}



        }
    }
}