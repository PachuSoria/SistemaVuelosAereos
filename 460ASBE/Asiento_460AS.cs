using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBE
{
    public enum TipoAsiento_460AS
    {
        Normal,
        VIP
    }
    public class Asiento_460AS
    {
        public string NumAsiento_460AS { get; set; }
        public string CodVuelo_460AS { get; set; }
        public bool Disponible_460AS { get; set; }
        public TipoAsiento_460AS Tipo_460AS { get; set; }
        public Reserva_460AS Reserva_460AS { get; set; }
        public Asiento_460AS(string numAsiento_460AS, string codVuelo_460AS, bool disponible_460AS, TipoAsiento_460AS tipo_460AS, Reserva_460AS reserva_460AS = null) 
        {
            this.NumAsiento_460AS = numAsiento_460AS;
            this.CodVuelo_460AS = codVuelo_460AS;
            this.Disponible_460AS = disponible_460AS;
            this.Tipo_460AS = tipo_460AS;
            this.Reserva_460AS = reserva_460AS;
        }
    }
}
