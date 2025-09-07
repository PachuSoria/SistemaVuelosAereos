using _460ASServicios;
using _460ASServicios.Composite;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Evento
    {
        private string cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True";

        public void GuardarEvento_460AS(Evento_460AS evento)
        {
            string consulta = "INSERT INTO EVENTOS_460AS (IdEvento_460AS, Usuario_460AS, Fecha_460AS, Modulo_460AS, Actividad_460AS, Criticidad_460AS) " +
                       "VALUES (@IdEvento_460AS, @Usuario_460AS, @Fecha_460AS, @Modulo_460AS, @Actividad_460AS, @Criticidad_460AS)";

            using (SqlConnection con = new SqlConnection(cx))
            using (SqlCommand cmd = new SqlCommand(consulta, con))
            {
                cmd.Parameters.AddWithValue("@IdEvento_460AS", evento.IdEvento_460AS);
                cmd.Parameters.AddWithValue("@Usuario_460AS", evento.Usuario_460AS);
                cmd.Parameters.AddWithValue("@Fecha_460AS", evento.Fecha_460AS);
                cmd.Parameters.AddWithValue("@Modulo_460AS", evento.Modulo_460AS);
                cmd.Parameters.AddWithValue("@Actividad_460AS", evento.Actividad_460AS);
                cmd.Parameters.AddWithValue("@Criticidad_460AS", evento.Criticidad_460AS);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IList<Evento_460AS> ObtenerTodos_460AS()
        {
            List<Evento_460AS> lista = new List<Evento_460AS>();

            using (SqlConnection con = new SqlConnection(cx))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM EVENTOS_460AS", con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Evento_460AS evento = new Evento_460AS
                        {
                            IdEvento_460AS = reader["IdEvento_460AS"].ToString(),
                            Usuario_460AS = reader["Usuario_460AS"].ToString(),
                            Fecha_460AS = Convert.ToDateTime(reader["Fecha_460AS"]),
                            Modulo_460AS = reader["Modulo_460AS"].ToString(),
                            Actividad_460AS = reader["Actividad_460AS"].ToString(),
                            Criticidad_460AS = Convert.ToInt32(reader["Criticidad_460AS"])
                        };
                        lista.Add(evento);
                    }
                }
            }
            return lista;
        }

        public Evento_460AS ObtenerUltimo_460AS()
        {
            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = @"SELECT TOP 1 * FROM EVENTOS_460AS ORDER BY Fecha_460AS DESC";
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Evento_460AS
                        {
                            IdEvento_460AS = reader["IdEvento_460AS"].ToString(),
                            Usuario_460AS = reader["Usuario_460AS"].ToString(),
                            Fecha_460AS = Convert.ToDateTime(reader["Fecha_460AS"]),
                            Modulo_460AS = reader["Modulo_460AS"].ToString(),
                            Actividad_460AS = reader["Actividad_460AS"].ToString(),
                            Criticidad_460AS = Convert.ToInt32(reader["Criticidad_460AS"])
                        };
                    }
                }
            }
            return null;
        }
    }
}
