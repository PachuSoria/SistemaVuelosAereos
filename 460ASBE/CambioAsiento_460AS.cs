using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBE
{
    public class CambioAsiento_460AS : ServiciosDecorator_460AS
    {
        public string NumAsiento_460AS { get; set; }
        public decimal Precio_460AS { get; set; }
        public CambioAsiento_460AS(ComponenteReserva_460AS reserva, string numAsiento, decimal precio) : base(reserva)
        {
            NumAsiento_460AS = numAsiento;
            Precio_460AS = precio;
        }

        public override decimal CalcularPrecio()
        {
            return _reserva.CalcularPrecio() + Precio_460AS;
        }

        public override string GetDescripcion()
        {
            return $"{_reserva.GetDescripcion()} + Cambio de asiento ({NumAsiento_460AS} – {Precio_460AS:0.00} USD)";
        }
    }
}
