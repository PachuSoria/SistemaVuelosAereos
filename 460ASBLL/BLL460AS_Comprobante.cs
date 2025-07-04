using _460ASBE;
using _460ASDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_Comprobante
    {
        private DAL460AS_Comprobante _comprobanteDAL;
        public BLL460AS_Comprobante()
        {
            _comprobanteDAL = new DAL460AS_Comprobante();
        }

        public void GuardarComprobante_460AS(Comprobante_460AS comprobante)
        {
            _comprobanteDAL.GuardarComprobante(comprobante);
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
