////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;

////namespace Day9_onwards
////{
////    /* C# Generics:
//// * create a single class or method that can be used with different types of data.
//// * it is used to create an class of any datatype
//// * to define  a generic Class we use " < > " 
//// * With the help of generics in C#, we can write code that will work with different types of data
//// * The collections framework uses the concept of generics
//// * 
//// * 
//// * Int the below example the letter T inside < > is called type Parameter
//// * while Createing the instance of the class , we specify the data type of the object which replaces the Type Parameter

//// for specify T type like below we set 'class' keyword
//public class Generic<T> where T : class
//{

//}

//public class GenericWithParameters<TKey, TValue>
//{

//}

//public class Room
//{

//}




//// */
////    public class Student<T> 
////    {
////        //Defining The Constructor
////        //public T Data;
////        //public Student(T Data) 
////        //{ 
////        //    this.Data = Data;
////        //    Console.WriteLine("Data Fetched: {0} ", this.Data);
////        //}

////        // define a generics method that displays the passed data  
////        public void displayData(T data)
////        {
////            Console.WriteLine("The data passed is: " + data);
////        }

////        //// defining a generics method that returns T type value    
////        //public T displayData(T data)
////        //{
////        //    return data;
////        //}

////    }
//public static class Generic_Class_Practice
//{

//    static void Main(string[] args)
//    {
//        Generic<string> obj = new Generic<string>();
//        Generic<Room> roomObj = new Generic<Room>();

//        GenericWithParameters<string, int> withTwoParamObj = new GenericWithParameters<string, int>();


//        //Generic Class Object Instance Creation with Specified Datatypes
//        //Student<string> studentName = new Student<string>("Name");
//        //Student<int> studentId = new Student<int>(11);

//        //Generic Class Instance without parameterized Constructor;
//        //Student<string> studentName = new Student<string>();
//        //Student<int> studentId = new Student<int>();
//        //studentName.displayData("MohsinRaza");
//        //studentId.displayData(19);

//    }

//}
////}
