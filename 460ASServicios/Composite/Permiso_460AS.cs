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
        public Permiso_460AS(string codigo, string nombre) 
        {
            Codigo_460AS = codigo;
            Nombre_460AS = nombre;
        }

        public void AgregarHijo(IComponentePermiso_460AS hijo)
        {
            throw new InvalidOperationException("Un permiso no puede tener hijos");
        }

        public void EliminarHijo(IComponentePermiso_460AS hijo)
        {
            throw new InvalidOperationException("Un permiso no puede eliminar hijos");
        }

        public bool EsIgual(IComponentePermiso_460AS otro) =>
            otro != null && otro.Codigo_460AS == this.Codigo_460AS;

        public List<IComponentePermiso_460AS> ObtenerHijos() => new List<IComponentePermiso_460AS>();

        public List<Permiso_460AS> ObtenerPermisos() => new List<Permiso_460AS> { this };
    }
}
