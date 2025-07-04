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
    public class BLL460AS_Cliente
    {
        private DAL460AS_Cliente _clienteDAL;
        public BLL460AS_Cliente() 
        {
            _clienteDAL = new DAL460AS_Cliente();
        }

        public void GuardarCliente_460AS(Cliente_460AS cliente)
        {
            cliente.NroPasaporte_460AS = Cifrado_460AS.EncriptarPasaporteAES_460AS(cliente.NroPasaporte_460AS);
            _clienteDAL.GuardarCliente_460AS(cliente);
        }

        public List<Cliente_460AS> ObtenerClientes_460AS()
        {
            return _clienteDAL.ObtenerClientes_460AS().ToList();
        }

        public void ActualizarCliente_460AS(Cliente_460AS cliente)
        {
            cliente.NroPasaporte_460AS = Cifrado_460AS.EncriptarPasaporteAES_460AS(cliente.NroPasaporte_460AS);
            _clienteDAL.ActualizarCliente_460AS(cliente);
        }

        public void EliminarCliente(string dni)
        {
            _clienteDAL.EliminarCliente_460AS(dni);
        }
    }
}
