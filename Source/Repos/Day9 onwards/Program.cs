

//  start your code here

//static double addNumber(double num1, double num2)
//{
//    return num1 + num2;
//}

//static double subNumber(double num1, double num2)
//{
//    return num1 - num2;
//}

//static double mulNumber(double num1, double num2)
//{
//    return num1 * num2;
//}

//static double divNumber(double num1, double num2)
//{
//    return num1 / num2;
//}

////Main Code Starts Form Here
//Console.WriteLine("Hello, Welcome to niky's Program \nChoose Your Options from 1...4:\n1: Addition\n2: Subtraction\n3: Multiplication\n4: Division");
//int option = int.Parse(Console.ReadLine());
//double inputNumber1, inputNumber2;
//Console.WriteLine("Your Option chosen: " + option);
//Console.WriteLine("Enter the two Numbers you want to perform the Chosen Arithmetic operation on:");
//inputNumber1 = double.Parse(Console.ReadLine());
//inputNumber2 = double.Parse(Console.ReadLine());

//switch (option)
//{
//    case 1:

//        Console.WriteLine("The Result After selected operations Is:  " + addNumber(inputNumber1, inputNumber2));
//        break;
//    case 2:

//        Console.WriteLine("The Result After selected operations Is:  " + subNumber(inputNumber1, inputNumber2));
//        break;
//    case 3:

//        Console.WriteLine("The Result After selected operations Is:  " + mulNumber(inputNumber1, inputNumber2));
//        break;
//    case 4:
//        Console.WriteLine("The Result After selected operations Is:  " + divNumber(inputNumber1, inputNumber2));
//        break;
//    default:
//        Console.WriteLine("Incorrect option Chosen");
//        break;
//}



//Doing Methods Practice from here:

//string numString = "1234";

////int numInt = int.Parse(numString);
////it throw an error exception when tried to do incrorrect 
//int result;
//int.TryParse(numString, out  result);
////if tryparse fails, instead of throwing an exception it display false or 0 
////out Allows us to not explicitly initializing the variable, used in Try methods

//Console.Write(result);

//Doing Ref practice
//void ModifyValue(ref int x)
//{
//    x = x * 3; // Reads and writes
//}

//int myValue = 10;
//Console.WriteLine(myValue);
//ModifyValue(ref myValue); // myValue is now 20
//Console.WriteLine(myValue);


////String Methods
//using System.Linq;

//string myName = "Mohsin Raza";
//string mySentence = "My Name is ";
//string myNameSentence = string.Concat(mySentence, myName);
////Format
//string myNameSentence2 = string.Format("Hello {0}", myName);
////Compare
//int resultCompare = string.Compare(mySentence, myName);
////Join
//string[] text = { "C#", "Java", "C++" };
//string stringJoin = string.Join(" ", text);
////Equals
//bool stringEquals = string.Equals(text, myName);



////Split
//string[] stringsArray = mySentence.Split(" ");
////Substring
//string stringSub1 = mySentence.Substring(5,4);
////Replace
//string stringReplaced = mySentence.Replace("My", "Your");
////Contains
//bool stringContains = mySentence.Contains("Your");
////Trim
//string stringTrim = "     text       ";
//stringTrim = stringTrim.Trim();
////stringTrim = stringTrim.TrimEnd();
////stringTrim = stringTrim.TrimStart();

////IndexOf
//int stringIndex = myName.IndexOf("Ra");
////LastIndexOf
//int stringLastIndex = myNameSentence2.LastIndexOf("a");
////Remove
//string stringRemove = myNameSentence2.Remove(5);
////string stringRemove = myNameSentence2.Remove(5,3); here 3 is the number of character to be removed starting from index 5
////ToUpper, ToLower
//string stringToUpper = myName.ToUpper();
//string stringToLower = myName.ToLower();

////EndsWith
//bool stringEndsWith = myName.EndsWith("Raza");
////StartsWith
//bool stringStartsWith = myName.StartsWith("Raza");
//// CharArray
//char[] stringCharArray = mySentence.ToCharArray();
//// PadLeft, PadRight
//string stringPadLeft = myName.PadLeft(20);
//string stringPadRight = myName.PadRight(20);

//// All, not applicable here , specific case is needed

