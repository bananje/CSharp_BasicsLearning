// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;
using System.Xml;
using static System.Console; // статический импорт
using static System.Convert;

namespace CP
{
    // F9 - установить точку останова

    class TestProject
    {
        private const string firstname = "Omar";
        private const string lastname = "Rudberg";
        private const string fullname = firstname + " " + lastname;

        // форматирование с помощью интерполированных строк
        private const string fullname1 = $"{firstname} {lastname}";

        // валидация ОС
        public static void OperatingSystemValidation()
        {
            if (OperatingSystem.IsWindows())
            {
                // выполнить код, работающий только в Windows
            }
            else if (OperatingSystem.IsWindowsVersionAtLeast(major: 10))
            {
                // выполнить код, работающий только в Windows 10 или более поздней версии
            }
            else if (OperatingSystem.IsIOSVersionAtLeast(major: 14, minor: 5))
            {
                // выполнить код, работающий только в iOS 14.5 или более поздней версии
            }
            else if (OperatingSystem.IsBrowser())
            {
                // выполнить код, работающий только в браузере с Blazor
            }

        }

        public static void DataTypeValidation()
        {
            int omled = 1;
            // типы данных
            char symbol = 'd'; // литерал
            string text = "123456789"; //литеральная строка
            string path = @"C:\telev\sony\tr.exe"; // дословная литеральная строка  использование ссылки в переменной

            uint naturalNumber = 23; // целое число без знака означает положительное целое число или 0
            int integerNumber = -23; // целое число означает отрицательное или положительное целое число или 0
            float realNumber = 2.3F; // float означает число одинарной точности с плавающей запятой. суффикс F указывает, что это литерал типа float
            double anotherRealNumber = 2.3; // double означает число двойной точности с плавающей запятой
            int decimalNotation = 2_000_000; // запись большого числа с помощью нижнего подчёркивания

            // динамический тип данных
            dynamic dynamicValue = "OLm";
            dynamicValue = 1;

            // Приставки - типы данных
            // L означает long;
            // UL означает ulong;
            // M означает decimal;
            // D означает double;
            // F означает float.
            var number = 1M;

            XmlDocument xml = new(); // короткая запись объвления нового экземпляра объекта

            var defaultStringValue = default(string); // ключ слово default позволяет получить дефолнтное значение любого типа, а также обнулить существующие

            double t = 0.1;
            int h = ToInt32(t); // преобразование с помощью Convert (пространство статически импортировано выше)

            // метод tryparse
            string? input = ReadLine(); 
            if(int.TryParse(input, out int num))
            {
                Write($"Число {num} преобразовано");
            }
            else
            {
                WriteLine($"{input} не является числом!");
            }




            // nameof - название переменной

            //WriteLine($"Имя переменной {nameof(omled)} имеет значение {number}");
            //WriteLine($"Вес типа double {sizeof(double)}");
        }

        public void ExceptionValidation()
        {
           //Оператор checked дает.NET команду при возникновении переполнения вызывать
           //исключение, вместо того чтобы позволять ему происходить автоматически, что
           //делается по умолчанию из соображений производительности.

            // оператор checked предупреждает о переполнении
            try
            {
                checked
                {
                    int x = int.MaxValue - 1;
                    WriteLine($"Initial value: {x}");
                    x++;
                    WriteLine($"After incrementing: {x}");
                    x++;
                    WriteLine($"After incrementing: {x}");
                    x++;
                    WriteLine($"After incrementing: {x}");
                }
            }
            catch (OverflowException)
            {
                WriteLine("The code overflowed but I caught the exception.");
            }


        }

        public static void CyclesHandle()
        {
            // цикл while
            #region while
            int x = 0;
            while (x < 10)
            {
                WriteLine(x);
                x++;
            }
            #endregion

            // оператор do
            #region do/while
            string? password;
            int count = 0;

            do // выполняется хотя бы раз в любом случае
            {
                if (count == 10)
                {
                    WriteLine("Максимальное количество попыток");
                    return;
                }
                Write("Введите пароль:");
                password = ReadLine();
                count++;
            }
            while (password == ""); // проверка условия
            #endregion 
                       
            WriteLine("Успешно");           
        }

        public static void VariableHandle()
        {
            int number = (new Random()).Next(1, 7);
            WriteLine($"My random number is {number}");
            switch (number)
            {
                case 1:
                    WriteLine("One");
                    break; // переход в конец оператора switch
                case 2:
                    WriteLine("Two");
                    goto case 1;
                case 3: // блок, содержащий несколько случаев
                case 4:
                    WriteLine("Three or four");
                    goto case 1;
                case 5:
                    goto A_label; // ссылка на именованную метку
                default:
                    WriteLine("Default");
                    break;
            } // конец оператора switch
            WriteLine("After end of switch");

        A_label: // именованная метка
            WriteLine($"After A_label");
        }

        public static void ShortSwitchHandle()
        {
            string path = @"C:\Users\vgerm\OneDrive\Рабочий стол\C# Study\HelloCS\TopLevelProgram\Files\";
            Write("Press R for read-only or W for writeable");
            ConsoleKeyInfo key = ReadKey();
            WriteLine();

            Stream? s;

            if(key.Key == ConsoleKey.R)
            {
                s = File.Open(
                    Path.Combine(path, "file.txt"),
                    FileMode.OpenOrCreate,
                    FileAccess.Read);
            }
            else if(key.Key == ConsoleKey.W)
            {
                s = File.Open(
                    Path.Combine(path, "file.txt"),
                    FileMode.OpenOrCreate,
                    FileAccess.Write);
            }
            else
            {
                s = null;
            }
            
            string message;

            message = s switch // короткий вариант записи switch
            {
                FileStream writeableFile when s.CanWrite // when - точное сопосталвение с образцом
                    => "Файл можно записать",
                FileStream readOnlyFile
                    => "Файл можно только прочесть",
                _ // символ замена ключевого слова default
                    => "Дефолтное отображение"
            };
            WriteLine(message);
        }

        public static void ConsoleOutputFormater()
        {
            // форматирование вывода на консоль alignment
            string i, t;
            i = "Name";
            t = "Count";
            WriteLine(format: "{0, -3} {1, 8}", arg0: i, arg1: t);
        }

        // Рекурсивные функции
        static int Factorial(int num)
        {
            if(num < 1)
            {
                return 0;
            }
            else if(num == 1)
            {
                return 1;
            }
            else
            {
                return num * Factorial(num- 1);
            }
        }

        // императивная функция
        static int FibImperative(int term)
        {
            if(term == 1)
            {
                return 0;
            }
            else if(term == 2)
            {
                return 1;
            }

            else
            {
                return FibImperative(term - 1) + FibImperative(term - 2);
            }
        }
        // декларативная функция
        static int FibFunctional(int term) =>
            term switch
            {
              1 => 0,
              2 => 1,
              _ => FibFunctional(term - 1) + FibFunctional(term - 2)
            };             



        public static void Main()
        {
            //for (int i = 0; i < 15; i++)
            //{
            //    WriteLine($"{i}! = {Factorial(i):N0}");
            //}

            
            ReadKey();
        }

    }
}

