using Chapter06;
using static System.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        string authorName = null;

        // следующий код генерирует исключение NullReferenceException
        //int x = authorName.Length;

        // вместо того чтобы генерировать исключение, y присваивается null
        int? y = authorName?.Length;

        Person harry = new();

        // назначение метода полю делегата
        harry.Shout += Harry_Shout;
        harry.Shout += omled_test;

        // Джинерик
        Dictionary<int, string> lookupIntString = new();
        lookupIntString.Add(key: 1, value: "omled");
        lookupIntString.Add(key: 2, value: "omled1");
        lookupIntString.Add(key: 3, value: "omled2");

        int key = 3;
        WriteLine(format: "Key {0} has value: {1}",
         arg0: key,
         arg1: lookupIntString[key]);

        Person jill = new();

        //вызов статическеого метода класса
        Person baby = Person.Procreate(harry, jill);

        // вызов операции
        Person baby3 = harry * jill;


        harry.Poke();
        harry.Poke();
        harry.Poke();

        SortNames();

        DisplacementVector dv1 = new(3, 5);
        DisplacementVector dv2 = new(-2, 7);
        DisplacementVector dv3 = dv1 + dv2;


        Employee alice = new() { Name = "Alice", EmployeeCode = "a13fd" };
        Person person = alice;
        alice.WriteToConsole();
        person.WriteToConsole();

        WriteLine(alice.ToString());
        WriteLine(person.ToString());

        // шаблон объявления (нет необходимости явно приводить тип)
        if(person is Employee employee)
        {
            WriteLine($"{nameof(employee)} IS an Employee");
            // безопасно выполняем что-либо с explicitAlice
        }

        Employee? employee1 = person as Employee;
        if(employee1 != null)
        {
            WriteLine($"{employee1} is Employee");
        }
        if(employee1 is not Employee)
        {
            WriteLine($"{employee1} is not Employee");
        }


        Person john = new();
        john.DateOfBirth = new(1951, 12, 25); 

        // Реализация выброса исключения
        try
        {
            john.TimeTravel(when: new(1999, 12, 31));
            john.TimeTravel(when: new(1950, 12, 25));
        }
        catch(PersonException ex)
        {
            WriteLine(ex.Message);
        }
        
        string email1 = "pamela@test.com";
        string email2 = "ian&test.com";

        WriteLine("{0} is a valid e-mail address: {1}",
         arg0: email1,
         arg1: StringExtensions.IsValidEmail(email1));

        WriteLine("{0} is a valid e-mail address: {1}",
         arg0: email2,
         arg1: email2.IsValidEmail());

        String d = "fdf";
        WriteLine(d);

    }  

    int Add(int a, int b) => a + b; 
    int Multiple(int c, int d) => c * d;

    delegate int Operation(int x, int y);

    static void Harry_Shout(object? sender, EventArgs e)
    {
        if (sender is null) return;
        Person p = (Person)sender;
        WriteLine($"{p.Name} is this angry: {p.AngerLevel}.");
    }
    static void omled_test(object? sender, EventArgs e)
    {
        WriteLine("Test");
    }

    // наследование к классе Person интерфейска IComparable для сопоставления объектов
    public static void SortNames()
    {
        Person[] people =
        {
            new() { Name = "Simon" },
            new() { Name = "Jenny" },
            new() { Name = "Adam" },
            new() { Name = "Richard" }
        };

        WriteLine("Initial list of people:");
        foreach (Person p in people)
        {
            WriteLine($" {p.Name}");
        }

        WriteLine("Use Person's IComparable implementation to sort:");
        Array.Sort(people);
        foreach (Person p in people)
        {
            WriteLine($" {p.Name}");
        }


        // сортировка 

        //WriteLine("Use PersonComparer's IComparer implementation to sort:");
        //Array.Sort(people, new PersonComparer());
        //foreach (Person p in people)
        //{
        //    WriteLine($" {p.Name}");
        //}

    }


    // метод с локальной функцией
    public static int Factorial(int number)
    {
        if (number < 0)
        {
            throw new ArgumentException(
            $"{nameof(number)} cannot be less than zero.");
        }
        return localFactorial(number);

        int localFactorial(int localNumber) // локальная функция
        {
            if (localNumber < 1) return 1;
            return localNumber * localFactorial(localNumber - 1);
        }
    }
}