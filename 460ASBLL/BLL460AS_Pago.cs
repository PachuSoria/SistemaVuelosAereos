using _460ASBE;
using _460ASDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_Pago
    {
        private readonly DAL460AS_Pago dalPago;

        public BLL460AS_Pago()
        {
            dalPago = new DAL460AS_Pago();
        }

        public void GuardarPago_460AS(Pago_460AS pago)
        {
            if (pago == null)
                throw new Exception("El pago no puede ser nulo");

            if (pago.Reserva_460AS == null)
                throw new Exception("Debe asociar el pago a una reserva");

            if (pago.Monto_460AS <= 0)
                throw new Exception("El monto del pago debe ser mayor a cero");

            pago.CodPago_460AS = Guid.NewGuid().ToString();
            pago.FechaPago_460AS = DateTime.Now;

            dalPago.GuardarPago_460AS(pago);
        }

        public List<Pago_460AS> ObtenerPagosPorReserva_460AS(string codReserva)
        {
            if (string.IsNullOrWhiteSpace(codReserva))
                throw new Exception("Debe indicar una reserva válida");

            return dalPago.ObtenerPagosPorReserva_460AS(codReserva);
        }

        public List<Pago_460AS> ObtenerTodosLosPagos_460AS()
        {
            return dalPago.ObtenerTodosLosPagos_460AS();
        }

        public List<Pago_460AS> ObtenerPagosServicios_460AS()
        {
            return dalPago.ObtenerPagosServicios_460AS();
        }

        public List<string> ObtenerNombresServiciosDePago_460AS(string codPago)
        {
            return dalPago.ObtenerNombresServiciosDePago(codPago);
        }
    }
}

