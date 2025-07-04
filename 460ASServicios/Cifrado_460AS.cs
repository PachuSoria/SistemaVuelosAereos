using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _460ASServicios
{
    public static class Cifrado_460AS
    {
        private static readonly string aesKey = "CLAVE32BYTESPARAAES256__"; 
        private static readonly string aesIV = "VECTORINICIALAES";

        public static string EncriptarPasaporteAES_460AS(string nroPasaporte)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(aesKey);
                aes.IV = Encoding.UTF8.GetBytes(aesIV);

                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] inputBytes = Encoding.UTF8.GetBytes(nroPasaporte);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public static string DesencriptarPasaporteAES_460AS(string pasaporteCifrado)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(aesKey);
                aes.IV = Encoding.UTF8.GetBytes(aesIV);

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] inputBytes = Convert.FromBase64String(pasaporteCifrado);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
