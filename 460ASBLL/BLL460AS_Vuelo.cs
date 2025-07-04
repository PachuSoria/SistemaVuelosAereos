using _460ASBE;
using _460ASDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_Vuelo
    {
        private DAL460AS_Vuelo _vueloDAL;
        public BLL460AS_Vuelo() 
        {
            _vueloDAL = new DAL460AS_Vuelo();
        }

        public List<Vuelo_460AS> ObtenerVuelos_460AS()
        {
            return _vueloDAL.ObtenerVuelos_460AS().ToList();
        }

        public void AgregarVuelo_460AS(Vuelo_460AS vuelo)
        {
            _vueloDAL.AgregarVuelo_460AS(vuelo);
        }

        public void ActualizarVuelo_460AS(Vuelo_460AS vuelo)
        {
            _vueloDAL.ActualizarVuelo_460AS(vuelo);
        }

        public void EliminarVuelo_460AS(string codVuelo)
        {
            _vueloDAL.EliminarVuelo_460AS(codVuelo);
        }
    }
}
