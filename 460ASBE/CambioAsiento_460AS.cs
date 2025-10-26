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
        public List<(Asiento_460AS AsientoViejo_460AS, Asiento_460AS AsientoNuevo_460AS)> ListaCambios { get; set; }
            = new List<(Asiento_460AS, Asiento_460AS)>();

        public CambioAsiento_460AS(ComponenteReserva_460AS reserva, string numAsiento) : base(reserva)
        {
            NumAsiento_460AS = numAsiento;
        }
        public CambioAsiento_460AS() { }

        public CambioAsiento_460AS(ComponenteReserva_460AS reserva) : base(reserva)
        {
        }

        public override decimal CalcularPrecio()
        {
            return Precio_460AS;
        }

        public override string GetDescripcion()
        {
            return $"Cambio de asiento ({NumAsiento_460AS} – {Precio_460AS:0.00} USD)";
        }
    }
}
