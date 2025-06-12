using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _460ASServicios.Observer
{
    public class IdiomaManager_460AS : IIdiomaSubject_460AS
    {
        private static IdiomaManager_460AS instancia;
        private Dictionary<string, string> traducciones;
        private List<IIdiomaObserver_460AS> observers = new List<IIdiomaObserver_460AS>();
        public string IdiomaActual {  get; private set; }
        private IdiomaManager_460AS() { }

        public static IdiomaManager_460AS Instancia
        {
            get
            {
                if (instancia == null) instancia = new IdiomaManager_460AS();
                return instancia;
            }
        }

        public void CargarIdioma(string idioma)
        {
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Observer", "Idiomas", idioma + ".json");
            if (!File.Exists(ruta)) throw new Exception($"El archivo de idioma {idioma}.json no fue encontrado");
            string json = File.ReadAllText(ruta);
            traducciones = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            IdiomaActual = idioma;
            NotificarObservers();
        }

        public string Traducir(string clave)
        {
            if (traducciones != null && traducciones.ContainsKey(clave)) return traducciones[clave];
            return $"[{clave}]";
        }

        public void RegistrarObserver(IIdiomaObserver_460AS observer)
        {
            if (!observers.Contains(observer)) observers.Add(observer);
        }

        public void RemoverObserver(IIdiomaObserver_460AS observer)
        {
            if (observers.Contains(observer)) observers.Remove(observer);
        }

        public void NotificarObservers()
        {
            foreach (var observer in observers) observer.ActualizarIdioma();
        }
    }
}
