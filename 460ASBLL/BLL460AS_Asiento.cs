using _460ASBE;
using _460ASDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_Asiento
    {
        private DAL460AS_Asiento _asientoDAL;
        public BLL460AS_Asiento()
        {
            _asientoDAL = new DAL460AS_Asiento();
        }

        public List<Asiento_460AS> ObtenerAsientos_460AS()
        {
            return _asientoDAL.ObtenerAsientos_460AS().ToList();
        }

        public List<Asiento_460AS> ObtenerAsientos_460AS(string codVuelo, TipoAsiento_460AS tipo)
        {
            return ObtenerAsientos_460AS().Where(a => a.CodVuelo_460AS == codVuelo && a.Tipo_460AS == tipo).ToList();
        }

        public List<Asiento_460AS> ObtenerAsientos_460AS(string codVuelo)
        {
            return ObtenerAsientos_460AS().Where(a => a.CodVuelo_460AS == codVuelo).ToList();
        }

        public void ActualizarEstadoAsiento_460AS(string numAsiento, string codVuelo)
        {
            _asientoDAL.ActualizarEstadoAsiento_460AS(numAsiento, codVuelo);
        }

        public void AsignarReservaAsiento_460AS(string numAsiento, string codVuelo, string codReserva)
        {
            _asientoDAL.AsignarReservaAsiento_460AS(numAsiento, codVuelo, codReserva);
        }

        public void AgregarAsiento_460AS(Asiento_460AS asiento)
        {
            _asientoDAL.AgregarAsiento_460AS(asiento);
        }

        public void ActualizarAsiento(Asiento_460AS asiento)
        {
            _asientoDAL.ActualizarAsiento_460AS(asiento);
        }

        public void EliminarAsiento(string numAsiento, string codVuelo)
        {
            _asientoDAL.EliminarAsiento_460AS(numAsiento, codVuelo);
        }
    }
}
