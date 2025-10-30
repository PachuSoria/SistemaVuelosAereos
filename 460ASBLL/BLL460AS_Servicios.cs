using _460ASBE;
using _460ASDAL;
using Microsoft.Data.SqlClient;
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

            if (string.IsNullOrWhiteSpace(servicio.CodServicio_460AS))
                servicio.CodServicio_460AS = Guid.NewGuid().ToString();

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
                    comida.CodServicio_460AS = servicioPlano.CodServicio_460AS;
                    dalServicios.GuardarComidaEspecial_460AS(comida);
                    break;

                case ValijaExtra_460AS valija:
                    valija.CodServicio_460AS = servicioPlano.CodServicio_460AS;
                    dalServicios.GuardarValijaExtra_460AS(valija);
                    break;

                case SeguroViaje_460AS seguro:
                    seguro.CodServicio_460AS = servicioPlano.CodServicio_460AS;
                    dalServicios.GuardarSeguroViaje_460AS(seguro);
                    break;

                case CambioAsiento_460AS cambio:

                    if (cambio.ListaCambios != null && cambio.ListaCambios.Any())
                    {
                        DAL460AS_Servicios dal = new DAL460AS_Servicios();
                        BLL460AS_Asiento bllAsiento = new BLL460AS_Asiento();

                        foreach (var det in cambio.ListaCambios)
                        {
                            dal.GuardarDetalleCambioAsiento_460AS(
                                cambio.CodServicio_460AS,
                                det.AsientoNuevo_460AS.NumAsiento_460AS
                            );

                            bllAsiento.ActualizarCambioAsiento_460AS(
                                det.AsientoViejo_460AS,
                                det.AsientoNuevo_460AS
                            );
                        }
                    }
                    break;
            }
        }
        public List<Servicio_460AS> ObtenerServiciosPorReserva_460AS(string codReserva)
        {
            return dalServicios.ObtenerServiciosPorReserva_460AS(codReserva);
        }
    }
}
