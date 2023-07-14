using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chapter06
{

    //Для того, чтобы создать метод расширения, вначале надо создать статический класс, который и будет содержать этот метод.
    //В данном случае это класс StringExtension.Затем объявляем статический метод.

    //Собственно метод расширения - это обычный статический метод, который в качестве первого параметра всегда принимает такую конструкцию: 
    //this имя_типа название_параметра, то есть в нашем случае this string str. 
    //Так как наш метод будет относиться к типу string, то мы и используем данный тип.

    // метод расширения для закрытого типа
    public static class StringExtensions 
    {
        public static bool IsValidEmail(this string input)
        {
            // используем простое регулярное выражение для проверки того,
            // что входная строка является действительным адресом
            // электронной почты
            return Regex.IsMatch(input,
            @"[a-zA-Z0-9\.-_]+@[a-zA-Z0-9\.-_]+");
        }
    }
}
