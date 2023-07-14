using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Chapter06
{
    // исключения для типа Person
    public class PersonException : Exception
    {
        public PersonException() : base() { }
        public PersonException(string message) : base(message) { }
        public PersonException(string message, Exception innerException)
        : base(message, innerException) { }

       
    }
}
