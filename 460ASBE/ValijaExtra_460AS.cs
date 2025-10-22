using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBE
{
    public class ValijaExtra_460AS : ServiciosDecorator_460AS
    {
        public int Cantidad_460AS { get; set; }
        public decimal PesoTotal_460AS { get; set; }
        public decimal Precio_460AS { get; set; }
        public ValijaExtra_460AS(ComponenteReserva_460AS reserva, int cantidad, decimal peso, decimal precio) : base(reserva)
        {
            Cantidad_460AS = cantidad;
            PesoTotal_460AS = peso;
            Precio_460AS = precio;
        }

        public override decimal CalcularPrecio()
        {
            return _reserva.CalcularPrecio() + Precio_460AS;
        }
        public override string GetDescripcion()
        {
            return $"{_reserva.GetDescripcion() + Precio_460AS} + Valija extra ({Cantidad_460AS} × {PesoTotal_460AS} kg – {Precio_460AS:0.00} USD)";
        }
    }
}
