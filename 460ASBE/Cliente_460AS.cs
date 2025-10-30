using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _460ASBE
{
    [Serializable]
    public class Cliente_460AS
    {
        public string DNI_460AS { get; set; }
        public string Nombre_460AS { get; set;}
        public string Apellido_460AS { get; set; }
        public DateTime FechaNacimiento_460AS { get; set;}
        public int Telefono_460AS { get; set; }
        public string NroPasaporte_460AS { get; set; }
        public bool Eliminado_460AS { get; set; }
        public Cliente_460AS(string dni_460AS, string nombre_460AS, string apellido_460AS, DateTime fechaNacimiento_460AS, int telefono_460AS, string nroPasaporte_460AS) 
        {
            this.DNI_460AS = dni_460AS;
            this.Nombre_460AS = nombre_460AS;
            this.Apellido_460AS = apellido_460AS;
            this.FechaNacimiento_460AS = fechaNacimiento_460AS;
            this.Telefono_460AS = telefono_460AS;
            this.NroPasaporte_460AS = nroPasaporte_460AS;
            Eliminado_460AS = false;
        }

        public Cliente_460AS(string dni, string nombre, string apellido, DateTime fechaNacimiento, int telefono, string nroPasaporte, bool eliminado)
        {
            DNI_460AS = dni;
            Nombre_460AS = nombre;
            Apellido_460AS = apellido;
            FechaNacimiento_460AS = fechaNacimiento;
            Telefono_460AS = telefono;
            NroPasaporte_460AS = nroPasaporte;
            Eliminado_460AS = eliminado;
        }
        public Cliente_460AS() { }
    }
}
