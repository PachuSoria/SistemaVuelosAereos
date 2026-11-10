using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_BackUpRestore : DAL_Abstract
    {
        private string ObtenerNombreBaseDatos()
        {
            var builder = new SqlConnectionStringBuilder(cx);
            return string.IsNullOrWhiteSpace(builder.InitialCatalog) ? "AviagoBD" : builder.InitialCatalog;
        }

        public void RealizarBackup_460S(string backUpPath)
        {
            string nombreBD = ObtenerNombreBaseDatos();
            string archivo = $"BCK_{DateTime.Now:ddMMyy_HHmm}.bak";
            string ruta = System.IO.Path.Combine(backUpPath, archivo);
            string comando = $"BACKUP DATABASE [{nombreBD}] TO DISK = @ruta";
            using (SqlConnection con = new SqlConnection(cx))
            {
                SqlCommand cmd = new SqlCommand(comando, con);
                cmd.Parameters.AddWithValue("@ruta", ruta);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void RealizarRestore_460AS(string backUpFilePath)
        {
            string nombreBD = ObtenerNombreBaseDatos();
            using (SqlConnection con = new SqlConnection(cx))
            {
                con.Open();

                using (SqlCommand setMaster = new SqlCommand("USE master;", con))
                {
                    setMaster.ExecuteNonQuery();
                }

                try
                {
                    using (SqlCommand setSingleUser = new SqlCommand($"ALTER DATABASE [{nombreBD}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", con))
                    {
                        setSingleUser.ExecuteNonQuery();
                    }

                    string consulta = $"RESTORE DATABASE [{nombreBD}] FROM DISK = @ruta WITH REPLACE;";
                    using (SqlCommand cmd = new SqlCommand(consulta, con))
                    {
                        cmd.Parameters.AddWithValue("@ruta", backUpFilePath);
                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    using (SqlCommand setMultiUser = new SqlCommand($"ALTER DATABASE [{nombreBD}] SET MULTI_USER;", con))
                    {
                        setMultiUser.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
