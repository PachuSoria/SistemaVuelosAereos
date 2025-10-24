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

        public override decimal CalcularPrecio()
        {
            return Precio_460AS;
        }

        public override string GetDescripcion()
        {
            return $"Comida especial {TipoComida_460AS} ({Precio_460AS:0.00} USD)";
        }
    }
}
