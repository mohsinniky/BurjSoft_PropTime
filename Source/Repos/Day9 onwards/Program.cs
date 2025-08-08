using System;
using System.Drawing;

Console.WriteLine("Hello, This Is Mohsin Writing His First Program!");
Console.Write("Using Write() to write this line lets see what happens! \n");                                                  
//Below is the code of using Read() and ReadLine()
Console.WriteLine("Enter your Age");
int age = Convert.ToInt32(Console.ReadLine());

if (age <= 0)
{
    Console.WriteLine("Invalid Age Entered");
}else if (age <= 20)
{
    Console.WriteLine("Too Young to See this Message");
}else
{
    Console.WriteLine("You can View The Message");
}




//int rdExample = Console.Read();
//Console.WriteLine(rdExample);

//Variable Initialization and Testing
int myNum = 5;
double myDoubleNum = 5.99D;
char myLetter = 'D';
bool myBool = true;
string myText = "Hello";

Console.WriteLine("Int: " + myNum + "\nDouble: " + myDoubleNum + "\nChar: " + myLetter + "\nBool: " + myBool + "\nString: " + myText);
//const Variable 
const int myConstant= 15;
//myConstant = 20; // error


//Variables Names Must be unique and they are also called as identifiers..


float f1 = 35e3F;
Console.WriteLine("Float: " + f1);

//Implicit Casting(automatically) -converting a smaller type to a larger type size
//char -> int -> long -> float -> double

//Explicit Casting (manually) - converting a larger type to a smaller size type
//double -> float -> long -> int -> char

double myDoubleValue = 10.90;
int myIntForTypeCasting = (int) myDoubleValue;    // Manual casting: double to int
string myStringTypeCasting = Convert.ToString(myDoubleValue);

Console.WriteLine($" Double: {myDoubleValue}, Int: {myIntForTypeCasting}, String: {myStringTypeCasting}");
Console.WriteLine(myDoubleValue + myStringTypeCasting);

//Math
Console.WriteLine(Math.Min(5, 10));
Console.WriteLine(Math.Sqrt(20));
Console.WriteLine(Math.Round(1212.211));



//Loops
int i = 0;
do
{
    Console.WriteLine(i);
    i++;
}
while (i < 5);


//ForEach 
string[] cars = { "Volvo", "BMW", "Ford", "Mazda" };
foreach (string item in cars)
{
    Console.WriteLine(item);
}

