using _460ASBE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Comprobante
    {
        private string cx;
        public DAL460AS_Comprobante()
        {
            cx = "Data Source=.;Initial Catalog=\"Vuelos Aereos\";Integrated Security=True;Trust Server Certificate=True";
        }

        public void GuardarComprobante(Comprobante_460AS comprobante)
        {
            string consulta = "INSERT INTO COMPROBANTE_460AS (CodComprobante_460AS, CodReserva_460AS, Monto_460AS, TipoPago_460AS, FechaPago_460AS)" + 
                              "VALUES (@CodComprobante_460AS, @CodReserva_460AS, @Monto_460AS, @TipoPago_460AS, @FechaPago_460AS)";
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@CodComprobante_460AS", comprobante.CodComprobante_460AS);
                    comando.Parameters.AddWithValue("@CodReserva_460AS", comprobante.Reserva_460AS.CodReserva_460AS);
                    comando.Parameters.AddWithValue("@Monto_460AS", comprobante.Monto_460AS);
                    comando.Parameters.AddWithValue("@TipoPago_460AS", comprobante.TipoPago_460AS);
                    comando.Parameters.AddWithValue("@FechaPago_460AS", comprobante.FechaPago_460AS);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public bool ExisteCodigoComprobante_460AS(string codComprobante)
        {
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM COMPROBANTE_460AS WHERE CodComprobante_460AS = @CodComprobante_460AS", conexion);
                cmd.Parameters.AddWithValue("@CodComprobante_460AS", codComprobante);
                conexion.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public List<Comprobante_460AS> ObtenerTodos_460AS()
        {
            List<Comprobante_460AS> lista = new List<Comprobante_460AS>();

            string consulta = "SELECT CodComprobante_460AS, CodReserva_460AS, Monto_460AS, TipoPago_460AS, FechaPago_460AS FROM COMPROBANTE_460AS";

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        string codComprobante = lector["CodComprobante_460AS"].ToString();
                        string codReserva = lector["CodReserva_460AS"].ToString();
                        decimal monto = Convert.ToDecimal(lector["Monto_460AS"]);
                        string tipoPago = lector["TipoPago_460AS"].ToString();
                        DateTime fechaPago = Convert.ToDateTime(lector["FechaPago_460AS"]);

                        Comprobante_460AS comprobante = new Comprobante_460AS
                        {
                            CodComprobante_460AS = codComprobante,
                            Reserva_460AS = new Reserva_460AS { CodReserva_460AS = codReserva }, 
                            Monto_460AS = monto,
                            TipoPago_460AS = tipoPago,
                            FechaPago_460AS = fechaPago
                        };

                        lista.Add(comprobante);
                    }
                }
            }

            return lista;
        }
    }
}
