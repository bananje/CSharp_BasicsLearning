using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public partial class Person
    {
        public string Origin
        {
            get
            {
                return $"{Name} was born on {HomePlanet}";
            }
        }
        // два свойства, определенные с помощью синтаксиса
        // лямбда-выражений C# 6+
        public string Greeting => $"{Name} says 'Hello!'";
        public int Age => System.DateTime.Today.Year - DateOfBirth.Year;

        public string FavoriteIceCream { get; set; } // автосинтаксис

        private string favoritePrimaryColor;

        public string FavouritePrimaryColor
        {
            get
            {
                return favoritePrimaryColor;
            }
            set
            {
                switch (value.ToLower())
                {
                    case "red":
                    case "green":
                    case "blue":
                        favoritePrimaryColor = value;
                        break;
                    default:
                        throw new ArgumentException(
                            $"{value} is not a primary color. " +
                            "Choose from: red, green, blue.");
                }
            }
        }
    }
}
