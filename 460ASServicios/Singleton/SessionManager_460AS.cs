using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASServicios.Singleton
{
    public class SessionManager_460AS
    {
        private static Session_460AS _instancia;
        private static Object _lock = new Object();
        public static Session_460AS Instancia
        {
            get
            {
                lock(_lock)
                {
                    if (_instancia == null)
                    {
                        _instancia = new Session_460AS();
                    }
                }
                return _instancia;
            }
        }
    }
}
