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

        public IList<Evento_460AS> ObtenerPorFechas_460AS(DateTime desde, DateTime hasta)
        {
            List<Evento_460AS> lista = new List<Evento_460AS>();

            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = @"SELECT * 
                            FROM EVENTOS_460AS
                            WHERE Fecha_460AS BETWEEN @desde AND @hasta
                            ORDER BY Fecha_460AS DESC";
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@desde", desde);
                cmd.Parameters.AddWithValue("@hasta", hasta);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Evento_460AS
                        {
                            IdEvento_460AS = reader["IdEvento_460AS"].ToString(),
                            Usuario_460AS = reader["Usuario_460AS"].ToString(),
                            Fecha_460AS = Convert.ToDateTime(reader["Fecha_460AS"]),
                            Modulo_460AS = reader["Modulo_460AS"].ToString(),
                            Actividad_460AS = reader["Actividad_460AS"].ToString(),
                            Criticidad_460AS = Convert.ToInt32(reader["Criticidad_460AS"])
                        });
                    }
                }
            }
            return lista;
        }

        public IList<Evento_460AS> FiltrarEventos_460AS(DateTime desde, DateTime hasta, string actividadPrefijo = null, string usuario = null, string modulo = null, int? criticidad = null)
        {
            List<Evento_460AS> lista = new List<Evento_460AS>();

            using (SqlConnection con = new SqlConnection(cx))
            {
                var sql = new StringBuilder(@"SELECT * 
                                      FROM EVENTOS_460AS
                                      WHERE Fecha_460AS BETWEEN @desde AND @hasta");
                var cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@desde", desde);
                cmd.Parameters.AddWithValue("@hasta", hasta);

                if (!string.IsNullOrWhiteSpace(actividadPrefijo))
                {
                    sql.Append(" AND Actividad_460AS LIKE @prefijo + '%'");
                    cmd.Parameters.AddWithValue("@prefijo", actividadPrefijo);
                }
                if (!string.IsNullOrWhiteSpace(usuario))
                {
                    sql.Append(" AND Usuario_460AS LIKE @usuario");
                    cmd.Parameters.AddWithValue("@usuario", "%" + usuario + "%");
                }
                if (!string.IsNullOrWhiteSpace(modulo))
                {
                    sql.Append(" AND Modulo_460AS = @modulo");
                    cmd.Parameters.AddWithValue("@modulo", modulo);
                }
                if (criticidad.HasValue)
                {
                    sql.Append(" AND Criticidad_460AS = @crit");
                    cmd.Parameters.AddWithValue("@crit", criticidad.Value);
                }

                sql.Append(" ORDER BY Fecha_460AS DESC");
                cmd.CommandText = sql.ToString();
                cmd.Connection = con;

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Evento_460AS
                        {
                            IdEvento_460AS = reader["IdEvento_460AS"].ToString(),
                            Usuario_460AS = reader["Usuario_460AS"].ToString(),
                            Fecha_460AS = Convert.ToDateTime(reader["Fecha_460AS"]),
                            Modulo_460AS = reader["Modulo_460AS"].ToString(),
                            Actividad_460AS = reader["Actividad_460AS"].ToString(),
                            Criticidad_460AS = Convert.ToInt32(reader["Criticidad_460AS"])
                        });
                    }
                }
            }
            return lista;
        }
    }
}
