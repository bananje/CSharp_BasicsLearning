// See https://aka.ms/new-console-template for more information
using Exercise03;
using static System.Console; // статический импорт

internal class Program
{
    private static void Main(string[] args)
    {
        string[] header = new[] { "Type \t Byte(s) of memory", "Min", "Max" };

        string[] types = new[] { "sbyte", "byte", "short", "ushort", "int", "uint", "long", "ulong", "float", "double", "decimal" };
        WriteLine(format: "{0, -3} {1, 22} {2, 22}", arg0: header[0], arg1: header[1], arg2: header[2]);
        WriteLine();

        foreach (var item in types)
        {
            WriteLine(item + item as Type);
        }
    }
}