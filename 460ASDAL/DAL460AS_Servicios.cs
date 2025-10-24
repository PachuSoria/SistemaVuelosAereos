using _460ASBE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Servicios
    {
        private string cx;
        public DAL460AS_Servicios()
        {
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True";
        }

        public void GuardarServicio_460AS(Servicio_460AS servicio)
        {
            string consulta = "INSERT INTO SERVICIOS_460AS (CodServicio_460AS, CodReserva_460AS, TipoServicio_460AS, Descripcion_460AS, Precio_460AS) " +
                              "VALUES (@CodServicio_460AS, @CodReserva_460AS, @TipoServicio_460AS, @Descripcion_460AS, @Precio_460AS)";

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@CodServicio_460AS", servicio.CodServicio_460AS);
                    comando.Parameters.AddWithValue("@CodReserva_460AS", servicio.CodReserva_460AS);
                    comando.Parameters.AddWithValue("@TipoServicio_460AS", servicio.TipoServicio_460AS);
                    comando.Parameters.AddWithValue("@Descripcion_460AS", servicio.Descripcion_460AS);
                    comando.Parameters.AddWithValue("@Precio_460AS", servicio.Precio_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Servicio_460AS> ObtenerServiciosPorReserva_460AS(string codReserva)
        {
            var lista = new List<Servicio_460AS>();

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = "SELECT CodServicio_460AS, CodReserva_460AS, TipoServicio_460AS, Descripcion_460AS, Precio_460AS " +
                                  "FROM SERVICIOS_460AS WHERE CodReserva_460AS = @CodReserva_460AS";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@CodReserva_460AS", codReserva);
                    conexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var svc = new Servicio_460AS
                            {
                                CodServicio_460AS = reader["CodServicio_460AS"].ToString(),
                                CodReserva_460AS = reader["CodReserva_460AS"].ToString(),
                                TipoServicio_460AS = reader["TipoServicio_460AS"].ToString(),
                                Descripcion_460AS = reader["Descripcion_460AS"].ToString(),
                                Precio_460AS = Convert.ToDecimal(reader["Precio_460AS"])
                            };
                            lista.Add(svc);
                        }
                    }
                }
            }
            return lista;
        }

        public void GuardarComidaEspecial_460AS(ComidaEspecial_460AS comida)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = "INSERT INTO COMIDA_ESPECIAL_460AS (CodServicio_460AS, TipoComida_460AS) " +
                                  "VALUES (@CodServicio_460AS, @TipoComida_460AS)";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@CodServicio_460AS", comida.CodServicio_460AS);
                    cmd.Parameters.AddWithValue("@TipoComida_460AS", comida.TipoComida_460AS);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GuardarValijaExtra_460AS(ValijaExtra_460AS valija)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = "INSERT INTO VALIJA_EXTRA_460AS (CodServicio_460AS, Cantidad_460AS, PesoTotal_460AS) " +
                                  "VALUES (@CodServicio_460AS, @Cantidad_460AS, @PesoTotal_460AS)";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@CodServicio_460AS", valija.CodServicio_460AS);
                    cmd.Parameters.AddWithValue("@Cantidad_460AS", valija.Cantidad_460AS);
                    cmd.Parameters.AddWithValue("@PesoTotal_460AS", valija.PesoTotal_460AS);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GuardarSeguroViaje_460AS(SeguroViaje_460AS seguro)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = "INSERT INTO SEGURO_VIAJE_460AS (CodServicio_460AS, Cobertura_460AS, Vencimiento_460AS) " +
                                  "VALUES (@CodServicio_460AS, @Cobertura_460AS, @Vencimiento_460AS)";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@CodServicio_460AS", seguro.CodServicio_460AS);
                    cmd.Parameters.AddWithValue("@Cobertura_460AS", seguro.Cobertura_460AS);
                    cmd.Parameters.AddWithValue("@Vencimiento_460AS", seguro.Vencimiento_460AS);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GuardarCambioAsiento_460AS(CambioAsiento_460AS cambio)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = "INSERT INTO CAMBIO_ASIENTO_460AS (CodServicio_460AS, NumAsiento_460AS) " +
                                  "VALUES (@CodServicio_460AS, @NumAsiento_460AS)";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@CodServicio_460AS", cambio.CodServicio_460AS);
                    cmd.Parameters.AddWithValue("@NumAsiento_460AS", cambio.NumAsiento_460AS);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
