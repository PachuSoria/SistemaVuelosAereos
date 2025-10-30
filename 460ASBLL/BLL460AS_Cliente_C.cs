using _460ASBE;
using _460ASDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _460ASBLL
{
    public class BLL460AS_Cliente_C
    {
        private DAL460AS_Cliente_C dalClienteC;
        public BLL460AS_Cliente_C()
        {
            dalClienteC = new DAL460AS_Cliente_C();
        }

        public List<Cliente_C_460AS> ObtenerTodos_460AS()
        {
            return dalClienteC.ObtenerTodos_460AS();
        }

        public List<Cliente_C_460AS> FiltrarClientesC_460AS(
            string dni = null,
            string nombre = null,
            string apellido = null,
            DateTime? fechaNacimiento = null,
            int? telefono = null,
            string nroPasaporte = null,
            DateTime? fechaCambio = null,
            bool? activo = null)
        {
            return dalClienteC.FiltrarClientesC_460AS(dni, nombre, apellido, fechaNacimiento, telefono, nroPasaporte, fechaCambio, activo);
        }

        public void ActivarCliente_460AS(string dni, DateTime fechaCambio)
        {
            dalClienteC.ActivarCliente_460AS(dni, fechaCambio);
        }
    }
}
