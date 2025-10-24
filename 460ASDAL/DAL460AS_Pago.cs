using _460ASBE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Pago
    {
        private string cx;
        public DAL460AS_Pago()
        {
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True";
        }

        public void GuardarPago_460AS(Pago_460AS pago)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = @"INSERT INTO PAGO_460AS 
                            (CodPago_460AS, Monto_460AS, FechaPago_460AS, TipoPago_460AS, CodReserva_460AS, DNICliente_460AS)
                            VALUES 
                            (@CodPago_460AS, @Monto_460AS, @FechaPago_460AS, @TipoPago_460AS, @CodReserva_460AS, @DNICliente_460AS)";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    if (pago == null) throw new Exception("El objeto pago es nulo.");
                    if (pago.Reserva_460AS == null) throw new Exception("La reserva del pago no puede ser nula.");
                    if (pago.Reserva_460AS.Cliente_460AS == null) throw new Exception("El cliente del pago no puede ser nulo.");

                    cmd.Parameters.AddWithValue("@CodPago_460AS", pago.CodPago_460AS);
                    cmd.Parameters.AddWithValue("@Monto_460AS", pago.Monto_460AS);
                    cmd.Parameters.AddWithValue("@FechaPago_460AS", pago.FechaPago_460AS);
                    cmd.Parameters.AddWithValue("@TipoPago_460AS", pago.TipoPago_460AS ?? "Desconocido");
                    cmd.Parameters.AddWithValue("@CodReserva_460AS", pago.Reserva_460AS.CodReserva_460AS);
                    cmd.Parameters.AddWithValue("@DNICliente_460AS", pago.Reserva_460AS.Cliente_460AS.DNI_460AS);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            GuardarServiciosPagados_460AS(pago);
        }

        private void GuardarServiciosPagados_460AS(Pago_460AS pago)
        {
            if (pago.ServiciosPagados == null || pago.ServiciosPagados.Count == 0)
                return;

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = @"INSERT INTO PAGO_SERVICIOS_460AS
                                    (CodPago_460AS, CodServicio_460AS)
                                    VALUES (@CodPago_460AS, @CodServicio_460AS)";
                conexion.Open();

                foreach (var s in pago.ServiciosPagados)
                {
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@CodPago_460AS", pago.CodPago_460AS);
                        cmd.Parameters.AddWithValue("@CodServicio_460AS", s.CodServicio_460AS);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<Pago_460AS> ObtenerPagosPorReserva_460AS(string codReserva)
        {
            var lista = new List<Pago_460AS>();

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = @"SELECT * FROM PAGO_460AS 
                                    WHERE CodReserva_460AS = @CodReserva_460AS";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@CodReserva_460AS", codReserva);
                    conexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pago = new Pago_460AS
                            {
                                CodPago_460AS = reader["CodPago_460AS"].ToString(),
                                Monto_460AS = Convert.ToDecimal(reader["Monto_460AS"]),
                                TipoPago_460AS = reader["TipoPago_460AS"].ToString(),
                                FechaPago_460AS = Convert.ToDateTime(reader["FechaPago_460AS"]),
                                Reserva_460AS = new Reserva_460AS
                                {
                                    CodReserva_460AS = reader["CodReserva_460AS"].ToString()
                                }
                            };
                            lista.Add(pago);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
