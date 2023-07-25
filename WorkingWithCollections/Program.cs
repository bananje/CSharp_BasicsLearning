using System.Collections.Immutable;
using static System.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        WorkingWithLists();
    }
    static void Output(string title, IEnumerable<string> collection)
    {
        WriteLine(title);
        foreach (string item in collection)
        {
            WriteLine($" {item}");
        }
    }

    // работа с листами
    static void WorkingWithLists()
    {
        // Представляет собой коллекцию пар «ключ —
        // значение», которые сортируются по ключу

        // SortedList<TKey, TValue>

        // SortedSet<T> -- Представляет собой коллекцию уникальных объектов, которые сопровождаются в отсортированном порядке

        // простой синтаксис для создания списка и добавления трех элементов
        List<string> cities = new();
        cities.Add("London");
        cities.Add("Paris");
        cities.Add("Milan");

        /* альтернативный синтаксис, который преобразуется компилятором
        в три вышеприведенных вызова метода Add
        List<string> cities = new()
        { "London", "Paris", "Milan" };
        */

        /* альтернативный синтаксис, передающий массив строковых значений
        методу AddRange
        List<string> cities = new();
        cities.AddRange(new[] { "London", "Paris", "Milan" });
        */

        Output("Initial list", cities);

        WriteLine($"The first city is {cities[0]}.");
        WriteLine($"The last city is {cities.Last()}.");

        cities.Insert(0, "Sydney");
        Output("After inserting Sydney at index 0", cities);

        cities.RemoveAt(1);
        cities.Remove("Milan");
        Output("After removing two cities", cities);

        // неизменяемая коллекция
        ImmutableList<string> immutableCities = cities.ToImmutableList();
        ImmutableList<string> newList = immutableCities.Add("Rio");
        Output("Immutable list of cities:", immutableCities);
        Output("New list of cities:", newList);
    }

    // работа со словарями
    static void WorkingWithDictionaries()
    {
        // Представляет собой коллекцию пар «ключ —
        //значение», которые сортируются по ключу

        // SortedDictionary<TKey, TValue>

        Dictionary<string, string> keywords = new();
        // добавление с использованием именованных параметров
        keywords.Add(key: "int", value: "32-bit integer data type");
        // добавление с использованием позиционных параметров
        keywords.Add("long", "64-bit integer data type");
        keywords.Add("float", "Single precision floating point number");

        /* альтернативный синтаксис; компилятор преобразует в вызовы метода Add
        Dictionary<string, string> keywords = new()
        {
            { "int", "32-bit integer data type"
            },
            { "long", "64-bit integer data type"
            },
            { "float", "Single precision floating point number"
            },
        };
        */

        Output("Dictionary keys:", keywords.Keys);
        Output("Dictionary values:", keywords.Values);

        WriteLine("Keywords and their definitions");
        foreach (KeyValuePair<string, string> item in keywords)
        {
            WriteLine($" {item.Key}: {item.Value}");
        }

        // ищем значение по ключу
        string key = "long";
        WriteLine($"The definition of {key} is {keywords[key]}");
    }

    // работа с очередями
    static void WorkingWithQueues()
    {      
        Queue<string> coffee = new();
        coffee.Enqueue("Damir"); // начало очереди
        coffee.Enqueue("Andrea");
        coffee.Enqueue("Ronald");
        coffee.Enqueue("Amin");
        coffee.Enqueue("Irina"); // конец очереди
        Output("Initial queue from front to back", coffee);

        // сервер обрабатывает следующего человека в очереди
        string served = coffee.Dequeue();
        WriteLine($"Served: {served}.");

        // сервер обрабатывает следующего человека в очереди
        served = coffee.Dequeue();
        WriteLine($"Served: {served}.");
        Output("Current queue from front to back", coffee);
        WriteLine($"{coffee.Peek()} is next in line.");
        Output("Current queue from front to back", coffee);
    }

    static void OutputPQ<TElement, TPriority>(string title,
                IEnumerable<(TElement Element, TPriority Priority)> collection)
    {
        WriteLine(title);
        foreach ((TElement, TPriority) item in collection)
        {
            WriteLine($" {item.Item1}: {item.Item2}");
        }
    }

    // работа с приоритетной очередью
    static void WorkingWithPriorityQueues()
    {
        PriorityQueue<string, string> vaccine = new();
        // добавляем несколько человек
        // 1 = высокоприоритетные люди в возрасте 70 лет или со слабым здоровьем
        // 2 = средний приоритет, например люди среднего возраста
        // 3 = низкий приоритет, например подростки и молодые люди
        vaccine.Enqueue("Pamela", "2"); // моя мама (70 лет)
        vaccine.Enqueue("Rebecca", "1"); // моя племянница (подросток)
        vaccine.Enqueue("Juliet", "2"); // моя сестра (40 лет)
        vaccine.Enqueue("Ian", "1"); // мой папа (70 лет)

        OutputPQ("Current queue for vaccination:", vaccine.UnorderedItems);

        WriteLine($"{vaccine.Dequeue()} has been vaccinated.");
        WriteLine($"{vaccine.Dequeue()} has been vaccinated.");

        OutputPQ("Current queue for vaccination:", vaccine.UnorderedItems);

        WriteLine($"{vaccine.Dequeue()} has been vaccinated.");

        vaccine.Enqueue("Mark", "3"); // я (40 лет)
        WriteLine($"{vaccine.Peek()} will be next to be vaccinated.");

        OutputPQ("Current queue for vaccination:", vaccine.UnorderedItems);
    }

}