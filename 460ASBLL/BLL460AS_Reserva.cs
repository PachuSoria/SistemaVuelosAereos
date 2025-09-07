using _460ASBE;
using _460ASDAL;
using _460ASServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_Reserva
    {
        private DAL460AS_Reserva _reservaDAL;
        private BLL460AS_Evento _eventoBLL;
        public BLL460AS_Reserva()
        {
            _reservaDAL = new DAL460AS_Reserva();
            _eventoBLL = new BLL460AS_Evento();
        }

        public void AgregarReserva_460AS(Reserva_460AS reserva)
        {
            _reservaDAL.AgregarReserva_460AS(reserva);
            Evento_460AS ultimo = _eventoBLL.ObtenerUltimo_460AS();
            var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 2, "Reservas", $"Reserva registrada - Código: {reserva.CodReserva_460AS}");
            _eventoBLL.GuardarEvento_460AS(ev);
        }

        private string GenerarCodigoReserva_460AS()
        {
            Random rnd = new Random();
            const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string letra1 = letras[rnd.Next(letras.Length)].ToString();
            string letra2 = letras[rnd.Next(letras.Length)].ToString();
            string numeros = rnd.Next(100, 1000).ToString(); 
            string letra3 = letras[rnd.Next(letras.Length)].ToString();
            return letra1 + letra2 + numeros + letra3;
        }

        public string GenerarCodigoReservaUnico_460AS()
        {
            string codigo;
            do
            {
                codigo = GenerarCodigoReserva_460AS();
            } while (_reservaDAL.ExisteCodigoReserva_460AS(codigo));
            return codigo;
        }
    }
}
