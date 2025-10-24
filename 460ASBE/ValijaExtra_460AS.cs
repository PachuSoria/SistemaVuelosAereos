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
        public ValijaExtra_460AS(ComponenteReserva_460AS reserva, int cantidad, decimal peso) : base(reserva)
        {
            Cantidad_460AS = cantidad;
            PesoTotal_460AS = peso;
        }
        public ValijaExtra_460AS() { }

        public override decimal CalcularPrecio()
        {
            return Precio_460AS;
        }
        public override string GetDescripcion()
        {
            return $"Valija extra ({Cantidad_460AS} × {PesoTotal_460AS} kg – {Precio_460AS:0.00} USD)";

        }
    }
}
