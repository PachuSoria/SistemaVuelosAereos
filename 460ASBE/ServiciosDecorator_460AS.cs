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
        public string CodServicio_460AS { get; set; }
        public string TipoServicio_460AS { get; set; }
        public string Descripcion_460AS { get; set; }
        public decimal Precio_460AS { get; set; }
        public Reserva_460AS Reserva_460AS { get; set; }
        public ServiciosDecorator_460AS(ComponenteReserva_460AS reserva)
        {
            _reserva = reserva;
        }
        public ServiciosDecorator_460AS() { }

        public override decimal CalcularPrecio()
        {
            return _reserva.CalcularPrecio() + Precio_460AS;
        }

        public override string GetDescripcion()
        {
            return $"{_reserva.GetDescripcion()} + {Descripcion_460AS}";
        }
    }
}
