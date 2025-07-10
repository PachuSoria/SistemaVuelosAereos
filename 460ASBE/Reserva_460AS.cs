using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBE
{
    public class Reserva_460AS
    {
        public string CodReserva_460AS { get; set; }
        public DateTime FechaReserva_460AS { get; set; }
        public Cliente_460AS Cliente_460AS { get; set; }
        public Vuelo_460AS Vuelo_460AS { get; set; }
        public decimal PrecioTotal_460AS { get; set; }
        public List<Asiento_460AS> AsientosReservados_460AS { get; set; }
        public Reserva_460AS(string codReserva_460AS, DateTime fechaReserva_460AS, Cliente_460AS cliente_460AS, Vuelo_460AS vuelo_460AS, decimal precioTotal_460AS) 
        {
            this.CodReserva_460AS = codReserva_460AS;
            this.FechaReserva_460AS = fechaReserva_460AS;
            this.Cliente_460AS = cliente_460AS;
            this.Vuelo_460AS = vuelo_460AS;
            this.PrecioTotal_460AS = precioTotal_460AS;
            AsientosReservados_460AS = new List<Asiento_460AS>();
        }

        public Reserva_460AS() { }
    }
}
