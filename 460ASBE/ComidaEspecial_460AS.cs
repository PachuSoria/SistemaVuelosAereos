using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBE
{
    public class ComidaEspecial_460AS : ServiciosDecorator_460AS
    {
        public string TipoComida_460AS { get; set; }
        public decimal Precio_460AS { get; set; }
        public ComidaEspecial_460AS(ComponenteReserva_460AS reserva, string tipo, decimal precio) : base(reserva)
        {
            TipoComida_460AS = tipo;
            Precio_460AS = precio;
        }

        public override decimal CalcularPrecio()
        {
            return _reserva.CalcularPrecio() + Precio_460AS;
        }

        public override string GetDescripcion()
        {
            return $"{_reserva.GetDescripcion()} + Comida especial {TipoComida_460AS} ({Precio_460AS:0.00} USD)";
        }
    }
}
