using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBE
{
    public class SeguroViaje_460AS : ServiciosDecorator_460AS
    {
        public string Cobertura_460AS { get; set; }
        public DateTime Vencimiento_460AS { get; set; }
        public SeguroViaje_460AS(ComponenteReserva_460AS reserva, string cobertura, DateTime vencimiento) : base(reserva)
        {
            Cobertura_460AS = cobertura;
            Vencimiento_460AS = vencimiento;
        }
        public SeguroViaje_460AS() { }

        public SeguroViaje_460AS(ComponenteReserva_460AS reserva) : base(reserva)
        {
        }

        public override decimal CalcularPrecio()
        {
            return Precio_460AS;
        }

        public override string GetDescripcion()
        {
            return $"Seguro de viaje {Cobertura_460AS} ({Precio_460AS:0.00} USD, vence {Vencimiento_460AS:dd/MM/yyyy})";
        }
    }
}
