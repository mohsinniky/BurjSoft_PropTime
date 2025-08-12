using Oops;
using System;
using System.IO;
using System.IO.Enumeration;

namespace Day9_onwards
{
    //Public Keyword being used below is an access modifier, which defines a members of class Accessibility
    //class childClass : ParentClass  // Inheraitance


    /*           Abstraction:
     *           - hiding certain details and showing only essential information
     *           - can be achieved with either abstract classes or interfaces
     *           Abstract class:
     *           - a restricted class that cannot be used to create objects 
     *              (to access it, it must be inherited from another class)
     *           - During Inheritance Can't Use Public Keyword
     * 
     * 
     * 
     * 
     * 
     */
    //Abstraction Method and inheriting
    abstract class Animal
    {
        public abstract void animalSound();
        public void sleep()
        {
            Console.WriteLine("Zzz");
        }
    }

    class Dog : Animal
    {
        public override void animalSound()
        {
            Console.WriteLine("Woof Woof!");
        }
    }

    //Practice of Interfaces
    interface IFirstInterface
    {
        void myMethod();
    }

    interface ISecondInterface
    {
        void myOtherMethod();
    }

    // Inheriting
    class DemoClass : IFirstInterface, ISecondInterface
    {
        public void myMethod()
        {
            Console.WriteLine("Some text..");
        }
        public void myOtherMethod()
        {
            Console.WriteLine("Some other text...");
        }
    }


    public static class Program
    {    

        static void Main(string[] args)
        {

            Dog myDog = new Dog();
            myDog.animalSound();

        }


    }
}