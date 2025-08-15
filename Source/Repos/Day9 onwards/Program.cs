using Oops;
using System;
using System.IO;
using System.IO.Enumeration;

namespace Day9_onwards
{
    /* C# Generics:
     * create a single class or method that can be used with different types of data.
     * it is used to create an class of any datatype
     * to define  a generic Class we use " < > " 
     * 
     * 
     * 
     * Int the below example the letter T inside < > is called type Parameter
     * while Createing the instance of the class , we specify the data type of the object which replaces the Type Parameter
     */
    public class Student<T>
    {
        //Defining The Constructor
        public T Data;
        public Student(T Data) 
        { 
            this.Data = Data;
            Console.WriteLine("Data Fetched: {0} ", this.Data);
        }

    }
    public static class Program
    {
        
        static void Main(string[] args)
        {
            //Generic Class Object Instance Creation with Specified Datatypes
            Student<string> studentName = new Student<string>("Name");
            Student<int> studentId = new Student<int>(11);

        }

    }
}