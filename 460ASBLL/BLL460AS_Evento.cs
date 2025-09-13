using _460ASDAL;
using _460ASServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_Evento
    {
        DAL460AS_Evento _dal;
        public BLL460AS_Evento()
        {
            _dal = new DAL460AS_Evento();
        }

        public void GuardarEvento_460AS(Evento_460AS evento)
        {
            _dal.GuardarEvento_460AS(evento);
        }

        public List<Evento_460AS> ObtenerTodos_460AS()
        {
            return _dal.ObtenerTodos_460AS().ToList();
        }

        public Evento_460AS ObtenerUltimo_460AS()
        {
            return _dal.ObtenerUltimo_460AS();
        }

        public IList<Evento_460AS> ObtenerEventosUltimosTresDias_460AS()
        {
            DateTime hoy = DateTime.Today;
            DateTime haceTresDias = hoy.AddDays(-3);
            DateTime hasta = hoy.AddDays(1).AddSeconds(-1); 
            return _dal.ObtenerPorFechas_460AS(haceTresDias, hasta);
        }

        public IList<Evento_460AS> ObtenerEventosPorFechas_460AS(DateTime desde, DateTime hasta)
        {
            return _dal.ObtenerPorFechas_460AS(desde, hasta);
        }

        public IList<Evento_460AS> FiltrarEventos_460AS(DateTime desde, DateTime hasta, string actividadPrefijo = null, string usuario = null, string modulo = null, int? criticidad = null)
        {
            return _dal.FiltrarEventos_460AS(desde, hasta, actividadPrefijo, usuario, modulo, criticidad);
        }
    }
}
