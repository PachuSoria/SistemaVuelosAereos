using _460ASBE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _460ASBLL
{
    public class BLL460AS_Serializar
    {
        public void Serializar(string ruta, List<Cliente_460AS> clientes)
        {
            var serializer = new XmlSerializer(typeof(List<Cliente_460AS>));
            using (var fs = new FileStream(ruta, FileMode.Create))
            {
                serializer.Serialize(fs, clientes);
            }
        }

        public List<Cliente_460AS> Deserializar(string ruta)
        {
            var serializer = new XmlSerializer(typeof(List<Cliente_460AS>));
            using (var fs = new FileStream(ruta, FileMode.Open))
            {
                return (List<Cliente_460AS>)serializer.Deserialize(fs);
            }
        }
    }
}
