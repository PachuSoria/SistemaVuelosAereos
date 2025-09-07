using _460ASServicios.Singleton;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASServicios
{
    public class Evento_460AS
    {
        public string IdEvento_460AS { get; set; }
        public string Usuario_460AS { get; set; }   
        public DateTime Fecha_460AS { get; set; }
        public string Modulo_460AS { get; set; }
        public string Actividad_460AS { get; set; }
        public int Criticidad_460AS { get; set; }

        public Evento_460AS(string idEvento, string usuario, DateTime fecha, string modulo, string actividad, int criticidad)
        {
            IdEvento_460AS = idEvento;
            Usuario_460AS = usuario;
            Fecha_460AS = fecha;
            Modulo_460AS = modulo;
            Actividad_460AS = actividad;
            Criticidad_460AS = criticidad;
        }

        public Evento_460AS() { }

        public static Evento_460AS GenerarEvento_460AS(Evento_460AS evento, int criticidad, string modulo, string actividad)
        {
            int numero = 1;
            if (evento != null)
            {
                var partes = evento.IdEvento_460AS.Split('-');
                var fecha = DateTime.ParseExact(partes[0], "yyyyMMdd", null);
                if (fecha.Date == DateTime.Now.Date) numero = int.Parse(partes[1]) + 1;
            }
            string id_evento = $"{DateTime.Now:yyyyMMdd}-{numero:D3}";
            string usuario = SessionManager_460AS.Instancia.Usuario != null
            ? SessionManager_460AS.Instancia.Usuario.Login_460AS
            : string.Empty;
            return new Evento_460AS(id_evento, usuario, DateTime.Now, modulo, actividad, criticidad);
        }
    }
}
