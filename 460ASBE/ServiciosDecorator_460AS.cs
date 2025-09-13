using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBE
{
    public abstract class ServiciosDecorator_460AS : ComponenteReserva_460AS
    {
        protected ComponenteReserva_460AS _reserva;
        public ServiciosDecorator_460AS(ComponenteReserva_460AS reserva)
        {
            _reserva = reserva;
        }

        public override decimal CalcularPrecio()
        {
            return _reserva.CalcularPrecio();
        }

        public override string GetDescripcion()
        {
            return _reserva.GetDescripcion();
        }
    }
}
