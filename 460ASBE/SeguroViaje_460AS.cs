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
        public decimal Precio_460AS { get; set; }
        public SeguroViaje_460AS(ComponenteReserva_460AS reserva, string cobertura, DateTime vencimiento, decimal precio) : base(reserva)
        {
            Cobertura_460AS = cobertura;
            Vencimiento_460AS = vencimiento;
            Precio_460AS = precio;
        }

        public override decimal CalcularPrecio()
        {
            return _reserva.CalcularPrecio() + Precio_460AS;
        }

        public override string GetDescripcion()
        {
            return $"{_reserva.GetDescripcion()} + Seguro de viaje {Cobertura_460AS} ({Precio_460AS:0.00} USD, vence {Vencimiento_460AS:dd/MM/yyyy})";
        }
    }
}
