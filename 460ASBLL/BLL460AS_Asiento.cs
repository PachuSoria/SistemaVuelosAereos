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
        private BLL460AS_DV _dvBLL;
        public BLL460AS_Asiento()
        {
            _asientoDAL = new DAL460AS_Asiento();
            _dvBLL = new BLL460AS_DV();
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

        public void AsignarReservaAsiento_460AS(string numAsiento, string codVuelo, string codReserva)
        {
            _asientoDAL.AsignarReservaAsiento_460AS(numAsiento, codVuelo, codReserva);
            _dvBLL.GuardarDV_460AS(new DV_460AS("Asiento_460AS"));
        }

        public void AgregarAsiento_460AS(Asiento_460AS asiento)
        {
            _asientoDAL.AgregarAsiento_460AS(asiento);
            _dvBLL.GuardarDV_460AS(new DV_460AS("Asiento_460AS"));
        }

        public void EliminarAsiento(string numAsiento, string codVuelo)
        {
            _asientoDAL.EliminarAsiento_460AS(numAsiento, codVuelo);
            _dvBLL.GuardarDV_460AS(new DV_460AS("Asiento_460AS"));
        }

        public List<Asiento_460AS> ObtenerAsientosDeReserva_460AS(string codReserva)
        {
            return _asientoDAL.ObtenerAsientosPorReserva_460AS(codReserva).ToList();
        }

        public void ActualizarCambioAsiento_460AS(Asiento_460AS viejo, Asiento_460AS nuevo)
        {
            DAL460AS_Asiento dal = new DAL460AS_Asiento();
            dal.LiberarAsiento_460AS(viejo.NumAsiento_460AS, viejo.CodVuelo_460AS);
            dal.OcuparAsiento_460AS(nuevo.NumAsiento_460AS, nuevo.CodVuelo_460AS, viejo.Reserva_460AS.CodReserva_460AS);
            _dvBLL.GuardarDV_460AS(new DV_460AS("Asiento_460AS"));
        }
    }
}
