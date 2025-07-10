using _460ASServicios.Composite;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Familia
    {
        private string cx;

        public DAL460AS_Familia()
        {
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True;MultipleActiveResultSets=True";
        }

        public void GuardarFamilia_460AS(Familia_460AS familia)
        {
            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = @"INSERT INTO FAMILIA_460AS (CodFamilia_460AS, Nombre_460AS)
                                VALUES (@CodFamilia_460AS, @Nombre_460AS)";
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@CodFamilia_460AS", familia.Codigo_460AS);
                cmd.Parameters.AddWithValue("@Nombre_460AS", familia.Nombre_460AS);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Familia_460AS> ObtenerTodas_460AS()
        {
            List<Familia_460AS> lista = new List<Familia_460AS>();

            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = "SELECT CodFamilia_460AS, Nombre_460AS FROM FAMILIA_460AS";
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Familia_460AS fam = new Familia_460AS(
                        reader["CodFamilia_460AS"].ToString(),
                        reader["Nombre_460AS"].ToString()
                    );
                    lista.Add(fam);
                }
            }
            return lista;
        }

        public void Eliminar_460AS(Familia_460AS familia)
        {
            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = @"DELETE FROM FAMILIA_460AS WHERE CodFamilia_460AS = @CodFamilia_460AS";
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@CodFamilia_460AS", familia.Codigo_460AS);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void GuardarFamiliaFamilia_460AS(Familia_460AS familiaPadre, Familia_460AS familiaHijo)
        {
            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = @"INSERT INTO FAMILIA_FAMILIA_460AS (CodFamiliaPadre_460AS, CodFamiliaHijo_460AS)
                                VALUES (@CodFamiliaPadre_460AS, @CodFamiliaHijo_460AS)";
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@CodFamiliaPadre_460AS", familiaPadre.Codigo_460AS);
                cmd.Parameters.AddWithValue("@CodFamiliaHijo_460AS", familiaHijo.Codigo_460AS);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Familia_460AS> ObtenerFamiliasHijas_460AS(Familia_460AS familiaPadre)
        {
            List<Familia_460AS> lista = new List<Familia_460AS>();

            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = @"
                SELECT f.CodFamilia_460AS, f.Nombre_460AS
                FROM FAMILIA_460AS f
                INNER JOIN FAMILIA_FAMILIA_460AS ff ON f.CodFamilia_460AS = ff.CodFamiliaHijo_460AS
                WHERE ff.CodFamiliaPadre_460AS = @CodFamiliaPadre_460AS";
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@CodFamiliaPadre_460AS", familiaPadre.Codigo_460AS);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Familia_460AS fam = new Familia_460AS(
                        reader["CodFamilia_460AS"].ToString(),
                        reader["Nombre_460AS"].ToString()
                    );
                    lista.Add(fam);
                }
            }
            return lista;
        }

        public void EliminarTodasRelacionesFamilia_460AS(Familia_460AS familia)
        {
            using (SqlConnection con = new SqlConnection(cx))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = @"DELETE FROM FAMILIA_FAMILIA_460AS WHERE CodFamiliaPadre_460AS = @Codigo_460AS OR CodFamiliaHijo_460AS = @Codigo_460AS";
                cmd.Parameters.AddWithValue("@Codigo_460AS", familia.Codigo_460AS);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarRelacionFamilia_460AS(Familia_460AS familiaPadre, Familia_460AS familiaHijo)
        {
            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = @"DELETE FROM FAMILIA_FAMILIA_460AS 
                                WHERE CodFamiliaPadre_460AS = @CodFamiliaPadre_460AS 
                                  AND CodFamiliaHijo_460AS = @CodFamiliaHijo_460AS";
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@CodFamiliaPadre_460AS", familiaPadre.Codigo_460AS);
                cmd.Parameters.AddWithValue("@CodFamiliaHijo_460AS", familiaHijo.Codigo_460AS);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AgregarPermisoAFamilia_460AS(string codFamilia, string codPermiso)
        {
            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = @"INSERT INTO FAMILIA_PERMISO_460AS (CodFamilia_460AS, CodPermiso_460AS)
                                VALUES (@CodFamilia, @CodPermiso)";
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@CodFamilia", codFamilia);
                cmd.Parameters.AddWithValue("@CodPermiso", codPermiso);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarPermisoDeFamilia_460AS(string codFamilia, string codPermiso)
        {
            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = @"DELETE FROM FAMILIA_PERMISO_460AS 
                                WHERE CodFamilia_460AS = @CodFamilia AND CodPermiso_460AS = @CodPermiso";
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@CodFamilia", codFamilia);
                cmd.Parameters.AddWithValue("@CodPermiso", codPermiso);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Permiso_460AS> ObtenerPermisosDeFamilia_460AS(string codFamilia)
        {
            List<Permiso_460AS> lista = new List<Permiso_460AS>();

            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = @"SELECT p.CodPermiso_460AS, p.Nombre_460AS, p.Descripcion_460AS
                                FROM PERMISO_460AS p
                                INNER JOIN FAMILIA_PERMISO_460AS fp ON p.CodPermiso_460AS = fp.CodPermiso_460AS
                                WHERE fp.CodFamilia_460AS = @CodFamilia";
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@CodFamilia", codFamilia);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Permiso_460AS permiso = new Permiso_460AS(
                        reader["CodPermiso_460AS"].ToString(),
                        reader["Nombre_460AS"].ToString()
                    )
                    {
                        Descripcion_460AS = reader.IsDBNull(2) ? null : reader["Descripcion_460AS"].ToString()
                    };
                    lista.Add(permiso);
                }
            }

            return lista;
        }



        public List<Familia_460AS> ObtenerFamiliasPadres_460AS(Familia_460AS familiaHija)
        {
            List<Familia_460AS> lista = new List<Familia_460AS>();

            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = @"
            SELECT f.CodFamilia_460AS, f.Nombre_460AS
            FROM FAMILIA_460AS f
            INNER JOIN FAMILIA_FAMILIA_460AS ff ON f.CodFamilia_460AS = ff.CodFamiliaPadre_460AS
            WHERE ff.CodFamiliaHijo_460AS = @CodFamiliaHijo_460AS";

                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@CodFamiliaHijo_460AS", familiaHija.Codigo_460AS);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Familia_460AS fam = new Familia_460AS(
                        reader["CodFamilia_460AS"].ToString(),
                        reader["Nombre_460AS"].ToString()
                    );
                    lista.Add(fam);
                }
            }

            return lista;
        }
    }
}
