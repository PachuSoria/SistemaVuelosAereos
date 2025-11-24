using _460ASBE;
using _460ASDAL;
using _460ASServicios;
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
        private BLL460AS_Evento _eventoBLL;
        private BLL460AS_DV _dvBLL;
        public BLL460AS_Vuelo() 
        {
            _vueloDAL = new DAL460AS_Vuelo();
            _eventoBLL = new BLL460AS_Evento();
            _dvBLL = new BLL460AS_DV();
        }

        public List<Vuelo_460AS> ObtenerVuelos_460AS()
        {
            return _vueloDAL.ObtenerVuelos_460AS().ToList();
        }

        public void AgregarVuelo_460AS(Vuelo_460AS vuelo)
        {
            _vueloDAL.AgregarVuelo_460AS(vuelo);
            var ultimo = _eventoBLL.ObtenerUltimo_460AS();
            var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 2, "Vuelos", $"Alta de vuelo: {vuelo.CodVuelo_460AS}");
            _eventoBLL.GuardarEvento_460AS(ev);
            _dvBLL.GuardarDV_460AS(new DV_460AS("Vuelo_460AS"));
        }

        public void ActualizarVuelo_460AS(Vuelo_460AS vuelo)
        {
            _vueloDAL.ActualizarVuelo_460AS(vuelo);
            var ultimo = _eventoBLL.ObtenerUltimo_460AS();
            var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 2, "Vuelos", $"Modificacion de vuelo: {vuelo.CodVuelo_460AS}");
            _eventoBLL.GuardarEvento_460AS(ev);
            _dvBLL.GuardarDV_460AS(new DV_460AS("Vuelo_460AS"));
        }

        public void EliminarVuelo_460AS(string codVuelo)
        {
            _vueloDAL.EliminarVuelo_460AS(codVuelo);
            var ultimo = _eventoBLL.ObtenerUltimo_460AS();
            var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 3, "Vuelos", $"Eliminacion de vuelo: {codVuelo}");
            _eventoBLL.GuardarEvento_460AS(ev);
            _dvBLL.GuardarDV_460AS(new DV_460AS("Vuelo_460AS"));
        }

        public Vuelo_460AS ObtenerVueloPorCodigo_460AS(string codVuelo)
        {
            return _vueloDAL.ObtenerVueloPorCodigo_460AS(codVuelo);
        }
    }
}
