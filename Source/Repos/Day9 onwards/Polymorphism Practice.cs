//using Oops;
//using System;
//using System.IO;
//using System.IO.Enumeration;

//namespace Day9_onwards
//{
//    //Public Keyword being used below is an access modifier, which defines a members of class Accessibility
//    //class childClass : ParentClass  // Inheraitance
//    public class Car
//    {




//        ////This Class is having 3 Members, 2 Fieldss 1 Method(Funtion)
//        //public string color = "red";
//        //public int speed = 200;
//        ////Method
//        //public void fullSpeed()
//        //{
//        //    Console.WriteLine("The car is going as fast as it can!");
//        //}

//        ////Constructor Code
//        //public string model;
//        //public string color;
//        //public int year;

//        //private string testString = "This is a test String";
//        //public void showString()
//        //{
//        //    Console.WriteLine(testString);
//        //}

//        //// Create a class constructor with multiple parameters
//        //public Car(string modelName, string modelColor, int modelYear)
//        //{
//        //    model = modelName;
//        //    color = modelColor;
//        //    year = modelYear;
//        //}

//        ////Property And Encapsualtion(Hiding Sensitive data)
//        //private string name; // field
//        //public string Name   // property
//        //{
//        //    get { return name; }
//        //    set { name = value; }
//        //}

//        ////Another Approach
//        //public string Name  // property
//        //{ get; set; }

//        //PolyMorphism (OverRiding Methods)



//    }

//    ////the base class method overrides the derived class method
//    //public class Animal  // Base class (parent) 
//    //{
//    //    public virtual void animalSound()
//    //    {
//    //        Console.WriteLine("The animal makes a sound");
//    //    }
//    //}

//    //class Pig : Animal  // Derived class (child) 
//    //{
//    //    public override void animalSound()
//    //    {
//    //        Console.WriteLine("The pig says: wee wee");
//    //    }
//    //}

//    // class Dog : Animal  // Derived class (child) 
//    //{
//    //    public override void animalSound()
//    //    {
//    //        Console.WriteLine("The dog says: bow wow");
//    //    }
//    //}

//    ////Practice of Interfaces
//    //interface IFirstInterface
//    //{
//    //    void myMethod(); // interface method
//    //}

//    //interface ISecondInterface
//    //{
//    //    void myOtherMethod(); // interface method
//    //}

//    //// Implement multiple interfaces
//    //class DemoClass : IFirstInterface, ISecondInterface
//    //{
//    //    public void myMethod()
//    //    {
//    //        Console.WriteLine("Some text..");
//    //    }
//    //    public void myOtherMethod()
//    //    {
//    //        Console.WriteLine("Some other text...");
//    //    }
//    //}


//    public static class Polymorphism_Practice
//    {
//        enum Level
//        {
//            Low,
//            Medium,
//            High
//        }




//        static void Main(string[] args)
//        {
//            //Car myObject = new Car();
//            //Car myObjectChanged = new Car();
//            //myObjectChanged.color= "Orange";
//            //Console.WriteLine("Color Of Car: {0}", myObject.color);
//            //Console.WriteLine("Color Of Car: {0}", myObjectChanged.color);

//            //Car Ford = new Car("Mustang", "Red", 1969);
//            //Console.WriteLine(Ford.color + " " + Ford.year + " " + Ford.model);


//            ////Console.WriteLine(Ford.testString);
//            //Ford.showString();

//            //Car myObj = new Car();
//            //myObj.Name = "Honda";
//            //Console.WriteLine(myObj.Name);

//            ////PolyMorphism Code Sample
//            //Animal myAnimal = new Animal();  // Create a Animal object
//            //Animal myPig = new Pig();  // Create a Pig object
//            //Animal myDog = new Dog();  // Create a Dog object

//            //myAnimal.animalSound();
//            //myPig.animalSound();
//            //myDog.animalSound();

//            ////Interface Code
//            //DemoClass myObj = new DemoClass();
//            //myObj.myMethod();
//            //myObj.myOtherMethod();

//            //Level myEnumVariable = Level.Medium;
//            //Console.WriteLine(myEnumVariable);

//            //string writeText = "Hello World!"; 
//            //File.WriteAllText("filename.txt", writeText);  

//            //string readText = File.ReadAllText("filename.txt"); 
//            //Console.WriteLine(readText);  

//            //bool fileExist =  File.Exists("filename.txt");

//            //Console.WriteLine("File Exists?  " f+ fileExist);

//            MyTestOops testOops = new MyTestOops();
//            testOops.Id = 90;
//            Console.WriteLine(testOops.Id);
//            //Console.WriteLine(testOops.name);
//            //Console.WriteLine(testOops.internalValue); 


//            MyPrivateOop myPrivateOop = new MyPrivateOop();
//            // myPrivateOop.RollNumber
//            myPrivateOop.PRivateShow();

//            MyProtectedOop MyProtectedOop = new MyProtectedOop();
//            MyProtectedOop.Id = 15;
//            MyProtectedOop.showPOrotectedSample();

//            NotMyProtectedOop notMyProtectedOop = new NotMyProtectedOop();
//            notMyProtectedOop.showPOrotectedSample2();












//        }


//    }
//}