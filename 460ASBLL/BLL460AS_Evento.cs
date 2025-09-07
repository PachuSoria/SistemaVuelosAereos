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
    }
}
