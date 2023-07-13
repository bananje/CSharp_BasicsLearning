using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacktLibraryModern
{
    public class ImmutablePerson
    {
        public string? FirstName { get; init; } // init только для инициализации (изменять нельзя и переопределять)
        public string? LastName { get; init; }
    }
}
