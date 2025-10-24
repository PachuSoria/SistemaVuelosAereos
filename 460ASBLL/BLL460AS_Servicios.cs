using _460ASBE;
using _460ASDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_Servicios
    {
        private DAL460AS_Servicios dalServicios;
        public BLL460AS_Servicios()
        {
            dalServicios = new DAL460AS_Servicios();
        }

        public void GuardarServicio_460AS(ServiciosDecorator_460AS servicio)
        {
            if (servicio == null)
                throw new Exception("El servicio no puede ser nulo.");

            var servicioPlano = new Servicio_460AS
            {
                CodServicio_460AS = servicio.CodServicio_460AS,
                CodReserva_460AS = (servicio.Reserva_460AS as Reserva_460AS)?.CodReserva_460AS,
                TipoServicio_460AS = servicio.TipoServicio_460AS,
                Descripcion_460AS = servicio.Descripcion_460AS,
                Precio_460AS = servicio.Precio_460AS
            };
            dalServicios.GuardarServicio_460AS(servicioPlano);

            switch (servicio)
            {
                case ComidaEspecial_460AS comida:
                    dalServicios.GuardarComidaEspecial_460AS(comida);
                    break;

                case ValijaExtra_460AS valija:
                    dalServicios.GuardarValijaExtra_460AS(valija);
                    break;

                case SeguroViaje_460AS seguro:
                    dalServicios.GuardarSeguroViaje_460AS(seguro);
                    break;

                case CambioAsiento_460AS cambio:
                    dalServicios.GuardarCambioAsiento_460AS(cambio);
                    break;

                default:
                    throw new Exception("Tipo de servicio no reconocido.");
            }
        }

        public void GuardarServicio_460AS(Servicio_460AS servicioPlano)
        {
            if (servicioPlano == null) throw new Exception("Servicio inválido.");
            dalServicios.GuardarServicio_460AS(servicioPlano);
        }

        public List<Servicio_460AS> ObtenerServiciosPorReserva_460AS(string codReserva)
        {
            return dalServicios.ObtenerServiciosPorReserva_460AS(codReserva);
        }
    }
}
