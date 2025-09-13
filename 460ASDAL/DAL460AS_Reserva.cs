using _460ASBE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Reserva
    {
        private string cx;
        public DAL460AS_Reserva()
        {
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True";
        }

        public void AgregarReserva_460AS(Reserva_460AS reserva)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO RESERVA_460AS (CodReserva_460AS, DNICliente_460AS, FechaReserva_460AS, CodVuelo_460AS, PrecioTotal_460AS) " +
                    "VALUES (@CodReserva_460AS, @DNICliente_460AS, @FechaReserva_460AS, @CodVuelo_460AS, @PrecioTotal_460AS)", conexion);

                cmd.Parameters.AddWithValue("@CodReserva_460AS", reserva.CodReserva_460AS);
                cmd.Parameters.AddWithValue("@DNICliente_460AS", reserva.Cliente_460AS.DNI_460AS);
                cmd.Parameters.AddWithValue("@FechaReserva_460AS", reserva.FechaReserva_460AS);
                cmd.Parameters.AddWithValue("@CodVuelo_460AS", reserva.Vuelo_460AS.CodVuelo_460AS);
                cmd.Parameters.AddWithValue("@PrecioTotal_460AS", reserva.PrecioTotal_460AS);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool ExisteCodigoReserva_460AS(string codReserva)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM RESERVA_460AS WHERE CodReserva_460AS = @CodReserva_460AS", conexion);
                cmd.Parameters.AddWithValue("@CodReserva_460AS", codReserva);
                conexion.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public List<Reserva_460AS> ObtenerReservasCliente_460AS(string dniCliente)
        {
            var reservas = new List<Reserva_460AS>();

            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = @"SELECT CodReserva_460AS, FechaReserva_460AS, CodVuelo_460AS, PrecioTotal_460AS
                                FROM RESERVAS_460AS
                                WHERE DNICliente_460AS = @DNICliente_460AS";

                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@DNICliente_460AS", dniCliente);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var reserva = new Reserva_460AS
                        {
                            CodReserva_460AS = reader["CodReserva_460AS"].ToString(),
                            FechaReserva_460AS = Convert.ToDateTime(reader["FechaReserva_460AS"]),
                            Cliente_460AS = new Cliente_460AS { DNI_460AS = dniCliente }, 
                            Vuelo_460AS = new Vuelo_460AS { CodVuelo_460AS = reader["CodVuelo_460AS"].ToString() },
                            PrecioTotal_460AS = Convert.ToDecimal(reader["PrecioTotal_460AS"])
                        };
                        reservas.Add(reserva);
                    }
                }
            }
            return reservas;
        }
    }
}
