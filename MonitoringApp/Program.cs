// See https://aka.ms/new-console-template for more information

using MonitoringLib;
using System.Text;
using static System.Console;

//WriteLine("Processing. Please wait...");
//Recorder.Start();

//// имитируем процесс использования ресурсов памяти...
//int[] largeArrayOfInts = Enumerable.Range(
// start: 1, count: 10_000).ToArray();

//// ...требуется некоторое время на выполнение
//Thread.Sleep(new Random().Next(5, 10) * 1000);

//Recorder.Stop();

// обработка строк с помощью string
int[] numbers = Enumerable.Range(1, 50_000).ToArray();
WriteLine("Using string with +");

Recorder.Start();
string str = string.Empty;
for (int i = 0; i < numbers.Length; i++)
{
    str += numbers[i] + ", ";
}
Recorder.Stop();

WriteLine("Using StringBuilder");
Recorder.Start();
StringBuilder stringBuilder = new();
for (int i = 0; i < numbers.Length; i++)
{
    stringBuilder.Append(numbers[i]);
    stringBuilder.Append(", ");
}
Recorder.Stop();
