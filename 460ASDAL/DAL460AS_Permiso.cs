using _460ASServicios.Composite;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Permiso
    {
        private string cx;

        public DAL460AS_Permiso()
        {
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True; MultipleActiveResultSets=True";
        }

        /// <summary>
        /// Guarda un Permiso_460AS en la base de datos.
        /// </summary>
        /// <param name="permiso">El permiso a guardar.</param>
        public void Guardar_460AS(Permiso_460AS permiso)
        {
            using SqlConnection conexion = new(cx);
            conexion.Open();

            string query = "INSERT INTO PERMISO_460AS (CodPermiso_460AS, Nombre_460AS, Descripcion_460AS) VALUES (@CodPermiso_460AS, @Nombre_460AS, @Descripcion_460AS)";
            using SqlCommand cmd = new(query, conexion);
            cmd.Parameters.AddWithValue("@CodPermiso_460AS", permiso.Codigo_460AS);
            cmd.Parameters.AddWithValue("@Nombre_460AS", permiso.Nombre_460AS);
            cmd.Parameters.AddWithValue("@Descripcion_460AS", (object)permiso.Descripcion_460AS ?? DBNull.Value); 
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Actualiza un Permiso_460AS existente en la base de datos.
        /// </summary>
        /// <param name="permiso">El permiso a actualizar.</param>
        public void Actualizar_460AS(Permiso_460AS permiso)
        {
            using SqlConnection conexion = new(cx);
            conexion.Open();

            string query = "UPDATE PERMISO_460AS SET Nombre_460AS = @Nombre_460AS, Descripcion_460AS = @Descripcion_460AS WHERE CodPermiso_460AS = @CodPermiso_460AS";
            using SqlCommand cmd = new(query, conexion);
            cmd.Parameters.AddWithValue("@Nombre_460AS", permiso.Nombre_460AS);
            cmd.Parameters.AddWithValue("@Descripcion_460AS", (object)permiso.Descripcion_460AS ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CodPermiso_460AS", permiso.Codigo_460AS);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Elimina un Permiso_460AS de la base de datos por su código.
        /// </summary>
        /// <param name="codigoPermiso">El código del permiso a eliminar.</param>
        public void EliminarPorCodigo_460AS(string codigoPermiso)
        {
            using SqlConnection conexion = new(cx);
            conexion.Open();

            string query = "DELETE FROM PERMISO_460AS WHERE CodPermiso_460AS = @CodPermiso_460AS";
            using SqlCommand cmd = new(query, conexion);
            cmd.Parameters.AddWithValue("@CodPermiso_460AS", codigoPermiso);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Elimina un Permiso_460AS de la base de datos.
        /// </summary>
        /// <param name="permiso">El permiso a eliminar.</param>
        public void Eliminar_460AS(Permiso_460AS permiso)
        {
            EliminarPorCodigo_460AS(permiso.Codigo_460AS);
        }

        /// <summary>
        /// Obtiene un Permiso_460AS por su código.
        /// </summary>
        /// <param name="codigoPermiso">El código del permiso a obtener.</param>
        /// <returns>El Permiso_460AS encontrado o null si no existe.</returns>
        public Permiso_460AS ObtenerPorCodigo_460AS(string codigoPermiso)
        {
            using SqlConnection conexion = new(cx);
            conexion.Open();

            string query = "SELECT CodPermiso_460AS, Nombre_460AS, Descripcion_460AS FROM PERMISO_460AS WHERE CodPermiso_460AS = @CodPermiso_460AS";
            using SqlCommand cmd = new(query, conexion);
            cmd.Parameters.AddWithValue("@CodPermiso_460AS", codigoPermiso);
            using SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read()) return null;

            Permiso_460AS permiso = new(
                codigo: reader.GetString(0),
                nombre: reader.GetString(1)
            )
            {
                Descripcion_460AS = reader.IsDBNull(2) ? null : reader.GetString(2)
            };

            return permiso;
        }

        /// <summary>
        /// Obtiene todos los Permiso_460AS de la base de datos.
        /// </summary>
        /// <returns>Una lista de todos los Permiso_460AS.</returns>
        public IList<Permiso_460AS> ObtenerTodos_460AS()
        {
            List<Permiso_460AS> permisos = new();

            using SqlConnection conexion = new(cx);
            conexion.Open();

            string query = "SELECT CodPermiso_460AS, Nombre_460AS, Descripcion_460AS FROM PERMISO_460AS";
            using SqlCommand cmd = new(query, conexion);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Permiso_460AS permiso = new(
                    codigo: reader.GetString(0),
                    nombre: reader.GetString(1)
                )
                {
                    Descripcion_460AS = reader.IsDBNull(2) ? null : reader.GetString(2)
                };
                permisos.Add(permiso);
            }
            return permisos;
        }
    }
}