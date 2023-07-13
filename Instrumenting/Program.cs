using System.Diagnostics;
using static System.Console;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        Withdraw("fdf", -1);

        // запись в текстовый файл, расположенный в папке проекта
        Trace.Listeners.Add(new TextWriterTraceListener(
            File.CreateText(Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.DesktopDirectory), "log.txt"))));

        // модуль записи текста буферизируется, поэтому данная опция
        // вызывает функцию Flush() для всех прослушивателей после записи
        Trace.AutoFlush = true;

        Debug.WriteLine("debug");
        Trace.WriteLine("trace");
    }

    // намеренный вызов исключений
    static void Withdraw(string accountName, decimal amount)
    {

        if (accountName is null)
        {
            throw new ArgumentNullException(paramName: nameof(accountName)); // выброс исключения по null
        }
        if (amount < 0)
        {
            throw new ArgumentException(
            message: $"{nameof(amount)} cannot be less than zero.");
        }

        // сгенерировать новое исключение с вложенным в него перехваченным исключением

        //throw new InvalidOperationException(
        //message: "Calculation had invalid values. See inner exception for why.",
        //innerException: ex);
    }

    static void FindFactors(int number)
    {
        if(number > 1000)
        {
            WriteLine("Введено число больше 1000!");
            return;
        }

        for (int i = 2; i <= number ; i++)
        {
            if(number % i == 0)
            {
                bool isPrime = true;

                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    WriteLine(i);
                    number /= i;
                    i--;
                }
            }
        }
    }
}