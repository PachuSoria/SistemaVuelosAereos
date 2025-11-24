using _460ASBE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASDAL
{
    public class DAL460AS_Pago : DAL_Abstract
    {
        public DAL460AS_Pago()
        {
            
        }

        public void GuardarPago_460AS(Pago_460AS pago)
        {
            if (string.IsNullOrWhiteSpace(pago.CodPago_460AS))
                pago.CodPago_460AS = Guid.NewGuid().ToString();
            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = @"INSERT INTO PAGO_460AS 
                            (CodPago_460AS, Monto_460AS, FechaPago_460AS, TipoPago_460AS, CodReserva_460AS, DNICliente_460AS)
                            VALUES 
                            (@CodPago_460AS, @Monto_460AS, @FechaPago_460AS, @TipoPago_460AS, @CodReserva_460AS, @DNICliente_460AS)";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    if (pago == null) throw new Exception("El objeto pago es nulo.");
                    if (pago.Reserva_460AS == null) throw new Exception("La reserva del pago no puede ser nula.");
                    if (pago.Reserva_460AS.Cliente_460AS == null) throw new Exception("El cliente del pago no puede ser nulo.");

                    cmd.Parameters.AddWithValue("@CodPago_460AS", pago.CodPago_460AS);
                    cmd.Parameters.AddWithValue("@Monto_460AS", pago.Monto_460AS);
                    cmd.Parameters.AddWithValue("@FechaPago_460AS", pago.FechaPago_460AS);
                    cmd.Parameters.AddWithValue("@TipoPago_460AS", pago.TipoPago_460AS ?? "Desconocido");
                    cmd.Parameters.AddWithValue("@CodReserva_460AS", pago.Reserva_460AS.CodReserva_460AS);
                    cmd.Parameters.AddWithValue("@DNICliente_460AS", pago.Reserva_460AS.Cliente_460AS.DNI_460AS);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            GuardarServiciosPagados_460AS(pago);
        }

        private void GuardarServiciosPagados_460AS(Pago_460AS pago)
        {
            if (pago.ServiciosPagados == null || pago.ServiciosPagados.Count == 0)
                return;

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                conexion.Open();

                string checkQuery = @"SELECT COUNT(*) FROM PAGO_SERVICIOS_460AS 
                              WHERE CodPago_460AS = @CodPago_460AS AND CodServicio_460AS = @CodServicio_460AS";

                string insertQuery = @"INSERT INTO PAGO_SERVICIOS_460AS
                               (CodPago_460AS, CodServicio_460AS)
                               VALUES (@CodPago_460AS, @CodServicio_460AS)";

                foreach (var s in pago.ServiciosPagados)
                {
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conexion))
                    {
                        checkCmd.Parameters.AddWithValue("@CodPago_460AS", pago.CodPago_460AS);
                        checkCmd.Parameters.AddWithValue("@CodServicio_460AS", s.CodServicio_460AS);

                        int existe = (int)checkCmd.ExecuteScalar();
                        if (existe > 0)
                            continue;
                    }

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conexion))
                    {
                        insertCmd.Parameters.AddWithValue("@CodPago_460AS", pago.CodPago_460AS);
                        insertCmd.Parameters.AddWithValue("@CodServicio_460AS", s.CodServicio_460AS);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<Pago_460AS> ObtenerPagosPorReserva_460AS(string codReserva)
        {
            var lista = new List<Pago_460AS>();

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = @"SELECT * FROM PAGO_460AS WHERE CodReserva_460AS = @CodReserva_460AS";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@CodReserva_460AS", codReserva);
                    conexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pago = new Pago_460AS
                            {
                                CodPago_460AS = reader["CodPago_460AS"].ToString(),
                                Monto_460AS = Convert.ToDecimal(reader["Monto_460AS"]),
                                TipoPago_460AS = reader["TipoPago_460AS"].ToString(),
                                FechaPago_460AS = Convert.ToDateTime(reader["FechaPago_460AS"]),
                                Reserva_460AS = new Reserva_460AS
                                {
                                    CodReserva_460AS = reader["CodReserva_460AS"].ToString()
                                }
                            };

                            lista.Add(pago);
                        }
                    }
                }

                foreach (var pago in lista)
                {
                    string consultaServicios = @"SELECT s.CodServicio_460AS, s.TipoServicio_460AS, s.Descripcion_460AS, s.Precio_460AS
                                         FROM PAGO_SERVICIOS_460AS ps
                                         INNER JOIN SERVICIOS_460AS s ON ps.CodServicio_460AS = s.CodServicio_460AS
                                         WHERE ps.CodPago_460AS = @CodPago_460AS";

                    using (SqlCommand cmdServ = new SqlCommand(consultaServicios, conexion))
                    {
                        cmdServ.Parameters.AddWithValue("@CodPago_460AS", pago.CodPago_460AS);

                        using (SqlDataReader readerServ = cmdServ.ExecuteReader())
                        {
                            var servicios = new List<Servicio_460AS>();

                            while (readerServ.Read())
                            {
                                servicios.Add(new Servicio_460AS
                                {
                                    CodServicio_460AS = readerServ["CodServicio_460AS"].ToString(),
                                    TipoServicio_460AS = readerServ["TipoServicio_460AS"].ToString(),
                                    Descripcion_460AS = readerServ["Descripcion_460AS"].ToString(),
                                    Precio_460AS = Convert.ToDecimal(readerServ["Precio_460AS"])
                                });
                            }

                            pago.ServiciosPagados = servicios;
                        }
                    }
                }
            }

            return lista;
        }

        public List<Pago_460AS> ObtenerTodosLosPagos_460AS()
        {
            var lista = new List<Pago_460AS>();

            using (SqlConnection conexion = new SqlConnection(cx))
            {
                string consulta = @"SELECT * FROM PAGO_460AS";
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pago = new Pago_460AS
                            {
                                CodPago_460AS = reader["CodPago_460AS"].ToString(),
                                Monto_460AS = Convert.ToDecimal(reader["Monto_460AS"]),
                                TipoPago_460AS = reader["TipoPago_460AS"].ToString(),
                                FechaPago_460AS = Convert.ToDateTime(reader["FechaPago_460AS"]),
                                Reserva_460AS = new Reserva_460AS
                                {
                                    CodReserva_460AS = reader["CodReserva_460AS"].ToString()
                                }
                            };
                            lista.Add(pago);
                        }
                    }
                }

                foreach (var pago in lista)
                {
                    string consultaServicios = @"SELECT s.CodServicio_460AS, s.TipoServicio_460AS, s.Descripcion_460AS, s.Precio_460AS
                                         FROM PAGO_SERVICIOS_460AS ps
                                         INNER JOIN SERVICIOS_460AS s ON ps.CodServicio_460AS = s.CodServicio_460AS
                                         WHERE ps.CodPago_460AS = @CodPago_460AS";

                    using (SqlCommand cmdServ = new SqlCommand(consultaServicios, conexion))
                    {
                        cmdServ.Parameters.AddWithValue("@CodPago_460AS", pago.CodPago_460AS);
                        using (SqlDataReader readerServ = cmdServ.ExecuteReader())
                        {
                            var servicios = new List<Servicio_460AS>();
                            while (readerServ.Read())
                            {
                                servicios.Add(new Servicio_460AS
                                {
                                    CodServicio_460AS = readerServ["CodServicio_460AS"].ToString(),
                                    TipoServicio_460AS = readerServ["TipoServicio_460AS"].ToString(),
                                    Descripcion_460AS = readerServ["Descripcion_460AS"].ToString(),
                                    Precio_460AS = Convert.ToDecimal(readerServ["Precio_460AS"])
                                });
                            }
                            pago.ServiciosPagados = servicios;
                        }
                    }
                }
            }

            return lista;
        }

        public List<Pago_460AS> ObtenerPagosServicios_460AS()
        {
            List<Pago_460AS> lista = new List<Pago_460AS>();
            using (SqlConnection con = new SqlConnection(cx))
            {
                con.Open();
                string query = @"
                                SELECT p.CodPago_460AS, p.CodReserva_460AS, p.Monto_460AS, 
                                       p.TipoPago_460AS, p.FechaPago_460AS, p.DNICliente_460AS,
                                       ps.CodServicio_460AS, s.TipoServicio_460AS, s.Descripcion_460AS, s.Precio_460AS
                                FROM PAGO_460AS p
                                LEFT JOIN PAGO_SERVICIOS_460AS ps ON p.CodPago_460AS = ps.CodPago_460AS
                                LEFT JOIN SERVICIOS_460AS s ON ps.CodServicio_460AS = s.CodServicio_460AS";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();

                Dictionary<string, Pago_460AS> pagos = new Dictionary<string, Pago_460AS>();

                while (dr.Read())
                {
                    string codPago = dr["CodPago_460AS"].ToString();
                    if (!pagos.ContainsKey(codPago))
                    {
                        Pago_460AS pago = new Pago_460AS
                        {
                            CodPago_460AS = codPago,
                            Monto_460AS = Convert.ToDecimal(dr["Monto_460AS"]),
                            TipoPago_460AS = dr["TipoPago_460AS"].ToString(),
                            FechaPago_460AS = Convert.ToDateTime(dr["FechaPago_460AS"]),
                            ServiciosPagados = new List<Servicio_460AS>(),
                            Reserva_460AS = new Reserva_460AS
                            {
                                CodReserva_460AS = dr["CodReserva_460AS"].ToString(),
                                Cliente_460AS = new Cliente_460AS
                                {
                                    DNI_460AS = dr["DNICliente_460AS"].ToString()
                                }
                            }
                        };
                        pagos.Add(codPago, pago);
                    }

                    if (dr["CodServicio_460AS"] != DBNull.Value)
                    {
                        pagos[codPago].ServiciosPagados.Add(new Servicio_460AS
                        {
                            CodServicio_460AS = dr["CodServicio_460AS"].ToString(),
                            TipoServicio_460AS = dr["TipoServicio_460AS"].ToString(),
                            Descripcion_460AS = dr["Descripcion_460AS"].ToString(),
                            Precio_460AS = Convert.ToDecimal(dr["Precio_460AS"])
                        });
                    }
                }
                dr.Close();
                lista = pagos.Values.ToList();
            }
            return lista;
        }

        public List<string> ObtenerNombresServiciosDePago(string codPago)
        {
            List<string> servicios = new List<string>();

            using (SqlConnection conn = new SqlConnection(cx))
            {
                string query = @"
                            SELECT S.TipoServicio_460AS
                            FROM PAGO_SERVICIOS_460AS PS
                            INNER JOIN SERVICIOS_460AS S ON PS.CodServicio_460AS = S.CodServicio_460AS
                            WHERE PS.CodPago_460AS = @codPago";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@codPago", codPago);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    servicios.Add(reader["TipoServicio_460AS"].ToString());
                }
            }

            return servicios;
        }
    }
}
