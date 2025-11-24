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
        public void RealizarBackup_460S(string backUpPath)
        {
            string archivo = $"BCK_{DateTime.Now:ddMMyy_HHmm}.bak";
            string ruta = System.IO.Path.Combine(backUpPath, archivo);
            string comando = $"BACKUP DATABASE [Vuelos Aereos] TO DISK= '{ruta}'";
            using (SqlConnection con = new SqlConnection(cx))
            {
                SqlCommand cmd = new SqlCommand(comando, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void RealizarRestore_460AS(string backUpFilePath)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cx))
                {
                    con.Open();

                    using (SqlCommand setMaster = new SqlCommand("USE master;", con))
                    {
                        setMaster.ExecuteNonQuery();
                    }

                    using (SqlCommand setSingleUser = new SqlCommand("ALTER DATABASE [Vuelos Aereos] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", con))
                    {
                        setSingleUser.ExecuteNonQuery();
                    }

                    string consulta = $"RESTORE DATABASE [Vuelos Aereos] FROM DISK = '{backUpFilePath}' WITH REPLACE;";
                    using (SqlCommand cmd = new SqlCommand(consulta, con))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand setMultiUser = new SqlCommand("ALTER DATABASE [Vuelos Aereos] SET MULTI_USER;", con))
                    {
                        setMultiUser.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
