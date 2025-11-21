using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_DV
    {
        private string cx;
        public DAL460AS_DV()
        {
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True";
        }

        public bool ExisteRegistro_460AS(string nombreTabla)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                string query = "SELECT COUNT(*) FROM DV_460AS WHERE Tabla_460AS = @Tabla";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Tabla", nombreTabla);
                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    return cantidad > 0;
                }
            }
        }

        public void InsertarDV_460AS(string nombreTabla, long dvv)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                string insert = @"INSERT INTO DV_460AS (Tabla_460AS, DVV_460AS, FechaUltimaActualizacion_460AS)
                                  VALUES (@Tabla, @DVV, GETDATE())";
                using (SqlCommand cmd = new SqlCommand(insert, conexion))
                {
                    cmd.Parameters.AddWithValue("@Tabla", nombreTabla);
                    cmd.Parameters.AddWithValue("@DVV", dvv);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarDV_460AS(string nombreTabla, long dvv)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                string update = @"UPDATE DV_460AS 
                                  SET DVV_460AS = @DVV, FechaUltimaActualizacion_460AS = GETDATE()
                                  WHERE Tabla_460AS = @Tabla";
                using (SqlCommand cmd = new SqlCommand(update, conexion))
                {
                    cmd.Parameters.AddWithValue("@Tabla", nombreTabla);
                    cmd.Parameters.AddWithValue("@DVV", dvv);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GuardarDV_460AS(string nombreTabla, long dvv)
        {
            if (ExisteRegistro_460AS(nombreTabla))
                ActualizarDV_460AS(nombreTabla, dvv);
            else
                InsertarDV_460AS(nombreTabla, dvv);
        }

        public long ObtenerDVV_460AS(string nombreTabla)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                string query = "SELECT DVV_460AS FROM DV_460AS WHERE Tabla_460AS = @Tabla";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Tabla", nombreTabla);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt64(result) : 0;
                }
            }
        }
    }
}
