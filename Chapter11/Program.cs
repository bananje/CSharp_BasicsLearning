// See https://aka.ms/new-console-template for more information
using System.Xml.Linq;
using static System.Console;


string[] names = new[] { "Michael", "Pam", "Jim", "Dwight",
 "Angela", "Kevin", "Toby", "Creed" };

// Вопрос: какие имена начинаются с буквы M?
// (используем метод расширения LINQ)
var query1 = names.Where(name => name.EndsWith("m"));

// Вопрос: какие имена заканчиваются на букву M?
// (используем синтаксис написания запросов LINQ)
var query2 = from name in names where name.EndsWith("m") select name;

// ответ возвращается в виде массива строк, содержащих Pam и Jim
string[] result1 = query1.ToArray();
// ответ возвращается в виде списка строк, содержащих Pam и Jim
List<string> result2 = query2.ToList();

WriteLine(query1);
WriteLine(query2);

//foreach (string name in query1)
//{
//    WriteLine(name); // вывод Pam
//    names[2] = "Jimmy"; // изменение Jim на Jimmy
//                        // на второй итерации Jimmy не заканчивается на букву M
//}


//Делегат Func<string, bool> сообщает нам, что для каждой переменной string,
//передаваемой методу, он должен возвращать значение bool. Если метод возвращает
//true, то это значит, что нам следует включить string в результаты, а если false —
//исключить ее
static bool NameLongerThanFour(string name)
{
    return name.Length > 4;
}
//var query = names.Where(new Func<string, bool>(NameLongerThanFour));

//var query = names.Where(NameLongerThanFour); // поведение не изменяется

//var query = names.Where(name => name.Length > 4); // c использованием лямбда выражения

//var query = names.Where(name => name.Length > 4).OrderBy(name => name.Length); // сортировка по длине имени

var query = names.Where(name => name.Length > 4) // сортировка по нескольким условиям
                 .OrderBy(name => name.Length)
                 .ThenBy(name => name);

//query.ToList().ForEach(name => { WriteLine(name); });


// Фильтрация по типу

WriteLine("Filtering by type");
List<Exception> exceptions = new()
{
 new ArgumentException(),
 new SystemException(),
 new IndexOutOfRangeException(),
 new InvalidOperationException(),
 new NullReferenceException(),
 new InvalidCastException(),
 new OverflowException(),
 new DivideByZeroException(),
 new ApplicationException()
};

//удалить
//исключения, которые не являются арифметическими, и записать в консоль
//только арифметические исключения:
IEnumerable<ArithmeticException> arithmeticExceptionsQuery = exceptions.OfType<ArithmeticException>(); 

arithmeticExceptionsQuery.ToList().ForEach(ex => { WriteLine(ex); });