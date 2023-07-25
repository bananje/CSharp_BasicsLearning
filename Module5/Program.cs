using ClassLibrary;
using PacktLibraryModern;
using static System.Console;

internal class Program
{

//    Свойства, доступные только для инициализации, обеспечивают некоторую неизменность C#. Вы можете развить эту концепцию с помощью записей. Они определяются
//с помощью ключевого слова record вместо ключевого слова class. Это делает весь
//объект неизменным, поэтому он работает как значение при сравнении
    public  record ImmutableVehicle
    {
        public int Wheels { get; init; }
        public string? Color { get; init; }
        public string? Brand { get; init; }
    }

    private static void Main(string[] args)
    {
        Person person = new()
        {
            Name = "Test",
            DateOfBirth = new(1965,12,22) // ввод даты 
        };

        person.World = WondersOfTheAncientWorld.ColossusOfRhodes;

        person.BucketList = WondersOfTheAncientWorld.MausoleumAtHalicarnassus | WondersOfTheAncientWorld.ColossusOfRhodes;

        WriteLine(person.BucketList);

        person.Children.Add(new() { Name="Test" });
        person.Children.Add(new() { Name = "Test1" });

        
        foreach (var children in person.Children)
        {
            WriteLine(children.Name);
        }

        BankAccount bankAccount = new();

        bankAccount.AccountName = "Test";
        bankAccount.Balance = 100;

        (string, int) fruit = bankAccount.GetFruit();
        WriteLine($"{fruit.Item1}, {fruit.Item2}");

        var cortage = bankAccount.GetNamed();
        WriteLine($"{cortage.Name} {cortage.Number}");

        // сохранение возвращаемого значения в переменной кортежа с двумя полями 
        (string name, int num) bank = bankAccount.GetNamed();
        WriteLine($"Deconstructed: {bank.name} {bank.num}");

        // деконструкция возвращаемого значения на две отдельные переменные 
        (string name, int num)  = bankAccount.GetNamed();
        WriteLine($"Deconstructed: {name} {num}");

        // деконструкция объекта
        Person man = new();

        man.Name = "Test";
        man.DateOfBirth= DateTime.Now;

        var (name1, date) = man;
        WriteLine($"{name1} {date}");



        // cопоставление с образцом с помощью объектов
        object[] passengers = {
             new FirstClassPassenger { AirMiles = 1_419 },
             new FirstClassPassenger { AirMiles = 16_562 },
             new BusinessClassPassenger(),
             new CoachClassPassenger { CarryOnKG = 25.7 },
             new CoachClassPassenger { CarryOnKG = 0 },
        };

        foreach (object passenger in passengers)
        {
            // старый синтаксис

            //decimal flightCost = passenger switch
            //{
            //    FirstClassPassenger p when p.AirMiles > 35000 => 1500M,
            //    FirstClassPassenger p when p.AirMiles > 15000 => 1750M,
            //    FirstClassPassenger _ => 2000M,
            //    BusinessClassPassenger => 1000M,
            //    CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
            //    CoachClassPassenger _ => 650M,
            //    _ => 800M
            //};

            // синтаксис сопоставления в новой версии
            decimal flightCost = passenger switch
            {                
                FirstClassPassenger p => p.AirMiles switch
                {
                    > 35000 => 1500M,
                    > 15000 => 1750M,
                    _ => 2000M
                },
                BusinessClassPassenger => 1000M,
                CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
                CoachClassPassenger => 650M,
                _ => 800M
            };
            WriteLine($"Flight costs {flightCost:C} for {passenger}");
        }

        // инициализация записи
        ImmutableVehicle car = new()
        {
            Brand = "Mazda MX-5 RF",
            Color = "Soul Red Crystal Metallic",
            Wheels = 4
        };

       
        ImmutableVehicle repaintedCar = car
                with
        { Color = "Polymetal Grey Metallic" };
    }
}