using System;

namespace calculator
{
class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Enter an expression (ex. 2 + 3): ");
		string input = Console.ReadLine();

		try
		{
			Parser parser = new Parser();
			(double num1, string op, double num2) = parser.Parse(input);

			Calculator calculator = new Calculator();
			double result = calculator.Calculate(num1, op, num2);

			Console.WriteLine($"Result: {result}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}
	}
}

// Parser class to parse the input
public class Parser
{
	public (double, string, double) Parse(string input)
	{
		string[] parts = input.Split(' ');

		if (parts.Length != 3)
		{
			throw new FormatException("Input must be in the format: number operator number");
		}

		double num1 = Convert.ToDouble(parts[0]);
		string op = parts[1];
		double num2 = Convert.ToDouble(parts[2]);

		return (num1, op, num2);
	}
}

// Calculator class to perform operations
public class Calculator
{
	public double Calculate(double num1, string op, double num2)
	{
		if(op == "+")
		{
			return num1 + num2;
		}
		if(op == "-")
		{
			return num1 - num2;
		}
		if(op == "*")
		{
			return num1 * num2;
		}
		if(op == "/")
		{
			if(num2 == 0) {
				throw new DivideByZeroException("Division by zero is not allowed");
				return 0;
			}
			return num1 / num2;
		}
		if(op == "**")
		{
			double res = 1;
			if(num2 < 0) {
				for(int i = 1; i <= -num2; i++)
					res /= num1;
			}
			else {
				for(int i = 1; i <= num2; i++)
					res *= num1;
			}
			return res;
		}
		if(op == "L")
		{
			double originNum1;
			double originNum2;
			originNum1 = num1;
			originNum2 = num2;
			double r;
			if(num1 <= num2)
			{
				r = num1;
				num1 = num2;
				num2 = r;
			}
			while(num2 != 0)
			{
				r = num1 % num2;
				num1 = num2;
				num2 = r;
			}
			return originNum1 * originNum2 / num1;
		}
		if(op == "G")
		{
			double r;
			if(num1 <= num2)
			{
				r = num1;
				num1 = num2;
				num2 = r;
			}
			while(num2 != 0)
			{
				r = num1 % num2;
				num1 = num2;
				num2 = r;
			}
			return num1;
		}
		if(op == "%")
		{
			return num1 % num2;
		}
		throw new InvalidOperationException("Invalid operator");
		return 0;
	}
}
}

/* example output

Enter an expression (ex. 2 + 3):
>> 4 * 3
Result: 12

*/


/* example output (CHALLANGE)

Enter an expression (ex. 2 + 3):
>> 4 ** 3
Result: 64

Enter an expression (ex. 2 + 3):
>> 5 ** -2
Result: 0.04

Enter an expression (ex. 2 + 3):
>> 12 G 15
Result: 3

Enter an expression (ex. 2 + 3):
>> 12 L 15
Result: 60

Enter an expression (ex. 2 + 3):
>> 12 % 5
Result: 2

*/
