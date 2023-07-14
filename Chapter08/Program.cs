// See https://aka.ms/new-console-template for more information
using System.Numerics;
using static System.Console;
using System.Globalization;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

WriteLine("Working with large integers:");
WriteLine("-----------------------------------");
ulong big = ulong.MaxValue;
WriteLine($"{big,40:N0}");

BigInteger bigger =
 BigInteger.Parse("123456789012345678901234567890");
WriteLine($"{bigger,40:N0}");


// работа с комплексными числами
WriteLine("Working with complex numbers:");
Complex c1 = new(real: 4, imaginary: 2);
Complex c2 = new(real: 3, imaginary: 7);
Complex c3 = c1 + c2;
// вывод с использованием реализации ToString по умолчанию
WriteLine($"{c1} added to {c2} is {c3}");
// вывод в пользовательском формате
WriteLine("{0} + {1}i added to {2} + {3}i is {4} + {5}i",
 c1.Real, c1.Imaginary,
 c2.Real, c2.Imaginary,
 c3.Real, c3.Imaginary);

// извлечение фрагмента строки
string fullName = "Alan Jones";
int indexOfTheSpace = fullName.IndexOf(' ');
string firstName = fullName.Substring(
 startIndex: 0, length: indexOfTheSpace);
string lastName = fullName.Substring(
 startIndex: indexOfTheSpace + 1);

WriteLine($"Original: {fullName}");
WriteLine($"Swapped: {lastName}, {firstName}");


// проверка содержимого в строках
string company = "Gross";
string g = "f";
bool startsWithG = company.StartsWith("G");
bool endsWithS = company.EndsWith("n");
WriteLine($"Начинается: {startsWithG}, заканчивается {endsWithS}");

string t = string.Join(g, company);
WriteLine(t);

// работа с DateTime
WriteLine("Earliest date/time value is: {0}",
 arg0: DateTime.MinValue);
WriteLine("UNIX epoch date/time value is: {0}",
 arg0: DateTime.UnixEpoch);
WriteLine("Date/time value Now is: {0}",
 arg0: DateTime.Now);
WriteLine("Date/time value Today is: {0}",
 arg0: DateTime.Today);


DateTime christmas = new(year: 2021, month: 12, day: 25);
WriteLine("Christmas: {0}",
 arg0: christmas); // дефолтный формат
WriteLine("Christmas: {0:dddd, dd MMMM yyyy}",
 arg0: christmas); // пользовательский формат
WriteLine("Christmas is in month {0} of the year.",
 arg0: christmas.Month);
WriteLine("Christmas is day {0} of the year.",
 arg0: christmas.DayOfYear);
WriteLine("Christmas {0} is on a {1}.",
 arg0: christmas.Year,
 arg1: christmas.DayOfWeek);

DateTime beforeXmas = christmas.Subtract(TimeSpan.FromDays(12)); // вычесть 12 дней
DateTime afterXmas = christmas.AddDays(12); // добавить 12 дней

WriteLine("12 days before Christmas is: {0}",
 arg0: beforeXmas);

WriteLine("12 days after Christmas is: {0}",
 arg0: afterXmas);

DateOnly queensBirthday = new(year: 2022, month: 4, day: 21);
TimeOnly partyStarts = new(hour: 20, minute: 30);

DateTime calendarEntry = queensBirthday.ToDateTime(partyStarts); // соединение даты и времени и приведение к общему формату


Regex testRegex = new(@"[0-9]{2}$");
string test = "92";

if (testRegex.IsMatch(test))
{
    WriteLine(true);
}


Write("Enter your age: ");
string? input = ReadLine();
Regex ageChecker = new(@"\d"); // преффикс d - цифры

//В регулярных выражениях начало ввода обозначается символом каретки ^,
//а конец — символом доллара $. Мы воспользуемся этими символами, чтобы
//указать, что между началом и концом ввода не ожидаем ничего другого, кроме
//цифры.
ageChecker = new(@"^\d+$");
if (ageChecker.IsMatch(input))
{
    WriteLine("Thank you!");
}
else
{
    WriteLine($"This is not a valid age: {input}");
}

// Разбивка сложных строк, разделенных запятыми

string films = "\"Monsters, Inc.\",\"I, Tonya\",\"Lock, Stock and Two Smoking Barrels\"";
WriteLine($"Films to split: {films}");
string[] filmsDumb = films.Split(',');
WriteLine("Splitting with string.Split method:");
foreach (string film in filmsDumb)
{
    WriteLine(film);
}

WriteLine();
Regex csv = new(
 "(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)");
MatchCollection filmsSmart = csv.Matches(films);
WriteLine("Splitting with regular expression:");
foreach (Match film in filmsSmart)
{
    WriteLine(film.Groups[2].Value);
}

List<string> names = new();
names.EnsureCapacity(10_000);
// загружаем десять тысяч имен в список 