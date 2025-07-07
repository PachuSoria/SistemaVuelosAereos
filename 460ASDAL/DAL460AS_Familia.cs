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
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True; MultipleActiveResultSets=True";
        }

        public void Guardar_460AS(Familia_460AS familia)
        {
            using SqlConnection conexion = new(cx);
            conexion.Open();
            SqlTransaction transaction = conexion.BeginTransaction();

            try
            {
                string query = "INSERT INTO FAMILIA_460AS (CodFamilia_460AS, Nombre_460AS) VALUES (@CodFamilia_460AS, @Nombre_460AS)";
                using SqlCommand cmd = new(query, conexion, transaction);
                cmd.Parameters.AddWithValue("@CodFamilia_460AS", familia.Codigo_460AS);
                cmd.Parameters.AddWithValue("@Nombre_460AS", familia.Nombre_460AS);
                cmd.ExecuteNonQuery();

                GuardarRelaciones_460AS(familia, conexion, transaction);
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Actualizar_460AS(Familia_460AS familia)
        {
            using SqlConnection conexion = new(cx);
            conexion.Open();
            SqlTransaction transaction = conexion.BeginTransaction();

            try
            {
                string query = "UPDATE FAMILIA_460AS SET Nombre_460AS = @Nombre_460AS WHERE CodFamilia_460AS = @CodFamilia_460AS";
                using SqlCommand cmd = new(query, conexion, transaction);
                cmd.Parameters.AddWithValue("@Nombre_460AS", familia.Nombre_460AS);
                cmd.Parameters.AddWithValue("@CodFamilia_460AS", familia.Codigo_460AS);
                cmd.ExecuteNonQuery();

                EliminarRelaciones_460AS(familia.Codigo_460AS, conexion, transaction);
                GuardarRelaciones_460AS(familia, conexion, transaction);
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Eliminar_460AS(Familia_460AS familia)
        {
            EliminarPorCodigo_460AS(familia.Codigo_460AS);
        }

        public void EliminarPorCodigo_460AS(string codigoFamilia)
        {
            using SqlConnection conexion = new(cx);
            conexion.Open();
            SqlTransaction transaction = conexion.BeginTransaction();

            try
            {
                EliminarRelaciones_460AS(codigoFamilia, conexion, transaction);

                string query = "DELETE FROM FAMILIA_460AS WHERE CodFamilia_460AS = @CodFamilia_460AS";
                using SqlCommand cmd = new(query, conexion, transaction);
                cmd.Parameters.AddWithValue("@CodFamilia_460AS", codigoFamilia);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public Familia_460AS ObtenerPorCodigo_460AS(string codigoFamilia)
        {
            using SqlConnection conexion = new(cx);
            conexion.Open();

            string query = "SELECT CodFamilia_460AS, Nombre_460AS FROM FAMILIA_460AS WHERE CodFamilia_460AS = @CodFamilia_460AS";
            using SqlCommand cmd = new(query, conexion);
            cmd.Parameters.AddWithValue("@CodFamilia_460AS", codigoFamilia);
            using SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read()) return null;

            Familia_460AS familia = new()
            {
                Codigo_460AS = reader.GetString(0),
                Nombre_460AS = reader.GetString(1)
            };
            reader.Close();

            Dictionary<string, IComponentePermiso_460AS> loadedComponents = new Dictionary<string, IComponentePermiso_460AS>();
            loadedComponents.Add(familia.Codigo_460AS, familia);

            CargarRelaciones_460AS(familia, conexion, loadedComponents);

            return familia;
        }

        public IList<Familia_460AS> ObtenerTodos_460AS()
        {
            List<Familia_460AS> familias = new();

            using SqlConnection conexion = new(cx);
            conexion.Open();

            string query = "SELECT CodFamilia_460AS, Nombre_460AS FROM FAMILIA_460AS";
            using SqlCommand cmd = new(query, conexion);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Familia_460AS familia = new()
                {
                    Codigo_460AS = reader.GetString(0),
                    Nombre_460AS = reader.GetString(1)
                };
                familias.Add(familia);
            }
            reader.Close();

            Dictionary<string, IComponentePermiso_460AS> loadedComponents = new Dictionary<string, IComponentePermiso_460AS>();
            foreach (var familia in familias)
            {
                if (!loadedComponents.ContainsKey(familia.Codigo_460AS))
                {
                    loadedComponents.Add(familia.Codigo_460AS, familia);
                }
                CargarRelaciones_460AS(familia, conexion, loadedComponents);
            }

            return familias;
        }

        private void GuardarRelaciones_460AS(Familia_460AS familia, SqlConnection conexion, SqlTransaction transaction)
        {
            foreach (var hijo in familia.ObtenerHijos())
            {
                if (hijo is Permiso_460AS permiso)
                {
                    string insert = "INSERT INTO FAMILIA_PERMISO_460AS (CodFamilia_460AS, CodPermiso_460AS) VALUES (@CodFamilia_460AS, @CodPermiso_460AS)";
                    using SqlCommand cmd = new(insert, conexion, transaction);
                    cmd.Parameters.AddWithValue("@CodFamilia_460AS", familia.Codigo_460AS);
                    cmd.Parameters.AddWithValue("@CodPermiso_460AS", permiso.Codigo_460AS);
                    cmd.ExecuteNonQuery();
                }
                else if (hijo is Familia_460AS famHijo)
                {
                    string insert = "INSERT INTO FAMILIA_FAMILIA_460AS (CodFamiliaPadre_460AS, CodFamiliaHijo_460AS) VALUES (@CodFamiliaPadre_460AS, @CodFamiliaHijo_460AS)";
                    using SqlCommand cmd = new(insert, conexion, transaction);
                    cmd.Parameters.AddWithValue("@CodFamiliaPadre_460AS", familia.Codigo_460AS);
                    cmd.Parameters.AddWithValue("@CodFamiliaHijo_460AS", famHijo.Codigo_460AS);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void EliminarRelaciones_460AS(string codigoFamilia, SqlConnection conexion, SqlTransaction transaction)
        {
            string deletePermisos = "DELETE FROM FAMILIA_PERMISO_460AS WHERE CodFamilia_460AS = @CodFamilia_460AS";
            using SqlCommand cmd1 = new(deletePermisos, conexion, transaction);
            cmd1.Parameters.AddWithValue("@CodFamilia_460AS", codigoFamilia);
            cmd1.ExecuteNonQuery();

            string deleteHijos = "DELETE FROM FAMILIA_FAMILIA_460AS WHERE CodFamiliaPadre_460AS = @CodFamiliaPadre_460AS";
            using SqlCommand cmd2 = new(deleteHijos, conexion, transaction);
            cmd2.Parameters.AddWithValue("@CodFamiliaPadre_460AS", codigoFamilia);
            cmd2.ExecuteNonQuery();
        }

        public void CargarRelaciones_460AS(Familia_460AS familia, SqlConnection conexion, Dictionary<string, IComponentePermiso_460AS> loadedComponents)
        {
            string queryPermisos = @"
            SELECT p.CodPermiso_460AS, p.Nombre_460AS, p.Descripcion_460AS 
            FROM FAMILIA_PERMISO_460AS fp 
            JOIN PERMISO_460AS p ON fp.CodPermiso_460AS = p.CodPermiso_460AS 
            WHERE fp.CodFamilia_460AS = @CodFamilia_460AS";

            using (SqlCommand cmd = new(queryPermisos, conexion))
            {
                cmd.Parameters.AddWithValue("@CodFamilia_460AS", familia.Codigo_460AS);
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string codPermiso = reader.GetString(0);
                    if (loadedComponents.TryGetValue(codPermiso, out IComponentePermiso_460AS componenteExistente) && componenteExistente is Permiso_460AS pExistente)
                    {
                        familia.AgregarHijo(pExistente);
                    }
                    else
                    {
                        Permiso_460AS permiso = new(
                            codigo: codPermiso,
                            nombre: reader.GetString(1)
                        )
                        {
                            Descripcion_460AS = reader.IsDBNull(2) ? null : reader.GetString(2)
                        };
                        familia.AgregarHijo(permiso);
                        loadedComponents.Add(permiso.Codigo_460AS, permiso);
                    }
                }
            }

            string queryHijos = @"
            SELECT f.CodFamilia_460AS, f.Nombre_460AS 
            FROM FAMILIA_FAMILIA_460AS ff 
            JOIN FAMILIA_460AS f ON ff.CodFamiliaHijo_460AS = f.CodFamilia_460AS 
            WHERE ff.CodFamiliaPadre_460AS = @CodFamiliaPadre_460AS";

            using (SqlCommand cmd2 = new(queryHijos, conexion))
            {
                cmd2.Parameters.AddWithValue("@CodFamiliaPadre_460AS", familia.Codigo_460AS);
                using SqlDataReader reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    string codHijo = reader2.GetString(0);
                    if (loadedComponents.TryGetValue(codHijo, out IComponentePermiso_460AS componenteExistente) && componenteExistente is Familia_460AS famHijoExistente)
                    {
                        familia.AgregarHijo(famHijoExistente);
                    }
                    else
                    {
                        Familia_460AS hijo = new()
                        {
                            Codigo_460AS = codHijo,
                            Nombre_460AS = reader2.GetString(1)
                        };
                        familia.AgregarHijo(hijo);
                        loadedComponents.Add(hijo.Codigo_460AS, hijo);

                        CargarRelaciones_460AS(hijo, conexion, loadedComponents);
                    }
                }
            }
        }
    }
}