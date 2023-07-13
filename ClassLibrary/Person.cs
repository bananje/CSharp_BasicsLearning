using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public partial class Person
    {
        public string Name;
        public DateTime DateOfBirth;
        public WondersOfTheAncientWorld World;
        public WondersOfTheAncientWorld BucketList;
        public List<Person> Children = new List<Person>();
        public readonly string HomePlanet = "Earth";

        public void Deconstruct(out string name, out DateTime date)
        {
            name = Name;
            date = DateOfBirth;
        }
        public void PassingParameters(int x, ref int y, out int z)
        {
            // параметры out не могут иметь значения по умолчанию
            // и должны быть инициализированы внутри метода
            z = 99; 
            // увеличиваем каждый параметр
            x++; // не будет инкрементироваться
            y++;
            z++;
        }
    }
}
