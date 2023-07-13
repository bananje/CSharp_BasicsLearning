using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter06
{
    public class Animal : IDisposable
    {
        public Animal()
        {

        }

        ~Animal() // финализатор
        {
            Dispose(false);
        }

        bool isDisposed = false; // Ресурсы были освобождены?
        public void Dispose()
        {
            Dispose(true);

            // сообщаем сборщику мусора, что ему не нужно вызывать финализатор
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;
            // освобождаем *неуправляемый* ресурс
            // ...
            if (disposing)
            {
                // освобождаем любые другие *управляемые* ресурсы
                // ...
            }
            isDisposed = true;
        }
    }
}
