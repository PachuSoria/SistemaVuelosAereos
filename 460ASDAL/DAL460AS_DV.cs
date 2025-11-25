using _460ASBE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using _460ASServicios;

namespace _460ASDAL
{
    public class DAL460AS_DV : DAL_Abstract
    {
        public DAL460AS_DV()
        {
            
        }

        public string CalcularDVH_460AS(DV_460AS dv)
        {
            BigInteger sumaTotal = 0;
            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = $"SELECT * FROM {dv.NombreTabla_460AS}";
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    object[] datos = new object[rdr.FieldCount];
                    rdr.GetValues(datos);
                    BigInteger sumaParcial = 0;
                    foreach (object o in datos)
                    {
                        string hex = Hashing_460AS.EncriptarSHA256_460AS(o.ToString());
                        BigInteger num = BigInteger.Parse("00" + hex, NumberStyles.HexNumber);
                        sumaParcial += num;
                    }
                    string hex2 = Hashing_460AS.EncriptarSHA256_460AS(sumaParcial.ToString());
                    sumaParcial = BigInteger.Parse("00" + hex2, NumberStyles.HexNumber);
                    sumaTotal += sumaParcial;
                }
            }
            return sumaTotal.ToString("X");
        }

        public string CalcularDVV_460AS(DV_460AS dv)
        {
            BigInteger sumaTotal = 0;
            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = $"SELECT * FROM {dv.NombreTabla_460AS}";
                SqlCommand cmd = new SqlCommand(consulta, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                List<object[]> registros = new List<object[]>();
                while (rdr.Read())
                {
                    object[] fila = new object[rdr.FieldCount];
                    rdr.GetValues(fila);
                    registros.Add(fila);
                }
                rdr.Close();
                if (registros.Count > 0)
                {
                    for (int col = 0; col < registros[0].Length; col++)
                    {
                        BigInteger sumaColumna = 0;

                        foreach (var fila in registros)
                        {
                            string texto = fila[col]?.ToString() ?? "";
                            string hex = Hashing_460AS.EncriptarSHA256_460AS(texto);

                            try
                            {
                                BigInteger valor = BigInteger.Parse("00" + hex, NumberStyles.HexNumber);
                                sumaColumna += valor;
                            }
                            catch
                            {

                            }
                        }
                        string hexCol = Hashing_460AS.EncriptarSHA256_460AS(sumaColumna.ToString());
                        sumaColumna = BigInteger.Parse("00" + hexCol, NumberStyles.HexNumber);

                        sumaTotal += sumaColumna;
                    }
                }
            }
            return sumaTotal.ToString("X");
        }

        public void GuardarDV_460AS(DV_460AS dv)
        {
            using (SqlConnection con = new SqlConnection(cx))
            {
                string consulta = "SELECT COUNT(*) FROM DV_460AS WHERE NombreTabla_460AS = @nombre";
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@nombre", dv.NombreTabla_460AS);
                con.Open();
                int existe = (int)cmd.ExecuteScalar();
                if (existe > 0)
                {
                    string queryUpdate = @"UPDATE DV_460AS SET DVH_460AS = @horizontal, DVV_460AS = @vertical WHERE NombreTabla_460AS = @nombre";

                    cmd = new SqlCommand(queryUpdate, con);
                    cmd.Parameters.AddWithValue("@horizontal", dv.DVH_460AS);
                    cmd.Parameters.AddWithValue("@vertical", dv.DVV_460AS);
                    cmd.Parameters.AddWithValue("@nombre", dv.NombreTabla_460AS);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<DV_460AS> ObtenerTodos_460AS()
        {
            List<DV_460AS> lista = new List<DV_460AS>();

            using (SqlConnection con = new SqlConnection(cx))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM DV_460AS", con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DV_460AS dv = new DV_460AS(reader["NombreTabla_460AS"].ToString(), reader["DVH_460AS"].ToString()
                        , reader["DVV_460AS"].ToString());

                    lista.Add(dv);
                }
            }
            return lista;
        }
    }
}
