using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ENTIDAD.GENERICS
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static T _instancia;
        private static readonly Mutex Mutex = new Mutex();

        public static T Instancia
        {
            get
            {
                Mutex.WaitOne();
                if (_instancia == null) _instancia = new T();
                Mutex.ReleaseMutex();
                return _instancia;
            }
        }
    }
}
