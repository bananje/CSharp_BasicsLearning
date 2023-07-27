// See https://aka.ms/new-console-template for more information
using MonitoringLib;
using static System.Console;


//await foreach (int number in GetNumbersAsync())
//{
//    WriteLine($"Number: {number}");    
//}
//Recorder.Stop();

//async static IAsyncEnumerable<int> GetNumbersAsync()
//{
//    Recorder.Start();
//    Random r = new();
//    // имитация работы
//    await Task.Delay(r.Next(1500, 3000));
//    yield return r.Next(0, 1001);
//    await Task.Delay(r.Next(1500, 3000));
//    yield return r.Next(0, 1001);
//    await Task.Delay(r.Next(1500, 3000));
//    yield return r.Next(0, 1001);
//}

foreach (int number in GetNumbers())
{
    WriteLine($"Number: {number}");
}
static IEnumerable<int> GetNumbers()
{
    Recorder.Start();
    Random rnd = new();

    Thread.Sleep(rnd.Next(1500, 3000));
    yield return rnd.Next(1, 1000);

    Thread.Sleep(rnd.Next(1500, 3000));
    yield return rnd.Next(1, 1000);

    Thread.Sleep(rnd.Next(1500, 3000));
    yield return rnd.Next(1, 1000);
}

Recorder.Stop();