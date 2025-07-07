using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASServicios.Composite
{
    public class Familia_460AS : IComponentePermiso_460AS
    {
        public string Codigo_460AS { get; set; }
        public string Nombre_460AS { get; set; }

        private List<IComponentePermiso_460AS> hijos = new List<IComponentePermiso_460AS>();
        public Familia_460AS(string codigo, string nombre)
        {
            Codigo_460AS = codigo;
            Nombre_460AS = nombre;
            hijos = new List<IComponentePermiso_460AS>();
        }

        public void AgregarHijo(IComponentePermiso_460AS hijo) => hijos.Add(hijo);

        public void EliminarHijo(IComponentePermiso_460AS hijo) => hijos.Remove(hijo);

        public bool EsIgual(IComponentePermiso_460AS otro) =>
            otro != null && otro.Codigo_460AS == this.Codigo_460AS;

        public List<IComponentePermiso_460AS> ObtenerHijos() => new List<IComponentePermiso_460AS>(hijos);

        public List<Permiso_460AS> ObtenerPermisos()
        {
            List<Permiso_460AS> permisos = new();

            foreach (var hijo in hijos)
            {
                permisos.AddRange(hijo.ObtenerPermisos());
            }

            return permisos;
        }

        public override string ToString()
        {
            return $"{Nombre_460AS} ({Codigo_460AS})";
        }
    }
}
