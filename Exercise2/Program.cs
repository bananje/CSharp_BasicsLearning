// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Text.RegularExpressions;
using static System.Console;
using Humanizer;

internal class Program
{

    private static void ToWords(int number)
    {
        WriteLine(number.ToWords());
    }

    private static void Main(string[] args)
    {
        //WriteLine("The default regular expression checks for at least one digit.");
        //WriteLine("Enter a regular expression (or press ENTER to use the default):");
        //string? regex = ReadLine();
        //Regex r = new("");

        //if (string.IsNullOrEmpty(regex))
        //{
        //    r = new(@"\S");
        //}         
        //r = new Regex(regex);

        //while (true)
        //{
        //    int i = 16_000_000;

        //    WriteLine("Input word:");
        //    string word = ReadLine();

        //    if (r.IsMatch(word))
        //    {
        //        WriteLine("True");
        //    }
        //    else
        //    {
        //        WriteLine("False");
        //    }
        //}        
        ToWords(345345);
    }
}