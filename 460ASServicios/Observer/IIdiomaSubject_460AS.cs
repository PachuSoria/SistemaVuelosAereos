using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASServicios.Observer
{
    public interface IIdiomaSubject_460AS
    {
        void RegistrarObserver(IIdiomaObserver_460AS observer);
        void RemoverObserver(IIdiomaObserver_460AS observer);
        void NotificarObservers();
    }
}
