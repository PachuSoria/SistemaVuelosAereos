using _460ASBE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Vuelo
    {
        private string cx;
        public DAL460AS_Vuelo()
        {
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True";
        }

        public IList<Vuelo_460AS> ObtenerVuelos_460AS()
        {
            List<Vuelo_460AS> vuelos = new List<Vuelo_460AS>();

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string sql = "SELECT * FROM VUELO_460AS";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Vuelo_460AS vuelo = new Vuelo_460AS
                    (
                        reader["CodVuelo_460AS"].ToString(),
                        reader["Aerolinea_460AS"].ToString(),
                        reader["Origen_460AS"].ToString(),
                        reader["Destino_460AS"].ToString(),
                        Convert.ToDateTime(reader["FechaSalida_460AS"]),
                        Convert.ToDateTime(reader["FechaLlegada_460AS"]),
                        Convert.ToDecimal(reader["PrecioVuelo_460AS"])
                    );
                    vuelos.Add(vuelo);
                }
            }
            return vuelos;
        }

        public void AgregarVuelo_460AS(Vuelo_460AS vuelo)
        {
            string consulta = "INSERT INTO VUELO_460AS (CodVuelo_460AS, Aerolinea_460AS, Origen_460AS, Destino_460AS, FechaSalida_460AS, FechaLlegada_460AS, PrecioVuelo_460AS)" +
                    "VALUES (@CodVuelo_460AS, @Aerolinea_460AS, @Origen_460AS, @Destino_460AS, @FechaSalida_460AS, @FechaLlegada_460AS, @PrecioVuelo_460AS)";

            using (SqlConnection conexion= new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@CodVuelo_460AS", vuelo.CodVuelo_460AS);
                    comando.Parameters.AddWithValue("@Aerolinea_460AS", vuelo.Aerolinea_460AS);
                    comando.Parameters.AddWithValue("@Origen_460AS", vuelo.Origen_460AS);
                    comando.Parameters.AddWithValue("@Destino_460AS", vuelo.Destino_460AS);
                    comando.Parameters.AddWithValue("@FechaSalida_460AS", vuelo.FechaSalida_460AS);
                    comando.Parameters.AddWithValue("@FechaLlegada_460AS", vuelo.FechaLlegada_460AS);
                    comando.Parameters.AddWithValue("@PrecioVuelo_460AS", vuelo.PrecioVuelo_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarVuelo_460AS(Vuelo_460AS vuelo)
        {
            string consulta = "UPDATE VUELO_460AS SET Aerolinea_460AS = @Aerolinea_460AS, Origen_460AS = @Origen_460AS, Destino_460AS = @Destino_460AS," +
                "FechaSalida_460AS = @FechaSalida_460AS, FechaLlegada_460AS = @FechaLlegada_460AS, PrecioVuelo_460AS = @PrecioVuelo_460AS " +
                "WHERE CodVuelo_460AS = @CodVuelo_460AS";
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@CodVuelo_460AS", vuelo.CodVuelo_460AS);
                    comando.Parameters.AddWithValue("@Aerolinea_460AS", vuelo.Aerolinea_460AS);
                    comando.Parameters.AddWithValue("@Origen_460AS", vuelo.Origen_460AS);
                    comando.Parameters.AddWithValue("@Destino_460AS", vuelo.Destino_460AS);
                    comando.Parameters.AddWithValue("@FechaSalida_460AS", vuelo.FechaSalida_460AS);
                    comando.Parameters.AddWithValue("@FechaLlegada_460AS", vuelo.FechaLlegada_460AS);
                    comando.Parameters.AddWithValue("@PrecioVuelo_460AS", vuelo.PrecioVuelo_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void EliminarVuelo_460AS(string codVuelo)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                string consulta = "DELETE FROM VUELO_460AS WHERE CodVuelo_460AS = @CodVuelo_460AS";
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@CodVuelo_460AS", codVuelo);
                cmd.ExecuteNonQuery();
            }
        }

        public Vuelo_460AS ObtenerVueloPorCodigo_460AS(string codVuelo)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                string sql = "SELECT * FROM VUELO_460AS WHERE CodVuelo_460AS = @CodVuelo_460AS";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@CodVuelo_460AS", codVuelo);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Vuelo_460AS
                        {
                            CodVuelo_460AS = reader["CodVuelo_460AS"].ToString(),
                            Aerolinea_460AS = reader["Aerolinea_460AS"].ToString(),
                            Origen_460AS = reader["Origen_460AS"].ToString(),
                            Destino_460AS = reader["Destino_460AS"].ToString(),
                            FechaSalida_460AS = Convert.ToDateTime(reader["FechaSalida_460AS"]),
                            FechaLlegada_460AS = Convert.ToDateTime(reader["FechaLlegada_460AS"]),
                            PrecioVuelo_460AS = Convert.ToDecimal(reader["PrecioVuelo_460AS"])
                        };
                    }
                }
            }
            return null;
        }
    }
}
