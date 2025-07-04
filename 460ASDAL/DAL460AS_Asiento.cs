using _460ASBE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Asiento
    {
        private string cx;
        public DAL460AS_Asiento() 
        {
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True";
        }

        public IList<Asiento_460AS> ObtenerAsientos_460AS()
        {
            List<Asiento_460AS> listaAsientos = new List<Asiento_460AS>();

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM ASIENTO_460AS", conexion);
                conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    bool disponible_460AS = Convert.ToBoolean(reader["Disponible_460AS"]);
                    string numAsiento_460AS = reader["NumAsiento_460AS"].ToString();
                    string codVuelo_460AS = reader["CodVuelo_460AS"].ToString();
                    string tipo_460AS = reader["Tipo_460AS"].ToString();
                    TipoAsiento_460AS tipo = (TipoAsiento_460AS)Enum.Parse(typeof(TipoAsiento_460AS), tipo_460AS);
                    Reserva_460AS reserva_460AS = null;

                    Asiento_460AS asiento = new Asiento_460AS(numAsiento_460AS, codVuelo_460AS, disponible_460AS, tipo, reserva_460AS);
                    listaAsientos.Add(asiento);
                }
            }
            return listaAsientos;
        }

        public void ActualizarEstadoAsiento_460AS(string numAsiento, string codVuelo)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string sql = @"
                UPDATE ASIENTO_460AS
                SET Disponible_460AS = 1, 
                CodReserva_460AS = NULL
                WHERE NumAsiento_460AS = @NumAsiento_460AS AND CodVuelo_460AS = @CodVuelo_460AS";

                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@NumAsiento_460AS", numAsiento);
                cmd.Parameters.AddWithValue("@CodVuelo_460AS", codVuelo);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AsignarReservaAsiento_460AS(string numAsiento, string codVuelo, string codReserva)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string sql = "UPDATE ASIENTO_460AS SET Disponible_460AS = 0, CodReserva_460AS = @CodReserva_460AS WHERE NumAsiento_460AS = @NumAsiento_460AS AND CodVuelo_460AS = @CodVuelo_460AS";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@CodReserva_460AS", codReserva);
                cmd.Parameters.AddWithValue("@NumAsiento_460AS", numAsiento);
                cmd.Parameters.AddWithValue("@CodVuelo_460AS", codVuelo);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AgregarAsiento_460AS(Asiento_460AS asiento)
        {
            string consulta = "INSERT INTO ASIENTO_460AS (NumAsiento_460AS, CodVuelo_460AS, Disponible_460AS, Tipo_460AS, CodReserva_460AS) " +
                              "VALUES (@NumAsiento_460AS, @CodVuelo_460AS, @Disponible_460AS, @Tipo_460AS, @CodReserva_460AS)";

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@NumAsiento_460AS", asiento.NumAsiento_460AS);
                    comando.Parameters.AddWithValue("@CodVuelo_460AS", asiento.CodVuelo_460AS);
                    comando.Parameters.AddWithValue("@Disponible_460AS", asiento.Disponible_460AS);
                    comando.Parameters.AddWithValue("@Tipo_460AS", asiento.Tipo_460AS.ToString());
                    if (asiento.Reserva_460AS == null) comando.Parameters.AddWithValue("@CodReserva_460AS", DBNull.Value);
                    else comando.Parameters.AddWithValue("@CodReserva_460AS", asiento.Reserva_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarAsiento_460AS(Asiento_460AS asiento)
        {
            string consulta = "UPDATE ASIENTO_460AS SET Disponible_460AS = @Disponible_460AS, Tipo_460AS = @Tipo_460AS, CodReserva_460AS = @CodReserva_460AS " +
                              "WHERE NumAsiento_460AS = @NumAsiento_460AS AND CodVuelo_460AS = @CodVuelo_460AS";

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Disponible_460AS", asiento.Disponible_460AS);
                    comando.Parameters.AddWithValue("@Tipo_460AS", asiento.Tipo_460AS.ToString());
                    comando.Parameters.AddWithValue("@NumAsiento_460AS", asiento.NumAsiento_460AS);
                    comando.Parameters.AddWithValue("@CodVuelo_460AS", asiento.CodVuelo_460AS);
                    if (asiento.Reserva_460AS == null) comando.Parameters.AddWithValue("@CodReserva_460AS", DBNull.Value);
                    else comando.Parameters.AddWithValue("@CodReserva_460AS", asiento.Reserva_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void EliminarAsiento_460AS(string numAsiento, string codVuelo)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                string consulta = "DELETE FROM ASIENTO_460AS WHERE NumAsiento_460AS = @NumAsiento_460AS AND CodVuelo_460AS = @CodVuelo_460AS";
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@NumAsiento_460AS", numAsiento);
                cmd.Parameters.AddWithValue("@CodVuelo_460AS", codVuelo);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
