using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBE
{
    public class Comprobante_460AS
    {
        public string CodComprobante_460AS { get; set; }
        public  Reserva_460AS Reserva_460AS { get; set; }
        public decimal Monto_460AS { get; set; }
        public string TipoPago_460AS { get; set; }
        public DateTime FechaPago_460AS { get; set; }
        public Comprobante_460AS(string codComprobante_460AS, Reserva_460AS reserva_460AS, decimal monto_460AS, string tipoPago_460AS, DateTime fechaPago_460AS) 
        {
            this.CodComprobante_460AS = codComprobante_460AS;
            this.Reserva_460AS = reserva_460AS;
            this.Monto_460AS = monto_460AS;
            this.TipoPago_460AS = tipoPago_460AS;
            this.FechaPago_460AS = fechaPago_460AS;
        }

        public Comprobante_460AS() { }
    }
}
