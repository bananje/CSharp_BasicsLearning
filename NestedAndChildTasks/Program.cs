// See https://aka.ms/new-console-template for more information
using static System.Console;

// Вложенные и дочерние задачи
static void OuterMethod()
{
    WriteLine("Outer method starting...");
    Task innerTask = Task.Factory.StartNew(InnerMethod);
    WriteLine("Outer method finished.");
}
static void InnerMethod()
{
    WriteLine("Inner method starting...");
    Thread.Sleep(2000);
    WriteLine("Inner method finished.");
}

Task outerTask = Task.Factory.StartNew(OuterMethod);
outerTask.Wait();
WriteLine("Console app is stopping.");