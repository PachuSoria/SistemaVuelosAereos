using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASServicios.Composite
{
    public class Perfil_460AS : IComponentePermiso_460AS
    {
        public string Codigo_460AS {  get; set; }
        public string Nombre_460AS { get; set;  }
        private List<IComponentePermiso_460AS> _hijos = new();
        public Perfil_460AS () { }

        public Perfil_460AS(string codigo, string nombre)
        {
            Codigo_460AS = codigo;
            Nombre_460AS = nombre;
        }

        public void AgregarHijo(IComponentePermiso_460AS componente)
        {
            _hijos.Add(componente);
        }

        public void EliminarHijo(IComponentePermiso_460AS componente)
        {
            _hijos.Remove(componente);
        }

        public IComponentePermiso_460AS ObtenerHijo(IComponentePermiso_460AS componente)
        {
            return _hijos.FirstOrDefault(h => h.Codigo_460AS == componente.Codigo_460AS);
        }

        public List<IComponentePermiso_460AS> ObtenerHijos()
        {
            return new List<IComponentePermiso_460AS>(_hijos);
        }

        public List<Permiso_460AS> ObtenerPermisosSimples()
        {
            var permisos = new List<Permiso_460AS>();
            foreach (var hijo in _hijos) permisos.AddRange(hijo.ObtenerPermisosSimples());
            return permisos;
        }

        public override string ToString()
        {
            return $"{Nombre_460AS} ({Codigo_460AS})";
        }
    }
}
