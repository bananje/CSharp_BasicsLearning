using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter06
{
    public interface INoImplementation // С# 1.0 и более поздние версии
    {
        void Alpha(); // должен быть реализован производным типом
    }
    public interface ISomeImplementation // С# 8.0 и более поздние версии
    {
        void Alpha(); // должен быть реализован производным типом
        void Beta()
        {
            // реализация по умолчанию; может быть переопределен
        }
    }

    // абстрактный класс похож на интерфейс, обязательная реализация методов
    public abstract class PartiallyImplemented // С# 1.0 и более поздние версии
    {
        public abstract void Gamma(); // должен быть реализован производным типом

        public virtual void Delta() // может быть переопределен
        {
            // реализация
        }
    }
    public class FullyImplemented : PartiallyImplemented, ISomeImplementation
    {
        public void Alpha()
        {
            // реализация
        }
        public override void Gamma()
        {
            // реализация
        }
    }
    public sealed class AbstractClass // sealed запрещает наследование (а также используется в методах для блокировки переопределения)
    {
        FullyImplemented a = new();
        //PartiallyImplemented b = new(); // Ошибка компиляции!
        //ISomeImplementation c = new(); // Ошибка компиляции!
        //INoImplementation d = new(); // Ошибка компиляции!
    }
}
