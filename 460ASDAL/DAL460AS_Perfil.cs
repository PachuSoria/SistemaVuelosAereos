using _460ASServicios.Composite;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Perfil
    {
        private string cx_460AS;

        public DAL460AS_Perfil()
        {
            cx_460AS = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True";
        }

        public void GuardarPerfil_460AS(Perfil_460AS perfil_460AS)
        {
            string consulta_460AS = @"INSERT INTO PERFIL_460AS (CodPerfil_460AS, Nombre_460AS) 
                                 VALUES (@CodPerfil_460AS, @Nombre_460AS)";
            using SqlConnection con_460AS = new SqlConnection(cx_460AS);
            using SqlCommand cmd_460AS = new SqlCommand(consulta_460AS, con_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodPerfil_460AS", perfil_460AS.Codigo_460AS);
            cmd_460AS.Parameters.AddWithValue("@Nombre_460AS", perfil_460AS.Nombre_460AS);
            con_460AS.Open();
            cmd_460AS.ExecuteNonQuery();
        }

        public List<Perfil_460AS> ObtenerTodos_460AS()
        {
            List<Perfil_460AS> lista_460AS = new List<Perfil_460AS>();

            using SqlConnection con_460AS = new SqlConnection(cx_460AS);
            SqlCommand cmd_460AS = new SqlCommand("SELECT CodPerfil_460AS, Nombre_460AS FROM PERFIL_460AS", con_460AS);
            con_460AS.Open();

            using SqlDataReader reader_460AS = cmd_460AS.ExecuteReader();

            while (reader_460AS.Read())
            {
                Perfil_460AS perfil_460AS = new Perfil_460AS(
                    reader_460AS.GetString(0),
                    reader_460AS.GetString(1)
                );
                lista_460AS.Add(perfil_460AS);
            }
            return lista_460AS;
        }

        public void EliminarPerfil_460AS(Perfil_460AS perfil_460AS)
        {
            string consulta_460AS = @"DELETE FROM PERFIL_460AS WHERE CodPerfil_460AS = @CodPerfil_460AS";
            using SqlConnection con_460AS = new SqlConnection(cx_460AS);
            using SqlCommand cmd_460AS = new SqlCommand(consulta_460AS, con_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodPerfil_460AS", perfil_460AS.Codigo_460AS);
            con_460AS.Open();
            cmd_460AS.ExecuteNonQuery();
        }

        //FamiliaPerfil

        public void AgregarFamiliaAPerfil_460AS(string codPerfil_460AS, string codFamilia_460AS)
        {
            string consulta_460AS = @"INSERT INTO PERFIL_FAMILIA_460AS (CodPerfil_460AS, CodFamilia_460AS)
                                 VALUES (@CodPerfil_460AS, @CodFamilia_460AS)";
            using SqlConnection con_460AS = new SqlConnection(cx_460AS);
            using SqlCommand cmd_460AS = new SqlCommand(consulta_460AS, con_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodPerfil_460AS", codPerfil_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodFamilia_460AS", codFamilia_460AS);
            con_460AS.Open();
            cmd_460AS.ExecuteNonQuery();
        }

        public List<Familia_460AS> ObtenerFamiliasDePerfil_460AS(string codPerfil_460AS)
        {
            List<Familia_460AS> lista_460AS = new List<Familia_460AS>();

            using SqlConnection con_460AS = new SqlConnection(cx_460AS);
            string consulta_460AS = @"SELECT f.CodFamilia_460AS, f.Nombre_460AS FROM FAMILIA_460AS f
                                 INNER JOIN PERFIL_FAMILIA_460AS pf ON f.CodFamilia_460AS = pf.CodFamilia_460AS
                                 WHERE pf.CodPerfil_460AS = @CodPerfil_460AS";
            using SqlCommand cmd_460AS = new SqlCommand(consulta_460AS, con_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodPerfil_460AS", codPerfil_460AS);
            con_460AS.Open();
            using SqlDataReader reader_460AS = cmd_460AS.ExecuteReader();
            while (reader_460AS.Read())
            {
                Familia_460AS familia_460AS = new Familia_460AS(
                    reader_460AS.GetString(0),
                    reader_460AS.GetString(1)
                );
                lista_460AS.Add(familia_460AS);
            }
            return lista_460AS;
        }

        public void EliminarFamiliasDePerfil_460AS(string codPerfil_460AS)
        {
            string consulta_460AS = @"DELETE FROM PERFIL_FAMILIA_460AS WHERE CodPerfil_460AS = @CodPerfil_460AS";
            using SqlConnection con_460AS = new SqlConnection(cx_460AS);
            using SqlCommand cmd_460AS = new SqlCommand(consulta_460AS, con_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodPerfil_460AS", codPerfil_460AS);
            con_460AS.Open();
            cmd_460AS.ExecuteNonQuery();
        }

        public void EliminarFamiliaDePerfil_460AS(string codPerfil_460AS, string codFamilia_460AS)
        {
            string consulta_460AS = @"DELETE FROM PERFIL_FAMILIA_460AS 
                                 WHERE CodPerfil_460AS = @CodPerfil_460AS AND CodFamilia_460AS = @CodFamilia_460AS";
            using SqlConnection con_460AS = new SqlConnection(cx_460AS);
            using SqlCommand cmd_460AS = new SqlCommand(consulta_460AS, con_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodPerfil_460AS", codPerfil_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodFamilia_460AS", codFamilia_460AS);
            con_460AS.Open();
            cmd_460AS.ExecuteNonQuery();
        }

        //PermisoPerfil

        public void AgregarPermisoAPerfil_460AS(string codPerfil_460AS, string codPermiso_460AS)
        {
            string consulta_460AS = @"INSERT INTO PERFIL_PERMISO_460AS (CodPerfil_460AS, CodPermiso_460AS)
                                 VALUES (@CodPerfil_460AS, @CodPermiso_460AS)";
            using SqlConnection con_460AS = new SqlConnection(cx_460AS);
            using SqlCommand cmd_460AS = new SqlCommand(consulta_460AS, con_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodPerfil_460AS", codPerfil_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodPermiso_460AS", codPermiso_460AS);
            con_460AS.Open();
            cmd_460AS.ExecuteNonQuery();
        }

        public List<Permiso_460AS> ObtenerPermisosDePerfil_460AS(string codPerfil_460AS)
        {
            List<Permiso_460AS> lista_460AS = new List<Permiso_460AS>();

            using SqlConnection con_460AS = new SqlConnection(cx_460AS);
            string consulta_460AS = @"SELECT p.CodPermiso_460AS, p.Nombre_460AS, p.Descripcion_460AS FROM PERMISO_460AS p
                                 INNER JOIN PERFIL_PERMISO_460AS pp ON p.CodPermiso_460AS = pp.CodPermiso_460AS
                                 WHERE pp.CodPerfil_460AS = @CodPerfil_460AS";
            using SqlCommand cmd_460AS = new SqlCommand(consulta_460AS, con_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodPerfil_460AS", codPerfil_460AS);
            con_460AS.Open();
            using SqlDataReader reader_460AS = cmd_460AS.ExecuteReader();
            while (reader_460AS.Read())
            {
                Permiso_460AS permiso_460AS = new Permiso_460AS(
                    reader_460AS.GetString(0),
                    reader_460AS.GetString(1)
                )
                {
                    Descripcion_460AS = reader_460AS.IsDBNull(2) ? null : reader_460AS.GetString(2)
                };
                lista_460AS.Add(permiso_460AS);
            }
            return lista_460AS;
        }

        public void EliminarPermisosDePerfil_460AS(string codPerfil_460AS)
        {
            string consulta_460AS = @"DELETE FROM PERFIL_PERMISO_460AS WHERE CodPerfil_460AS = @CodPerfil_460AS";
            using SqlConnection con_460AS = new SqlConnection(cx_460AS);
            using SqlCommand cmd_460AS = new SqlCommand(consulta_460AS, con_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodPerfil_460AS", codPerfil_460AS);
            con_460AS.Open();
            cmd_460AS.ExecuteNonQuery();
        }

        public void EliminarPermisoDePerfil_460AS(string codPerfil_460AS, string codPermiso_460AS)
        {
            string consulta_460AS = @"DELETE FROM PERFIL_PERMISO_460AS 
                                 WHERE CodPerfil_460AS = @CodPerfil_460AS AND CodPermiso_460AS = @CodPermiso_460AS";
            using SqlConnection con_460AS = new SqlConnection(cx_460AS);
            using SqlCommand cmd_460AS = new SqlCommand(consulta_460AS, con_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodPerfil_460AS", codPerfil_460AS);
            cmd_460AS.Parameters.AddWithValue("@CodPermiso_460AS", codPermiso_460AS);
            con_460AS.Open();
            cmd_460AS.ExecuteNonQuery();
        }

        public Perfil_460AS ObtenerPorCodigo_460AS(string codigo)
        {
            using (SqlConnection con = new SqlConnection(cx_460AS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM PERFIL_460AS WHERE CodPerfil_460AS = @CodPerfil_460AS", con);
                cmd.Parameters.AddWithValue("@CodPerfil_460AS", codigo);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Perfil_460AS
                        {
                            Codigo_460AS = reader["CodPerfil_460AS"].ToString(),
                            Nombre_460AS = reader["Nombre_460AS"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}
