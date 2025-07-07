using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASServicios.Composite
{
    public interface IComponentePermiso_460AS
    {
        string Codigo_460AS { get; set; }      
        string Nombre_460AS { get; set; }

        void AgregarHijo(IComponentePermiso_460AS hijo);
        void EliminarHijo(IComponentePermiso_460AS hijo);

        bool EsIgual(IComponentePermiso_460AS otro);

        List<IComponentePermiso_460AS> ObtenerHijos();
        List<Permiso_460AS> ObtenerPermisos();
    }
}
