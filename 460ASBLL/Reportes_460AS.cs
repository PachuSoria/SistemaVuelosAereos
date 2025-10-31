using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _460ASBE;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO; 
using System.Drawing;

namespace _460ASBLL
{
    public class Reportes_460AS
    {
        public string GenerarComprobantePDF(Comprobante_460AS comprobante, string rutaCarpeta)
        {
            if (comprobante == null)
                throw new ArgumentNullException(nameof(comprobante));

            if (string.IsNullOrWhiteSpace(rutaCarpeta))
                throw new ArgumentException("La ruta de la carpeta no puede estar vacía.", nameof(rutaCarpeta));

            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            string nombreArchivo = $"Comprobante_{comprobante.CodComprobante_460AS}.pdf";
            string rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

            Document documento = new Document(PageSize.A4, 40f, 40f, 40f, 40f);
            try
            {
                PdfWriter.GetInstance(documento, new FileStream(rutaArchivo, FileMode.Create));
                documento.Open();

                var titulo = new Paragraph("Comprobante de Pago")
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f,
                    Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18)
                };
                documento.Add(titulo);

                documento.Add(new Paragraph($"Código: {comprobante.CodComprobante_460AS}"));
                documento.Add(new Paragraph($"Fecha: {comprobante.FechaPago_460AS:dd/MM/yyyy HH:mm}"));
                documento.Add(new Paragraph($"Monto: ${comprobante.Monto_460AS} USD"));
                documento.Add(new Paragraph($"Tipo de pago: {comprobante.TipoPago_460AS}"));
                documento.Add(new Paragraph(" "));

                documento.Add(new Paragraph($"Reserva: {comprobante.Reserva_460AS.CodReserva_460AS}"));


                documento.Close();
                return rutaArchivo;
            }
            catch (Exception)
            {
                if (documento.IsOpen())
                    documento.Close();
                throw;
            }
        }

        public string GenerarComprobanteServiciosPDF(Pago_460AS pago, string rutaCarpeta)
        {
            if (pago == null)
                throw new ArgumentNullException(nameof(pago));

            if (string.IsNullOrWhiteSpace(rutaCarpeta))
                throw new ArgumentException("La ruta de la carpeta no puede estar vacía.", nameof(rutaCarpeta));

            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            string nombreArchivo = $"Comprobante_Servicios_{pago.CodPago_460AS}.pdf";
            string rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

            Document documento = new Document(PageSize.A4, 40f, 40f, 40f, 40f);
            try
            {
                PdfWriter.GetInstance(documento, new FileStream(rutaArchivo, FileMode.Create));
                documento.Open();

                var titulo = new Paragraph("Comprobante de Servicios Adicionales")
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f,
                    Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18)
                };
                documento.Add(titulo);

                documento.Add(new Paragraph($"Código de Pago: {pago.CodPago_460AS}"));
                documento.Add(new Paragraph($"Fecha: {pago.FechaPago_460AS:dd/MM/yyyy HH:mm}"));
                documento.Add(new Paragraph($"Monto Total: ${pago.Monto_460AS} USD"));
                documento.Add(new Paragraph($"Tipo de Pago: {pago.TipoPago_460AS}"));
                documento.Add(new Paragraph(" "));

                if (pago.Reserva_460AS != null)
                    documento.Add(new Paragraph($"Reserva Asociada: {pago.Reserva_460AS.CodReserva_460AS}"));

                if (pago.Reserva_460AS?.Cliente_460AS != null)
                    documento.Add(new Paragraph($"Cliente DNI: {pago.Reserva_460AS.Cliente_460AS.DNI_460AS}"));

                documento.Add(new Paragraph(" "));
                documento.Add(new Paragraph("Servicios Adicionales Pagados:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));

                if (pago.ServiciosPagados != null && pago.ServiciosPagados.Count > 0)
                {
                    foreach (var s in pago.ServiciosPagados)
                    {
                        documento.Add(new Paragraph($"-{s.Descripcion_460AS} (${s.Precio_460AS})"));
                    }
                }
                else
                {
                    documento.Add(new Paragraph("No hay servicios asociados."));
                }

                documento.Close();
                return rutaArchivo;
            }
            catch (Exception)
            {
                if (documento.IsOpen())
                    documento.Close();
                throw;
            }
        }
    }
}
