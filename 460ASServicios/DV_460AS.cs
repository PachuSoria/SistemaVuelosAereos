using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _460ASServicios
{
    public class DV_460AS
    {
        public int CalcularDVH_460AS(object entidad)
        {
            if (entidad == null) return 0;

            int suma = 0;

            // Recorremos todas las propiedades públicas del objeto
            foreach (PropertyInfo prop in entidad.GetType().GetProperties())
            {
                object valor = prop.GetValue(entidad);
                if (valor != null)
                {
                    // Convertimos a string y luego a bytes ASCII
                    byte[] bytes = Encoding.UTF8.GetBytes(valor.ToString());
                    foreach (byte b in bytes)
                        suma += b;
                }
            }

            return suma;
        }

        // Calcula el DVV (por tabla) sumando todos los DVH de esa tabla
        public int CalcularDVV_460AS(IEnumerable<int> listaDVH)
        {
            if (listaDVH == null || !listaDVH.Any())
                return 0;

            return listaDVH.Sum();
        }

        // Método para recalcular todos los DVH de una lista de entidades
        public Dictionary<object, int> RecalcularDVHTabla_460AS<T>(IEnumerable<T> registros)
        {
            var resultado = new Dictionary<object, int>();

            foreach (var registro in registros)
            {
                int dvh = CalcularDVH_460AS(registro);
                resultado.Add(registro, dvh);
            }

            return resultado;
        }

        // Método para verificar si un registro cambió (DVH actual vs guardado)
        public bool VerificarDVH_460AS(object entidad, int dvhGuardado)
        {
            int dvhActual = CalcularDVH_460AS(entidad);
            return dvhActual == dvhGuardado;
        }

        // Método auxiliar para comparar dos valores de DVH/DVV
        public bool CompararDV_460AS(int valor1, int valor2)
        {
            return valor1 == valor2;
        }
    }
}
