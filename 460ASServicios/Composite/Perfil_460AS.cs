using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASServicios.Composite
{
    public class Perfil_460AS : IComponentePermiso_460AS
    {
        public string Codigo_460AS { get; set; }
        public string Nombre_460AS { get; set; }
        private List<IComponentePermiso_460AS> _hijos;
        public Perfil_460AS(string codigo, string nombre)
        {
            Codigo_460AS = codigo;
            Nombre_460AS = nombre;
            _hijos = new List<IComponentePermiso_460AS>();
        }

        public void AgregarHijo(IComponentePermiso_460AS hijo)
        {
            _hijos.Add(hijo);
        }

        public void EliminarHijo(IComponentePermiso_460AS hijo)
        {
            _hijos.Remove(hijo);
        }

        public bool EsIgual(IComponentePermiso_460AS otro)
        {
            if (otro is Perfil_460AS otroPerfil)
            {
                return this.Codigo_460AS == otroPerfil.Codigo_460AS;
            }
            return false;
        }

        public List<Permiso_460AS> ObtenerPermisos()
        {
            List<Permiso_460AS> todosLosPermisos = new List<Permiso_460AS>();
            foreach (var hijo in _hijos)
            {
                todosLosPermisos.AddRange(hijo.ObtenerPermisos());
            }
            return todosLosPermisos;
        }

        public List<IComponentePermiso_460AS> ObtenerHijos()
        {
            return new List<IComponentePermiso_460AS>(_hijos); 
        }

        public override string ToString()
        {
            return $"{Nombre_460AS} ({Codigo_460AS})";
        }
    }
}
