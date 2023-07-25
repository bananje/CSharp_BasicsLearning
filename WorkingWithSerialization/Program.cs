using System.Xml.Serialization;
using WorkingWithSerialization;
using static System.Console;
using static System.Environment;
using static System.IO.Path;
using NewJson = System.Text.Json.JsonSerializer;

internal class Program
{
    private static void Main(string[] args)
    {
        JSONSerialization();
    }

    static string path = Combine(CurrentDirectory, "people.xml");
    static string dir = Combine(
        GetFolderPath(SpecialFolder.Personal),
        "Code", "Chapter09", "OutputFiles");
    // объектный граф
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

    ////При использовании класса XmlSerializer помните, что учитываются только
    ////открытые(public) поля и свойства, а тип должен содержать конструктор
    ////без параметров.Вывод можно настроить с помощью атрибутов.
    static void XMLSerialization()
    {
        // создаем объект, который будет форматировать список лиц как XML
        XmlSerializer xs = new(people.GetType());
        // создаем файл для записи
        path = Combine(CurrentDirectory, "people.xml");

        using (FileStream stream = File.Create(path))
        {
            // сериализуем объектный граф в поток
            xs.Serialize(stream, people);
        }

        WriteLine("Written {0:N0} bytes of XML to {1}", arg0: new FileInfo(path).Length, arg1: path); 
        WriteLine();

        // отображаем сериализованный граф объектов
        WriteLine(File.ReadAllText(path));

        using (FileStream xmlLoad = File.Open(path, FileMode.Open))
        {
            // десериализуем и приводим объектный граф в список лиц
            List<Person>? loadedPeople =
            xs.Deserialize(xmlLoad) as List<Person>;

            if (loadedPeople is not null)
            {
                foreach (Person p in loadedPeople)
                {
                    WriteLine("{0} has {1} children.",
                    p.LastName, p.Children?.Count ?? 0);
                }
            }
        }
    }
    static void JSONSerialization()
    {
        string jsonPath = Combine(dir,"people.json");

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