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
        public ComidaEspecial_460AS(ComponenteReserva_460AS reserva, string tipo) : base(reserva)
        {
            TipoComida_460AS = tipo;
        }
        public ComidaEspecial_460AS() { }

        public ComidaEspecial_460AS(ComponenteReserva_460AS reserva) : base(reserva)
        {
        }

        public override decimal CalcularPrecio()
        {
            return (_reserva != null ? _reserva.CalcularPrecio() : 0m) + Precio_460AS;
        }

        public override string GetDescripcion()
        {
            string baseDesc = _reserva != null ? _reserva.GetDescripcion() + " + " : string.Empty;
            return $"{baseDesc}Comida especial {TipoComida_460AS} ({Precio_460AS:0.00} USD)";
        }
    }
}
