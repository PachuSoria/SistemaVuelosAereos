using _460ASBE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Cliente_C : DAL_Abstract
    {
        public DAL460AS_Cliente_C()
        {
            
        }

        public List<Cliente_C_460AS> ObtenerTodos_460AS()
        {
            List<Cliente_C_460AS> lista = new List<Cliente_C_460AS>();

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = "SELECT * FROM CLIENTE_C_460AS ORDER BY FechaCambio_460AS DESC";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente_C_460AS
                            {
                                DNI_460AS = dr["DNI_460AS"].ToString(),
                                Nombre_460AS = dr["Nombre_460AS"].ToString(),
                                Apellido_460AS = dr["Apellido_460AS"].ToString(),
                                FechaNacimiento_460AS = Convert.ToDateTime(dr["FechaNacimiento_460AS"]),
                                Telefono_460AS = Convert.ToInt32(dr["Telefono_460AS"]),
                                NroPasaporte_460AS = dr["NroPasaporte_460AS"].ToString(),
                                FechaCambio_460AS = Convert.ToDateTime(dr["FechaCambio_460AS"]),
                                Activo_460AS = Convert.ToBoolean(dr["Activo_460AS"])
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public List<Cliente_C_460AS> FiltrarClientesC_460AS(
            string dni = null,
            string nombre = null,
            string apellido = null,
            DateTime? fechaNacimiento = null,
            int? telefono = null,
            string nroPasaporte = null,
            DateTime? fechaCambio = null,
            bool? activo = null)
        {
            List<Cliente_C_460AS> lista = new List<Cliente_C_460AS>();

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = "SELECT * FROM CLIENTE_C_460AS WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(dni))
                    consulta += " AND DNI_460AS LIKE @DNI_460AS";
                if (!string.IsNullOrWhiteSpace(nombre))
                    consulta += " AND Nombre_460AS LIKE @Nombre_460AS";
                if (!string.IsNullOrWhiteSpace(apellido))
                    consulta += " AND Apellido_460AS LIKE @Apellido_460AS";
                if (fechaNacimiento.HasValue)
                    consulta += " AND CONVERT(date, FechaNacimiento_460AS) = @FechaNacimiento_460AS";
                if (telefono.HasValue)
                    consulta += " AND Telefono_460AS = @Telefono_460AS";
                if (!string.IsNullOrWhiteSpace(nroPasaporte))
                    consulta += " AND NroPasaporte_460AS LIKE @NroPasaporte_460AS";
                if (fechaCambio.HasValue)
                    consulta += " AND CONVERT(date, FechaCambio_460AS) = @FechaCambio_460AS";
                if (activo.HasValue)
                    consulta += " AND Activo_460AS = @Activo_460AS";

                consulta += " ORDER BY FechaCambio_460AS DESC";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    if (!string.IsNullOrWhiteSpace(dni))
                        cmd.Parameters.AddWithValue("@DNI_460AS", $"%{dni}%");
                    if (!string.IsNullOrWhiteSpace(nombre))
                        cmd.Parameters.AddWithValue("@Nombre_460AS", $"%{nombre}%");
                    if (!string.IsNullOrWhiteSpace(apellido))
                        cmd.Parameters.AddWithValue("@Apellido_460AS", $"%{apellido}%");
                    if (fechaNacimiento.HasValue)
                        cmd.Parameters.AddWithValue("@FechaNacimiento_460AS", fechaNacimiento.Value);
                    if (telefono.HasValue)
                        cmd.Parameters.AddWithValue("@Telefono_460AS", telefono.Value);
                    if (!string.IsNullOrWhiteSpace(nroPasaporte))
                        cmd.Parameters.AddWithValue("@NroPasaporte_460AS", $"%{nroPasaporte}%");
                    if (fechaCambio.HasValue)
                        cmd.Parameters.AddWithValue("@FechaCambio_460AS", fechaCambio.Value);
                    if (activo.HasValue)
                        cmd.Parameters.AddWithValue("@Activo_460AS", activo.Value);

                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente_C_460AS
                            {
                                DNI_460AS = dr["DNI_460AS"].ToString(),
                                Nombre_460AS = dr["Nombre_460AS"].ToString(),
                                Apellido_460AS = dr["Apellido_460AS"].ToString(),
                                FechaNacimiento_460AS = Convert.ToDateTime(dr["FechaNacimiento_460AS"]),
                                Telefono_460AS = Convert.ToInt32(dr["Telefono_460AS"]),
                                NroPasaporte_460AS = dr["NroPasaporte_460AS"].ToString(),
                                FechaCambio_460AS = Convert.ToDateTime(dr["FechaCambio_460AS"]),
                                Activo_460AS = Convert.ToBoolean(dr["Activo_460AS"])
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public void ActivarCliente_460AS(string dni, DateTime fechaCambio)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                SqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    string disableTrigger = "DISABLE TRIGGER TR_Cliente_Update_460AS ON CLIENTE_460AS;";
                    using (SqlCommand cmd = new SqlCommand(disableTrigger, conexion, transaccion))
                        cmd.ExecuteNonQuery();

                    string desactivar = "UPDATE CLIENTE_C_460AS SET Activo_460AS = 0 WHERE DNI_460AS = @DNI;";
                    using (SqlCommand cmd = new SqlCommand(desactivar, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@DNI", dni);
                        cmd.ExecuteNonQuery();
                    }

                    string activar = @"UPDATE CLIENTE_C_460AS 
                               SET Activo_460AS = 1 
                               WHERE DNI_460AS = @DNI AND FechaCambio_460AS = @FechaCambio;";
                    using (SqlCommand cmd = new SqlCommand(activar, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@DNI", dni);
                        cmd.Parameters.AddWithValue("@FechaCambio", fechaCambio);
                        cmd.ExecuteNonQuery();
                    }

                    string actualizarCliente = @"
                UPDATE cli
                SET cli.Nombre_460AS = c.Nombre_460AS,
                    cli.Apellido_460AS = c.Apellido_460AS,
                    cli.FechaNacimiento_460AS = c.FechaNacimiento_460AS,
                    cli.Telefono_460AS = c.Telefono_460AS,
                    cli.NroPasaporte_460AS = c.NroPasaporte_460AS
                FROM CLIENTE_460AS cli
                INNER JOIN CLIENTE_C_460AS c 
                    ON cli.DNI_460AS = c.DNI_460AS
                WHERE c.DNI_460AS = @DNI AND c.FechaCambio_460AS = @FechaCambio;";

                    using (SqlCommand cmd = new SqlCommand(actualizarCliente, conexion, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@DNI", dni);
                        cmd.Parameters.AddWithValue("@FechaCambio", fechaCambio);
                        cmd.ExecuteNonQuery();
                    }

                    string enableTrigger = "ENABLE TRIGGER TR_Cliente_Update_460AS ON CLIENTE_460AS;";
                    using (SqlCommand cmd = new SqlCommand(enableTrigger, conexion, transaccion))
                        cmd.ExecuteNonQuery();

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }
    }
}
