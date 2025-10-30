using _460ASBE;
using _460ASServicios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Cliente
    {
        private string cx;
        public DAL460AS_Cliente()
        {
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True";
        }

        public void GuardarCliente_460AS(Cliente_460AS cliente)
        {
            string consulta = "INSERT INTO CLIENTE_460AS " +
                              "(DNI_460AS, Nombre_460AS, Apellido_460AS, FechaNacimiento_460AS, Telefono_460AS, NroPasaporte_460AS, Eliminado_460AS) " +
                              "VALUES (@DNI_460AS, @Nombre_460AS, @Apellido_460AS, @FechaNacimiento_460AS, @Telefono_460AS, @NroPasaporte_460AS, 0)";

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@DNI_460AS", cliente.DNI_460AS);
                    comando.Parameters.AddWithValue("@Nombre_460AS", cliente.Nombre_460AS);
                    comando.Parameters.AddWithValue("@Apellido_460AS", cliente.Apellido_460AS);
                    comando.Parameters.AddWithValue("@FechaNacimiento_460AS", cliente.FechaNacimiento_460AS);
                    comando.Parameters.AddWithValue("@Telefono_460AS", cliente.Telefono_460AS);
                    comando.Parameters.AddWithValue("@NroPasaporte_460AS", cliente.NroPasaporte_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public IList<Cliente_460AS> ObtenerClientes_460AS()
        {
            List<Cliente_460AS> listaClientes = new List<Cliente_460AS>();

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM CLIENTE_460AS WHERE Eliminado_460AS = 0", conexion);
                conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Cliente_460AS cliente = new Cliente_460AS(reader["DNI_460AS"].ToString(), reader["Nombre_460AS"].ToString(), reader["Apellido_460AS"].ToString(),
                        Convert.ToDateTime(reader["FechaNacimiento_460AS"]), Convert.ToInt32(reader["Telefono_460AS"]), reader["NroPasaporte_460AS"].ToString(), Convert.ToBoolean(reader["Eliminado_460AS"]));

                    listaClientes.Add(cliente);
                }
            }
            return listaClientes;
        }

        public void ActualizarCliente_460AS(Cliente_460AS cliente)
        {
            string consulta = "UPDATE CLIENTE_460AS SET " +
                              "Nombre_460AS = @Nombre_460AS, " +
                              "Apellido_460AS = @Apellido_460AS, " +
                              "FechaNacimiento_460AS = @FechaNacimiento_460AS, " +
                              "Telefono_460AS = @Telefono_460AS, " +
                              "NroPasaporte_460AS = @NroPasaporte_460AS, " +
                              "Eliminado_460AS = @Eliminado_460AS " +
                              "WHERE DNI_460AS = @DNI_460AS";

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre_460AS", cliente.Nombre_460AS);
                    comando.Parameters.AddWithValue("@Apellido_460AS", cliente.Apellido_460AS);
                    comando.Parameters.AddWithValue("@FechaNacimiento_460AS", cliente.FechaNacimiento_460AS);
                    comando.Parameters.AddWithValue("@Telefono_460AS", cliente.Telefono_460AS);
                    comando.Parameters.AddWithValue("@NroPasaporte_460AS", cliente.NroPasaporte_460AS);
                    comando.Parameters.AddWithValue("@Eliminado_460AS", cliente.Eliminado_460AS);
                    comando.Parameters.AddWithValue("@DNI_460AS", cliente.DNI_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void EliminarCliente_460AS(string dni)
        {
            string consulta = "UPDATE CLIENTE_460AS SET Eliminado_460AS = 1 WHERE DNI_460AS = @DNI_460AS";

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@DNI_460AS", dni);
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
