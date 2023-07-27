// See https://aka.ms/new-console-template for more information
using static System.Console;

string[] cohort1 = new[]
 { "Rachel", "Gareth", "Jonathan", "George" };
string[] cohort2 = new[]
 { "Jack", "Stephen", "Daniel", "Jack", "Jared" };
string[] cohort3 = new[]
 { "Declan", "Jack", "Jack", "Jasmine", "Conor" };

// вывод массивов
Output(cohort1, "Cohort 1");
Output(cohort2, "Cohort 2");
Output(cohort3, "Cohort 3");
Output(cohort2.Distinct(), "cohort2.Distinct()"); // убирает повторы
Output(cohort2.DistinctBy(name => name.Substring(0, 2)), // метод Substring позволяет указать начальный символ и длинну обработки
 "cohort2.DistinctBy(name => name.Substring(0, 2)):");
Output(cohort2.Union(cohort3), "cohort2.Union(cohort3)"); // объединение множеств с удалением повторов 
Output(cohort2.Concat(cohort3), "cohort2.Concat(cohort3)"); // стандартное объединение множеств
Output(cohort2.Intersect(cohort3), "cohort2.Intersect(cohort3)"); // находит пересечения (в данном случае одинаковые слова)
Output(cohort1.Zip(cohort2, (c1, c2) => $"{c1} matched with {c2}"), // сопоставление элеметов между множествами
 "cohort1.Zip(cohort2)");

static void Output(IEnumerable<string> cohort, string description = "")
{
    if (!string.IsNullOrEmpty(description))
    {
        WriteLine(description);
    }
    Write(" ");
    WriteLine(string.Join(", ", cohort.ToArray()));
    WriteLine();
}
