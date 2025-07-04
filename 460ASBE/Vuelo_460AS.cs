using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBE
{
    public class Vuelo_460AS
    {
        public string CodVuelo_460AS { get; set; }
        public string Aerolinea_460AS { get; set; }
        public string Origen_460AS { get; set; }
        public string Destino_460AS { get; set; }
        public DateTime FechaSalida_460AS { get; set; }
        public DateTime FechaLlegada_460AS { get; set; }
        public decimal PrecioVuelo_460AS { get; set; }
        public Vuelo_460AS(string codVuelo_460AS, string aerolinea_460AS, string origen_460AS, string destino_460AS, DateTime fechaSalida_460AS, DateTime fechaLlegada_460AS, decimal precioVuelo_460AS)
        {
            this.CodVuelo_460AS = codVuelo_460AS;
            this.Aerolinea_460AS = aerolinea_460AS;
            this.Origen_460AS = origen_460AS;
            this.Destino_460AS = destino_460AS;
            this.FechaSalida_460AS = fechaSalida_460AS;
            this.FechaLlegada_460AS = fechaLlegada_460AS;
            this.PrecioVuelo_460AS = precioVuelo_460AS;
        }
    }
}
