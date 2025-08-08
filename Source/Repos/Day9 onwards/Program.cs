

//  start your code here

static double addNumber(double num1, double num2)
{
    return num1 + num2;
}

static double subNumber(double num1, double num2)
{
    return num1 - num2;
}

static double mulNumber(double num1, double num2)
{
    return num1 * num2;
}

static double divNumber(double num1, double num2)
{
    return num1 / num2;
}

//Main Code Starts Form Here
Console.WriteLine("Hello, Welcome to niky's Program \nChoose Your Options from 1...4:\n1: Addition\n2: Subtraction\n3: Multiplication\n4: Division");
int option = int.Parse(Console.ReadLine());
double inputNumber1, inputNumber2;
Console.WriteLine("Your Option chosen: " + option);

switch (option)
{
    case 1:
        Console.WriteLine("Enter the two Numbers you want to perform the Addition operation on:");
        inputNumber1 = double.Parse(Console.ReadLine());
        inputNumber2 = double.Parse(Console.ReadLine());
        Console.WriteLine("The Result After selected operations Is:  " + addNumber(inputNumber1, inputNumber2));
        break;
    case 2:
        Console.WriteLine("Enter the two Numbers you want to perform the Subtraction operation on:");
        inputNumber1 = double.Parse(Console.ReadLine());
        inputNumber2 = double.Parse(Console.ReadLine());
        Console.WriteLine("The Result After selected operations Is:  " + subNumber(inputNumber1, inputNumber2));
        break;
    case 3:
        Console.WriteLine("Enter the two Numbers you want to perform the Multiplication operation on:");
        inputNumber1 = double.Parse(Console.ReadLine());
        inputNumber2 = double.Parse(Console.ReadLine());
        Console.WriteLine("The Result After selected operations Is:  " + mulNumber(inputNumber1, inputNumber2));
        break;
    case 4:
        Console.WriteLine("Enter the two Numbers you want to perform the Division operation on:");
        inputNumber1 = double.Parse(Console.ReadLine());
        inputNumber2 = double.Parse(Console.ReadLine());
        Console.WriteLine("The Result After selected operations Is:  " + divNumber(inputNumber1, inputNumber2));
        break;
    default:
        Console.WriteLine("Some Error Occured");
        break;
}


