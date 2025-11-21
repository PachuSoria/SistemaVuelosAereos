using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBE
{
    public class DV_460AS
    {
        public string NombreTabla_460AS { get; set; }
        public string DVH_460AS { get; set; }
        public string DVV_460AS {  get; set; }
        public DV_460AS() { }
        public DV_460AS(string nombreTabla, string dvh, string dvv) 
        {
            NombreTabla_460AS = nombreTabla;
            DVH_460AS = dvh;
            DVV_460AS = dvv;
        }
    }
}
