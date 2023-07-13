using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter06
{
    public interface IPlayable
    {
        void Play();
        void Pause();

        // void omled(); без реализации по умолчанию выдаёт ошибку компиляции

        void Stop() // реализация интерфейса по умолчанию
        {
            Console.WriteLine("Default implementation of Stop.");
        }
    }
}
