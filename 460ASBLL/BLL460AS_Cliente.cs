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
        private BLL460AS_Evento _eventoBLL;

        public BLL460AS_Cliente() 
        {
            _clienteDAL = new DAL460AS_Cliente();
            _eventoBLL = new BLL460AS_Evento();
        }

        public void GuardarCliente_460AS(Cliente_460AS cliente)
        {
            cliente.NroPasaporte_460AS = Cifrado_460AS.EncriptarPasaporteAES_460AS(cliente.NroPasaporte_460AS);
            var ultimo = _eventoBLL.ObtenerUltimo_460AS();
            _clienteDAL.GuardarCliente_460AS(cliente);
            var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 2, "Clientes", $"Alta de cliente: {cliente.DNI_460AS}");
            _eventoBLL.GuardarEvento_460AS(ev);
        }

        public List<Cliente_460AS> ObtenerClientes_460AS()
        {
            return _clienteDAL.ObtenerClientes_460AS().ToList();
        }

        public void ActualizarCliente_460AS(Cliente_460AS cliente)
        {
            _clienteDAL.ActualizarCliente_460AS(cliente);
            var ultimo = _eventoBLL.ObtenerUltimo_460AS();
            var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 2, "Clientes",$"Modificacion de cliente: {cliente.DNI_460AS}");
            _eventoBLL.GuardarEvento_460AS(ev);
        }

        public void EliminarCliente(string dni)
        {
            _clienteDAL.EliminarCliente_460AS(dni);
            var ultimo = _eventoBLL.ObtenerUltimo_460AS();
            var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 3, "Clientes", $"Baja de cliente: {dni}");
            _eventoBLL.GuardarEvento_460AS(ev);
        }
    }
}
