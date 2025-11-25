using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace _460ASDAL
{
    public abstract class DAL_Abstract
    {
        public static string cx = string.Empty;
        static DAL_Abstract()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Connection.json");

            if (!File.Exists(path))
                throw new FileNotFoundException($"No se encontró el archivo de configuración: {path}");

            string json = File.ReadAllText(path);

            var datos = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            if (datos != null && datos.ContainsKey("ConnectionString"))
                cx = datos["ConnectionString"];
            else
                throw new Exception("No se encontró la clave 'ConnectionString' en el archivo JSON.");
        }
    }
}
