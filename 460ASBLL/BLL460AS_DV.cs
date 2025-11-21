using _460ASBE;
using _460ASDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_DV
    {
        private DAL460AS_DV _dal;
        public BLL460AS_DV() 
        {
            _dal = new DAL460AS_DV();
        }

        public void GuardarDV_460AS(DV_460AS dv)
        {
            dv.DVH_460AS = _dal.CalcularDVH_460AS(dv);
            dv.DVV_460AS = _dal.CalcularDVV_460AS(dv);
            _dal.GuardarDV_460AS(dv);
        }

        public List<string> CompararDV_460AS()
        {
            List<string> lista = new List<string>();
            foreach (DV_460AS item in _dal.ObtenerTodos_460AS())
            {
                if (_dal.CalcularDVH_460AS(item) != item.DVH_460AS || _dal.CalcularDVV_460AS(item) != item.DVV_460AS)
                {
                    lista.Add(item.NombreTabla_460AS);
                }
            }
            return lista;
        }

        public List<DV_460AS> ObtenerTodos_327LG()
        {
            return _dal.ObtenerTodos_460AS();
        }
    }
}
