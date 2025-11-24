using _460ASServicios.Composite;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Permiso : DAL_Abstract
    {
        public DAL460AS_Permiso()
        {
            
        }

        public List<Permiso_460AS> ObtenerTodos_460AS()
        {
            List<Permiso_460AS> lista_460AS = new List<Permiso_460AS>();

            using SqlConnection con_460AS = new SqlConnection(cx);
            SqlCommand cmd_460AS = new SqlCommand("SELECT CodPermiso_460AS, Nombre_460AS, Descripcion_460AS FROM PERMISO_460AS", con_460AS);
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
    }
}

