using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using _460ASServicios;
using Microsoft.VisualBasic.ApplicationServices;


namespace _460ASDAL
{
    public class DAL460AS_Usuario
    {
        private string cx;
        public DAL460AS_Usuario()
        {
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True";
        }

        public void GuardarUsuario_460AS(Usuario_460AS usuario)
        {
            string consulta = "INSERT INTO USUARIO_460AS (DNI_460AS, Nombre_460AS, Apellido_460AS, Login_460AS, Password_460AS, Rol_460AS, Telefono_460AS, Bloqueado_460AS, Activo_460AS, Contador_460AS, UltimoIntento_460AS, Idioma_460AS)" +
                              "VALUES (@DNI_460AS, @Nombre_460AS, @Apellido_460AS, @Login_460AS, @Password_460AS, @Rol_460AS, @Telefono_460AS, @Bloqueado_460AS, @Activo_460AS, @Contador_460AS, @UltimoIntento_460AS, @Idioma_460AS)";
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@DNI_460AS", usuario.DNI_460AS);
                    comando.Parameters.AddWithValue("@Nombre_460AS", usuario.Nombre_460AS);
                    comando.Parameters.AddWithValue("@Apellido_460AS", usuario.Apellido_460AS);
                    comando.Parameters.AddWithValue("@Login_460AS", usuario.Login_460AS);
                    comando.Parameters.AddWithValue("@Password_460AS", usuario.Password_460AS);
                    comando.Parameters.AddWithValue("@Rol_460AS", usuario.Rol_460AS);
                    comando.Parameters.AddWithValue("@Telefono_460AS", usuario.Telefono_460AS);
                    comando.Parameters.AddWithValue("@Bloqueado_460AS", usuario.Bloqueado_460AS);
                    comando.Parameters.AddWithValue("@Activo_460AS", usuario.Activo_460AS);
                    comando.Parameters.AddWithValue("@Contador_460AS", usuario.Contador_460AS);
                    comando.Parameters.AddWithValue("@UltimoIntento_460AS", usuario.UltimoIntento_460AS);
                    comando.Parameters.AddWithValue("@Idioma_460AS", usuario.Idioma_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarUsuario_460AS(Usuario_460AS usuario)
        {
            string consulta = "UPDATE USUARIO_460AS SET Nombre_460AS = @Nombre_460AS, Apellido_460AS = @Apellido_460AS, Login_460AS = @Login_460AS, " +
                     "Password_460AS = @Password_460AS, Rol_460AS = @Rol_460AS, Telefono_460AS = @Telefono_460AS, Bloqueado_460AS = @Bloqueado_460AS, Activo_460AS = @Activo_460AS, Contador_460AS = @Contador_460AS, UltimoIntento_460AS = @UltimoIntento_460AS, Idioma_460AS = @Idioma_460AS " +
                     "WHERE DNI_460AS = @DNI_460AS";
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@DNI_460AS", usuario.DNI_460AS);
                    comando.Parameters.AddWithValue("@Nombre_460AS", usuario.Nombre_460AS);
                    comando.Parameters.AddWithValue("@Apellido_460AS", usuario.Apellido_460AS);
                    comando.Parameters.AddWithValue("@Login_460AS", usuario.Login_460AS);
                    comando.Parameters.AddWithValue("@Password_460AS", usuario.Password_460AS);
                    comando.Parameters.AddWithValue("@Rol_460AS", usuario.Rol_460AS);
                    comando.Parameters.AddWithValue("@Telefono_460AS", usuario.Telefono_460AS);
                    comando.Parameters.AddWithValue("@Bloqueado_460AS", usuario.Bloqueado_460AS);
                    comando.Parameters.AddWithValue("@Activo_460AS", usuario.Activo_460AS);
                    comando.Parameters.AddWithValue("@Contador_460AS", usuario.Contador_460AS);
                    comando.Parameters.AddWithValue("@UltimoIntento_460AS", usuario.UltimoIntento_460AS);
                    comando.Parameters.AddWithValue("@Idioma_460AS", usuario.Idioma_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ActivarUsuario_460AS(Usuario_460AS usuario)
        {
            string consulta = "UPDATE USUARIO_460AS SET Activo_460AS = 1 WHERE DNI_460AS = @DNI_460AS";
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@DNI_460AS", usuario.DNI_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void DesactivarUsuario_460AS(Usuario_460AS usuario)
        {
            string consulta = "UPDATE USUARIO_460AS SET Activo_460AS = 0 WHERE DNI_460AS = @DNI_460AS";
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@DNI_460AS", usuario.DNI_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void DesbloquearUsuario_460AS(Usuario_460AS usuario)
        {
            string consulta = "UPDATE USUARIO_460AS SET Bloqueado_460AS = 0 WHERE DNI_460AS = @DNI_460AS";
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@DNI_460AS", usuario.DNI_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public IList<Usuario_460AS> ObtenerUsuarios_460AS()
        {
            List<Usuario_460AS> listaUsuarios = new List<Usuario_460AS>();

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM USUARIO_460AS", conexion);
                conexion.Open();

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Usuario_460AS usuario = new Usuario_460AS(reader["DNI_460AS"].ToString(), reader["Nombre_460AS"].ToString(), reader["Apellido_460AS"].ToString(), reader["Login_460AS"].ToString(),
                        reader["Password_460AS"].ToString(), reader["Rol_460AS"].ToString(), Convert.ToInt32(reader["Telefono_460AS"]), Convert.ToBoolean(reader["Bloqueado_460AS"]), Convert.ToBoolean(reader["Activo_460AS"].ToString()), 
                        Convert.ToInt32(reader["Contador_460AS"]), Convert.ToDateTime(reader["UltimoIntento_460AS"]), reader["Idioma_460AS"].ToString());

                    listaUsuarios.Add(usuario);
                }
            }
            return listaUsuarios;
        }

        public void ActualizarPasswordUsuario_460AS(string loginUsuario, string nuevaPasswordHasheada)
        {
            string consulta = "UPDATE USUARIO_460AS SET Password_460AS = @Password_460AS WHERE Login_460AS = @Login_460AS";
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Login_460AS", loginUsuario);
                    comando.Parameters.AddWithValue("@Password_460AS", nuevaPasswordHasheada);

                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
    

