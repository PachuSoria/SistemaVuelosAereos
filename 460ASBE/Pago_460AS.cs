using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBE
{
    public class Pago_460AS
    {
        public string CodPago_460AS { get; set; }
        public Reserva_460AS Reserva_460AS { get; set; }
        public decimal Monto_460AS { get; set; }
        public string TipoPago_460AS { get; set; }
        public DateTime FechaPago_460AS { get; set; }
        public List<ServiciosDecorator_460AS> ServiciosPagados { get; set; }
        public Pago_460AS()
        {
            ServiciosPagados = new List<ServiciosDecorator_460AS>();
        }
        public Pago_460AS(string codPago_460AS, Reserva_460AS reserva_460AS, decimal monto_460AS, string tipoPago_460AS, DateTime fechaPago_460AS)
        {
            this.CodPago_460AS = codPago_460AS;
            this.Reserva_460AS = reserva_460AS;
            this.Monto_460AS = monto_460AS;
            this.TipoPago_460AS = tipoPago_460AS;
            this.FechaPago_460AS = fechaPago_460AS;
            this.ServiciosPagados = new List<ServiciosDecorator_460AS>();
        }
    }
}
