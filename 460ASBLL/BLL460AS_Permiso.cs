using _460ASDAL;
using _460ASServicios.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_Permiso
    {
        private DAL460AS_Permiso dalPermiso_460AS = new DAL460AS_Permiso();

        public List<Permiso_460AS> ObtenerTodos_460AS()
        {
            return dalPermiso_460AS.ObtenerTodos_460AS();
        }
    }
}
