using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Chapter06
{
    // наследование от интерфейса и реализация в классе
    public class Person : object, IComparable<Person>
    {
        public string? Name; // символ ? допускает нулевое значение
        public DateTime DateOfBirth;
        public List<Person> Children = new(); // C# 9 и более поздние версии
                                              // методы

        public override string ToString()
        {
            return $"{Name} is a {base.ToString()}";
        }

        public void WriteToConsole()
        {
            WriteLine($"{Name} was born on a {DateOfBirth:dddd}.");
        }

        // делегаты
        public delegate void EventHandler(object? sender, EventArgs e);
        public delegate void EventHandler<TEventArgs>(object? sender, TEventArgs e);
        
        // Методы экземпляра — действия,
        // которые объект выполняет к себе; статические методы — действия, которые выполняет тип.

        // статический метод для "размножения"
        public static Person Procreate(Person p1, Person p2)
        {
            Person baby = new()
            {
                Name = $"Baby of {p1.Name} and {p2.Name}"
            };
            p1.Children.Add(baby);
            p2.Children.Add(baby);
            return baby;
        }

        // метод экземпляра для "размножения"
        public Person ProcreateWith(Person partner)
        {
            return Procreate(this, partner);
        }

        // операция "размножения"
        public static Person operator *(Person p1, Person p2)
        {
            return Procreate(p1, p2);
        }

        // поле делегата
        public event EventHandler? Shout;
        // поле данных
        public int AngerLevel;
        // метод
        public void Poke()
        {
            AngerLevel++;
            if (AngerLevel >= 3)
            {
                // если что-то прослушивается...
                //if (Shout != null)
                //{
                //    // ...затем вызовите делегат
                //    Shout(this, EventArgs.Empty);                  
                //}
                // короткая версия проверки на null (метод инвок для выполнения делегата)
                Shout?.Invoke(this, EventArgs.Empty);
            }
        }

        public int CompareTo(Person? other)
        {
            if (Name is null) return 0;
            return Name.CompareTo(other?.Name);
        }
    }
}
