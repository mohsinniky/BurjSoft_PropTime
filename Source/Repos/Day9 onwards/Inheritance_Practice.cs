//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Day9_onwards
//{
//    //Public Keyword being used below is an access modifier, which defines a members of class Accessibility
//    //class childClass : ParentClass  // Inheraitance


//    /*           Abstraction:
//     *           - hiding certain details and showing only essential information
//     *           - can be achieved with either abstract classes or interfaces
//     *           Abstract class:
//     *           - a restricted class that cannot be used to create objects 
//     *              (to access it, it must be inherited from another class)
//     *           - Can contain fields, properties, and constructors
//     *           - During Inheritance Can't Use Public Keyword
//     *           Abstract Methods:
//     *           - methods without implementation
//     * 
//     * 
//     * 
//     * 
//     */
//    //Abstraction Method and inheriting
//    //abstract class Animal
//    //{
//    //    public abstract void animalSound();
//    //    public void sleep()
//    //    {
//    //        Console.WriteLine("Zzz");
//    //    }
//    //}

//    //class Dog : Animal
//    //{
//    //    public override void animalSound()
//    //    {
//    //        Console.WriteLine("Woof Woof!");
//    //    }
//    //}

//    //Practice of Interfaces
//    //It contains only method declarations(no implementation)
//    interface IFirstInterface
//    {
//        void myMethod();
//    }

//    interface ISecondInterface
//    {
//        void myOtherMethod();
//    }

//    // Inheriting
//    class DemoClass : IFirstInterface, ISecondInterface
//    {
//        public void myMethod()
//        {
//            Console.WriteLine("My First Method..");
//        }
//        public void myOtherMethod()
//        {
//            Console.WriteLine("My Second Method...");
//        }
//    }

//    enum WeekDays
//    {
//        Saturday,
//        Sunday,
//        Monday,
//        Tuesday,
//        WednesDay,
//        Thursday,
//        Friday
//    }

//    //the base class method overrides the derived class method
//    public class Animal  // Base class (parent) 
//    {
//        public virtual void animalSound()
//        {
//            Console.WriteLine("The animal makes a sound");
//        }
//    }

//    class Dog : Animal  // Derived class (child) 
//    {
//        public override void animalSound()
//        {
//            Console.WriteLine("The dog says: bow wow");
//        }
//    }
//    class Base
//    {
//        public void Show() => Console.WriteLine("Base");
//    }
//    class Derived : Base
//    {
//        public new void Show() => Console.WriteLine("Derived");
//    }
//    public class Inheritance_Practice
//    {
//        //static void checkAge(int age)
//        //{
//        //    if (age < 18)
//        //    {
//        //        throw new ArithmeticException("Access denied - You must be at least 18 years old.");
//        //    }
//        //    else
//        //    {
//        //        Console.WriteLine("Access granted - You are old enough!");
//        //    }
//        //}

//        static void Main(string[] args)
//        {

//            //Dog myDog = new Dog();
//            //myDog.animalSound();
//            //myDog.sleep();

//            //DemoClass demoClass = new DemoClass(); 
//            //demoClass.myMethod();
//            //demoClass.myOtherMethod();

//            //WeekDays variable = WeekDays.Monday;
//            //Console.WriteLine((int)variable);
//            //checkAge(5);
//            //checkAge(20);

//            //PolyMorphism Code Sample
//            //Animal myAnimal = new Animal();  // Create a Animal object
//            //Animal myDog = new Dog();  // Create a Dog object

//            //myAnimal.animalSound();
//            //myDog.animalSound();

//            //Base obj = new Derived();
//            //obj.Show();

//        }

//    }
//}