////Distinct
//var stringDistinct = myName.Distinct();
//// All()	Checks if all elements match a condition.
//// Any()	Checks if any element matches a condition (or if collection is non-empty).
//// Distinct()	Returns unique elements (removes duplicates).
//// DistinctBy()	Returns unique elements based on a condition.


//// is
//if (myName is string)
//{
//    Console.WriteLine("myName is String");
//}













////String Methods Being Displayed
//Console.WriteLine(myNameSentence);
//Console.WriteLine(myNameSentence2);
//Console.WriteLine(resultCompare);
//Console.WriteLine(stringJoin);
//Console.WriteLine(stringEquals);

//Console.WriteLine("\n\n" + stringsArray[0]);
//Console.WriteLine(stringSub1);
//Console.WriteLine(stringReplaced);
//Console.WriteLine(stringContains);
//Console.WriteLine(stringTrim);
//Console.WriteLine(stringIndex);
//Console.WriteLine(stringLastIndex);
//Console.WriteLine(stringRemove);
//Console.WriteLine(stringToUpper);
//Console.WriteLine(stringToLower);
//Console.WriteLine(stringEndsWith);
//Console.WriteLine(stringStartsWith);
//Console.Write(stringCharArray[0]);
//Console.Write(stringCharArray[1]);
//Console.WriteLine(stringCharArray[2]);
//Console.WriteLine(stringPadLeft);
//Console.WriteLine(stringPadRight);
//Console.WriteLine(stringDistinct);


//Array initialization , 1-D, 2-D, and 3-D
int[] oneDArray = { 1, 2, 3, 4, 5, 6 };
int[,] twoDArray = {{ 10, 20, 30, 40, 50, 60 },{ 1, 2, 3, 4, 5, 6 }};
int[,,] threeDArray = { { { 10, 20 }, { 30, 40 } },
                        { { 10, 20 }, { 30, 40 } } };

//Console.WriteLine(threeDArray[0,1,1]);

int[] testArray = { 1, 2, 3 };

Console.WriteLine("Array Is Fixed Size:" + testArray.IsFixedSize);

Console.WriteLine("Array Is Read Only:" + testArray.IsReadOnly);

Console.WriteLine("Array Is Synchronized:" + testArray.IsSynchronized);

Console.WriteLine("Array Length:" + testArray.Length);
Console.WriteLine("Array Long Length:" + threeDArray.LongLength);
Console.WriteLine("Array Rank:" + testArray.Rank);
Console.WriteLine("Array Equals:" + testArray.Equals(testArray));


//AsReadOnly Method add a wrapper, need to look further in details
Console.WriteLine("Array Binary Search:" + Array.BinarySearch(testArray,5));
Console.WriteLine("Array IndexOf:" + Array.IndexOf(testArray,1));
//Array Clear
Array.Clear(oneDArray, 1, 1);
Array.Reverse(oneDArray);
Console.WriteLine(oneDArray[0]);
//int[] copiedArray = Array.Copy(oneDArray);
//Exists
string[] language = {"Ruby", "C", "C++", "Java", "Perl", "C#", "Python", "Ruby"};
Console.WriteLine("Is Ruby part of language: {0}", Array.Exists(language, element => element == "Ruby"));
Console.WriteLine("Is Ruby4 part of language: {0}", Array.Exists(language, element => element == "Ruby4"));
//Find  Element in Array                                                                      
Console.WriteLine("Finding Element: {0}", Array.Find(language, element => element == "Ruby")); 
//Find Last Element in Array                                                                      
Console.WriteLine("Finding Last Element: {0}", Array.FindLast(language, element => element == "Ruby")); 
//FindIndex
Console.WriteLine("Find Index: {0}", Array.FindIndex(language, element => element == "C"));
//FindIndexLast
Console.WriteLine("Find Last Index: {0}", Array.FindLastIndex(language, element => element.StartsWith("C")));


Console.WriteLine("Get Length: {0}", language.GetLength(0) );
Console.WriteLine("Get Long Length: {0}", twoDArray.GetLongLength(1));
Console.WriteLine("Get Lower Bound: {0}", twoDArray.GetLowerBound(0));
Console.WriteLine("Get Upper Bound: {0}", twoDArray.GetUpperBound(0));
Console.WriteLine("Get Upper Bound: {0}", twoDArray.GetUpperBound(1));











//format


