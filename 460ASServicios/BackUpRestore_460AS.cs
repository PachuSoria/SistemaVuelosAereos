using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASServicios
{
    public class BackUpRestore_460AS
    {
        public bool VerificarRutaBackUp(string ruta)
        {
            return Directory.Exists(Path.GetDirectoryName(ruta));
        }
        public bool VerificarArchivoRestore(string ruta)
        {
            return File.Exists(ruta) && Path.GetExtension(ruta).Equals(".bak", StringComparison.OrdinalIgnoreCase);
        }
    }
}
