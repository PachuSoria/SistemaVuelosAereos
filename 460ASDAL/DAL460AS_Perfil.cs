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
        private string cx;
        public DAL460AS_Perfil()
        {
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True; MultipleActiveResultSets = True";
        }
        public void Guardar_460AS(Perfil_460AS perfil)
        {
            using SqlConnection conexion = new(cx);
            conexion.Open();
            SqlTransaction transaction = conexion.BeginTransaction();

            try
            {
                string query = "INSERT INTO PERFIL_460AS (CodPerfil_460AS, Nombre_460AS) VALUES (@CodPerfil_460AS, @Nombre_460AS)";
                using SqlCommand cmd = new(query, conexion, transaction);
                cmd.Parameters.AddWithValue("@CodPerfil_460AS", perfil.Codigo_460AS);
                cmd.Parameters.AddWithValue("@Nombre_460AS", perfil.Nombre_460AS);
                cmd.ExecuteNonQuery();

                GuardarRelaciones_460AS(perfil, conexion, transaction);
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Actualiza un Perfil_460AS y sus relaciones de forma atómica.
        /// </summary>
        /// <param name="perfil">El perfil a actualizar.</param>
        public void Actualizar_460AS(Perfil_460AS perfil)
        {
            using SqlConnection conexion = new(cx);
            conexion.Open();
            SqlTransaction transaction = conexion.BeginTransaction();

            try
            {
                string query = "UPDATE PERFIL_460AS SET Nombre_460AS = @Nombre_460AS WHERE CodPerfil_460AS = @CodPerfil_460AS";
                using SqlCommand cmd = new(query, conexion, transaction);
                cmd.Parameters.AddWithValue("@Nombre_460AS", perfil.Nombre_460AS);
                cmd.Parameters.AddWithValue("@CodPerfil_460AS", perfil.Codigo_460AS);
                cmd.ExecuteNonQuery();

                EliminarRelaciones_460AS(perfil.Codigo_460AS, conexion, transaction);
                GuardarRelaciones_460AS(perfil, conexion, transaction);
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Elimina un Perfil_460AS y sus relaciones de forma atómica.
        /// </summary>
        /// <param name="perfil">El perfil a eliminar.</param>
        public void Eliminar_460AS(Perfil_460AS perfil)
        {
            EliminarPorCodigo_460AS(perfil.Codigo_460AS);
        }

        /// <summary>
        /// Elimina un Perfil_460AS y sus relaciones por su código de forma atómica.
        /// </summary>
        /// <param name="codigoPerfil">El código del perfil a eliminar.</param>
        public void EliminarPorCodigo_460AS(string codigoPerfil)
        {
            using SqlConnection conexion = new(cx);
            conexion.Open();
            SqlTransaction transaction = conexion.BeginTransaction();

            try
            {
                // Eliminar primero las relaciones para evitar problemas de FK
                EliminarRelaciones_460AS(codigoPerfil, conexion, transaction);

                string query = "DELETE FROM PERFIL_460AS WHERE CodPerfil_460AS = @CodPerfil_460AS";
                using SqlCommand cmd = new(query, conexion, transaction);
                cmd.Parameters.AddWithValue("@CodPerfil_460AS", codigoPerfil);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Obtiene un Perfil_460AS por su código, cargando sus relaciones.
        /// </summary>
        /// <param name="codigoPerfil">El código del perfil a obtener.</param>
        /// <returns>El Perfil_460AS encontrado o null si no existe.</returns>
        public Perfil_460AS ObtenerPorCodigo_460AS(string codigoPerfil)
        {
            using SqlConnection conexion = new(cx);
            conexion.Open();

            string query = "SELECT CodPerfil_460AS, Nombre_460AS FROM PERFIL_460AS WHERE CodPerfil_460AS = @CodPerfil_460AS";
            using SqlCommand cmd = new(query, conexion);
            cmd.Parameters.AddWithValue("@CodPerfil_460AS", codigoPerfil);
            using SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read()) return null;

            Perfil_460AS perfil = new(
                codigo: reader.GetString(0),
                nombre: reader.GetString(1)
            );
            reader.Close();

            // Diccionario para controlar objetos ya cargados y evitar recursión infinita o duplicados
            Dictionary<string, IComponentePermiso_460AS> loadedComponents = new Dictionary<string, IComponentePermiso_460AS>();
            loadedComponents.Add(perfil.Codigo_460AS, perfil); // Añade el perfil actual

            CargarRelaciones_460AS(perfil, conexion, loadedComponents);

            return perfil;
        }

        /// <summary>
        /// Obtiene todos los Perfil_460AS, cargando sus relaciones.
        /// </summary>
        /// <returns>Una lista de todos los Perfil_460AS.</returns>
        public IList<Perfil_460AS> ObtenerTodos_460AS()
        {
            List<Perfil_460AS> perfiles = new();

            using SqlConnection conexion = new(cx);
            conexion.Open();

            string query = "SELECT CodPerfil_460AS, Nombre_460AS FROM PERFIL_460AS";
            using SqlCommand cmd = new(query, conexion);
            using SqlDataReader reader = cmd.ExecuteReader();

            // Primer pasada: Cargar solo los objetos raíz de Perfil_460AS
            while (reader.Read())
            {
                Perfil_460AS perfil = new(
                    codigo: reader.GetString(0),
                    nombre: reader.GetString(1)
                );
                perfiles.Add(perfil);
            }
            reader.Close();

            // Segunda pasada: Cargar las relaciones para cada perfil
            // Se usa un diccionario para evitar cargar componentes duplicados
            Dictionary<string, IComponentePermiso_460AS> loadedComponents = new Dictionary<string, IComponentePermiso_460AS>();
            foreach (var perfil in perfiles)
            {
                if (!loadedComponents.ContainsKey(perfil.Codigo_460AS))
                {
                    loadedComponents.Add(perfil.Codigo_460AS, perfil);
                }
                CargarRelaciones_460AS(perfil, conexion, loadedComponents);
            }

            return perfiles;
        }

        /// <summary>
        /// Guarda las relaciones de un perfil con sus hijos (permisos y familias).
        /// </summary>
        private void GuardarRelaciones_460AS(Perfil_460AS perfil, SqlConnection conexion, SqlTransaction transaction)
        {
            foreach (var hijo in perfil.ObtenerHijos())
            {
                if (hijo is Permiso_460AS permiso)
                {
                    string insert = "INSERT INTO PERFIL_PERMISO_460AS (CodPerfil_460AS, CodPermiso_460AS) VALUES (@CodPerfil_460AS, @CodPermiso_460AS)";
                    using SqlCommand cmd = new(insert, conexion, transaction);
                    cmd.Parameters.AddWithValue("@CodPerfil_460AS", perfil.Codigo_460AS);
                    cmd.Parameters.AddWithValue("@CodPermiso_460AS", permiso.Codigo_460AS);
                    cmd.ExecuteNonQuery();
                }
                else if (hijo is Familia_460AS familia)
                {
                    string insert = "INSERT INTO PERFIL_FAMILIA_460AS (CodPerfil_460AS, CodFamilia_460AS) VALUES (@CodPerfil_460AS, @CodFamilia_460AS)";
                    using SqlCommand cmd = new(insert, conexion, transaction);
                    cmd.Parameters.AddWithValue("@CodPerfil_460AS", perfil.Codigo_460AS);
                    cmd.Parameters.AddWithValue("@CodFamilia_460AS", familia.Codigo_460AS);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Elimina las relaciones de un perfil con sus hijos.
        /// </summary>
        private void EliminarRelaciones_460AS(string codigoPerfil, SqlConnection conexion, SqlTransaction transaction)
        {
            string deletePermisos = "DELETE FROM PERFIL_PERMISO_460AS WHERE CodPerfil_460AS = @CodPerfil_460AS";
            using SqlCommand cmd1 = new(deletePermisos, conexion, transaction);
            cmd1.Parameters.AddWithValue("@CodPerfil_460AS", codigoPerfil);
            cmd1.ExecuteNonQuery();

            string deleteFamilias = "DELETE FROM PERFIL_FAMILIA_460AS WHERE CodPerfil_460AS = @CodPerfil_460AS";
            using SqlCommand cmd2 = new(deleteFamilias, conexion, transaction);
            cmd2.Parameters.AddWithValue("@CodPerfil_460AS", codigoPerfil);
            cmd2.ExecuteNonQuery();
        }

        /// <summary>
        /// Carga las relaciones (hijos) de un perfil.
        /// </summary>
        /// <param name="perfil">El perfil al que se cargarán los hijos.</param>
        /// <param name="conexion">La conexión SQL activa.</param>
        /// <param name="loadedComponents">Diccionario para evitar la recarga de componentes ya instanciados.</param>
        private void CargarRelaciones_460AS(Perfil_460AS perfil, SqlConnection conexion, Dictionary<string, IComponentePermiso_460AS> loadedComponents)
        {
            // Cargar permisos directos
            string queryPermisos = @"
            SELECT p.CodPermiso_460AS, p.Nombre_460AS, p.Descripcion_460AS 
            FROM PERFIL_PERMISO_460AS pp 
            JOIN PERMISO_460AS p ON pp.CodPermiso_460AS = p.CodPermiso_460AS 
            WHERE pp.CodPerfil_460AS = @CodPerfil_460AS";

            using (SqlCommand cmd = new(queryPermisos, conexion))
            {
                cmd.Parameters.AddWithValue("@CodPerfil_460AS", perfil.Codigo_460AS);
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string codPermiso = reader.GetString(0);
                    if (loadedComponents.TryGetValue(codPermiso, out IComponentePermiso_460AS componenteExistente) && componenteExistente is Permiso_460AS pExistente)
                    {
                        perfil.AgregarHijo(pExistente);
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
                        perfil.AgregarHijo(permiso);
                        loadedComponents.Add(permiso.Codigo_460AS, permiso);
                    }
                }
            }

            string queryFamilias = @"
            SELECT f.CodFamilia_460AS, f.Nombre_460AS 
            FROM PERFIL_FAMILIA_460AS pf 
            JOIN FAMILIA_460AS f ON pf.CodFamilia_460AS = f.CodFamilia_460AS 
            WHERE pf.CodPerfil_460AS = @CodPerfil_460AS";

            using (SqlCommand cmd2 = new(queryFamilias, conexion))
            {
                cmd2.Parameters.AddWithValue("@CodPerfil_460AS", perfil.Codigo_460AS);
                using SqlDataReader reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    string codFamilia = reader2.GetString(0);
                    if (loadedComponents.TryGetValue(codFamilia, out IComponentePermiso_460AS componenteExistente) && componenteExistente is Familia_460AS famExistente)
                    {
                        perfil.AgregarHijo(famExistente);
                    }
                    else
                    {
                        Familia_460AS familia = new()
                        {
                            Codigo_460AS = codFamilia,
                            Nombre_460AS = reader2.GetString(1)
                        };
                        perfil.AgregarHijo(familia);
                        loadedComponents.Add(familia.Codigo_460AS, familia);

                        DAL460AS_Familia dalFamilia = new DAL460AS_Familia();
                        dalFamilia.CargarRelaciones_460AS(familia, conexion, loadedComponents);
                    }
                }
            }
        }
    }
}
