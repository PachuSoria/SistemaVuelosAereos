using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASServicios.Composite
{
    public class Permiso_460AS : IComponentePermiso_460AS
    {
        public string Codigo_460AS { get; set; }
        public string Nombre_460AS { get; set; }
        public string Descripcion_460AS { get; set; }
        public Permiso_460AS(string codigo, string nombre, string descripcion = null)
        {
            Codigo_460AS = codigo;
            Nombre_460AS = nombre;
            Descripcion_460AS = descripcion;
        }

        public Permiso_460AS() { }

        public void AgregarHijo(IComponentePermiso_460AS componente)
        {
            throw new NotImplementedException("No se pueden agregar hijos a un permiso simple.");
        }

        public void EliminarHijo(IComponentePermiso_460AS componente)
        {
            throw new NotImplementedException("No se pueden eliminar hijos de un permiso simple.");
        }

        public IComponentePermiso_460AS ObtenerHijo(IComponentePermiso_460AS componente)
        {
            return null;
        }

        public List<IComponentePermiso_460AS> ObtenerHijos()
        {
            return new List<IComponentePermiso_460AS>();
        }

        public List<Permiso_460AS> ObtenerPermisosSimples()
        {
            return new List<Permiso_460AS> { this };
        }

        public override string ToString()
        {
            return $"{Nombre_460AS} ({Codigo_460AS})";
        }
    }
}
