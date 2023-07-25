// See https://aka.ms/new-console-template for more information
using System.Xml.Serialization;
using WorkingWithSerialization;
using static System.Console;
using static System.Environment;
using static System.IO.Path;


internal class Program
{
    static List<Person> people = new()
        {
            new(30000M) // передача значения в конструктор
            {
                FirstName = "Alice",
                LastName = "Smith",
                DateOfBirth = new(1974,3,14)
            },
            new(40000M)
            {
                FirstName = "Bob",
                LastName = "Jones",
                DateOfBirth = new(1969, 11, 23)
            },
            new(20000M)
            {
                FirstName = "Charlie",
                LastName = "Cox",
                DateOfBirth = new(1984, 5, 4),
                Children = new()
                {
                    new(0M)
                    {
                        FirstName = "Sally",
                        LastName = "Cox",
                        DateOfBirth = new(2000, 7, 12)
                    }
                }
            }
    };
    static string dir = Combine(
       GetFolderPath(SpecialFolder.Personal),
       "Code", "Chapter09", "OutputFiles");
    private static void Main(string[] args)
    {
        JSONSerialization();
    }
    static void JSONSerialization()
    {
        string jsonPath = Combine(dir, "people.json");

        using (StreamWriter jsonStream = File.CreateText(jsonPath))
        {
            // создаем объект, который будет форматироваться как JSON
            Newtonsoft.Json.JsonSerializer jss = new();
            // сериализуем объектный граф в строку
            jss.Serialize(jsonStream, people);

            WriteLine(File.ReadAllText(jsonPath));
        }
    }
}