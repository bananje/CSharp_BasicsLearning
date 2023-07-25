using Microsoft.Extensions.Logging;
using static System.Console;

namespace WorkingWithEFCore
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new ConsoleLogger();
        }

        public void Dispose()
        {
            
        }
    }

    public class ConsoleLogger : ILogger
    {
        // если средство ведения журнала использует неуправляемые ресурсы,
        // то можно вернуть здесь класс, реализующий Idisposable
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // чтобы избежать переполнения журнала, можно выполнить фильтрацию
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Information:
                case LogLevel.None:
                    return false;
                case LogLevel.Debug:
                case LogLevel.Warning:
                case LogLevel.Error:
                case LogLevel.Critical:
                default:
                    return true;
            };
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {

            if(eventId.Id == 20100)
            {
                Write("Level: {0}, Event Id: {1}, Event: {2}", logLevel, eventId.Id, eventId.Name);
            }

            // пишем в журнал уровень и идентификатор события
            Write($"Level: {logLevel}, Event Id: {eventId.Id}");

            // выводим только существующее состояние или исключение
            if (state != null)
            {
                Write($", State: {state}");
            }
            if (exception != null)
            {
                Write($", Exception: {exception.Message}");
            }
            WriteLine();
        }
    }
}
