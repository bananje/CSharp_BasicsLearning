using static System.Console;
internal class Program
{
    private static void Main(string[] args)
    {
        ValidationException();
    }
    public static void ExerciseFizzBuzz()
    {
        for (int i = 1; i < 100; i++)
        {
            string symbol = i.ToString();

            if (i % 3 == 0)
            {
                symbol = "Fizz";
            }
            if (i % 5 == 0)
            {
                symbol = "Buzz";
            }
            if (i % 3 == 0 && i % 5 == 0)
            {
                symbol = "FizzBuzz";
            }

            if (i % 10 == 0)
            {
                WriteLine();
            }
            Write("\t" + symbol + ",");
        }
    }

    public static void ValidationException()
    {
        WriteLine("Введите число от 0 до 255: ");
        string num1 = ReadLine();
        WriteLine("Введите число от 0 до 255: ");
        string num2 = ReadLine();

        if((int.TryParse(num1, out int num3) && int.TryParse(num2, out int num4)) == false)
        {
            Write("Введены не числа");
            return;
        }

        try
        {
            WriteLine($"Делим число {num1} на число {num2} и получаем {num3/num4}");
        }
        catch(Exception ex)
        {
            WriteLine(ex.ToString()); 
            return;
        }
    }
}