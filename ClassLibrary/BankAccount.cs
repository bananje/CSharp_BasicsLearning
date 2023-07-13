using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class BankAccount
    {
        public readonly DateTime Instantiated;
        public BankAccount() // конструктор
        {
            AccountName = "f";
        }

        public string AccountName; // член экземпляра
        public decimal Balance; // член экземпляра
        public static decimal InterestRate; // общий член для всех экзепляров класса
        public const string Species = "Homo Sapiens"; // константа
        

        public (string, int) GetFruit() // метод, возращающий кортеж
        {
            return ("Apples", 5);
        }

        public (string Name, int Number) GetNamed() // присвоение имён
        {
            return (Name: "Apples", Number: 5);
        }

        // деконструктор
        public void Deconstruct(out string name, out DateTime date)
        {
            name = AccountName;
            date = Instantiated;
        }
    }
}
