﻿using System.Xml.Serialization;

namespace WorkingWithSerialization
{
    public class Person
    {
        public Person() { }
        public Person(decimal initialSalary)
        {
            Salary = initialSalary;
        }

        [XmlAttribute("fname")] // cокращение памяти за счёт вывода данных в виде xml атрибутов
        public string? FirstName { get; set; }

        [XmlAttribute("lname")]
        public string? LastName { get; set; }

        [XmlAttribute("dob")]
        public DateTime DateOfBirth { get; set; }
        public HashSet<Person>? Children { get; set; }
        protected decimal Salary { get; set; }
    }
}
