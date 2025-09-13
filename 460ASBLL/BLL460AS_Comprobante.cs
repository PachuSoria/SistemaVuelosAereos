using _460ASBE;
using _460ASDAL;
using _460ASServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _460ASBLL
{
    public class BLL460AS_Comprobante
    {
        private DAL460AS_Comprobante _comprobanteDAL;
        private BLL460AS_Evento _eventoBLL;
        public BLL460AS_Comprobante()
        {
            _comprobanteDAL = new DAL460AS_Comprobante();
            _eventoBLL = new BLL460AS_Evento();
        }

        public List<Comprobante_460AS> ObtenerComprobantes_460AS()
        {
            return _comprobanteDAL.ObtenerTodos_460AS();
        }

        public void GuardarComprobante_460AS(Comprobante_460AS comprobante)
        {
            _comprobanteDAL.GuardarComprobante(comprobante);
            var eventoBLL = new BLL460AS_Evento();
            Evento_460AS ultimo = eventoBLL.ObtenerUltimo_460AS();
            var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 4, "Comprobantes", $"Generacion de comprobante: {comprobante.CodComprobante_460AS}");
            eventoBLL.GuardarEvento_460AS(ev);
        }

        private string GenerarCodigoComprobante_460AS()
        {
            Random rnd = new Random();
            const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string letrasParte = new string(Enumerable.Range(0, 4).Select(_ => letras[rnd.Next(letras.Length)]).ToArray());
            string numerosParte = rnd.Next(0, 10000).ToString("D4");
            return letrasParte + numerosParte;
        }

        public string GenerarCodigoComprobanteUnico_460AS()
        {
            string codigo;
            do
            {
                codigo = GenerarCodigoComprobante_460AS();
            } while (_comprobanteDAL.ExisteCodigoComprobante_460AS(codigo));
            return codigo;
        }
    }
}
