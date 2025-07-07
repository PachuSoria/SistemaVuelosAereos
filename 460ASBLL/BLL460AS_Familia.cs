using _460ASDAL;
using _460ASServicios.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_Familia
    {
        private DAL460AS_Familia _dalFamilia;
        private DAL460AS_Permiso _dalPermiso; 

        public BLL460AS_Familia()
        {
            _dalFamilia = new DAL460AS_Familia();
            _dalPermiso = new DAL460AS_Permiso();
        }

        public void RegistrarFamilia(Familia_460AS familia)
        {
            if (familia == null)
            {
                throw new ArgumentNullException(nameof(familia));
            }
            if (string.IsNullOrWhiteSpace(familia.Codigo_460AS))
            {
                throw new ArgumentException("El código de la familia es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(familia.Nombre_460AS))
            {
                throw new ArgumentException("El nombre de la familia es obligatorio.");
            }

            if (_dalFamilia.ObtenerPorCodigo_460AS(familia.Codigo_460AS) != null)
            {
                throw new InvalidOperationException($"Ya existe una familia con el código '{familia.Codigo_460AS}'.");
            }

            foreach (var hijo in familia.ObtenerHijos())
            {
                if (hijo is Permiso_460AS permiso)
                {
                    if (_dalPermiso.ObtenerPorCodigo_460AS(permiso.Codigo_460AS) == null)
                    {
                        throw new InvalidOperationException($"El permiso con código '{permiso.Codigo_460AS}' no existe y no puede ser asignado a la familia.");
                    }
                }
                else if (hijo is Familia_460AS familiaHija) // Familia anidada
                {
                    // Para familias anidadas:
                    // 1. La familia hija debe existir en la base de datos (o se debería registrar primero).
                    // 2. Se deben evitar ciclos recursivos (una familia no puede contenerse a sí misma directa o indirectamente).
                    // La DAL ya maneja la prevención de ciclos en la carga con 'loadedComponents'.
                    // Aquí solo validamos que la familia hija, si ya existe en la DB, pueda ser asignada.
                    // Si la familiaHija aún no existe en la DB, se esperaría que se registre por separado.
                    if (familiaHija.Codigo_460AS == familia.Codigo_460AS)
                    {
                        throw new InvalidOperationException("Una familia no puede contenerse a sí misma");
                    }
                    if (_dalFamilia.ObtenerPorCodigo_460AS(familiaHija.Codigo_460AS) == null)
                    {
                        throw new InvalidOperationException($"La familia hija con código '{familiaHija.Codigo_460AS}' no existe y no puede ser asignada.");
                    }
                }
            }

            try
            {
                _dalFamilia.Guardar_460AS(familia);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la familia.", ex);
            }
        }

        public void ModificarFamilia(Familia_460AS familia)
        {
            if (familia == null)
            {
                throw new ArgumentNullException(nameof(familia));
            }
            if (string.IsNullOrWhiteSpace(familia.Codigo_460AS))
            {
                throw new ArgumentException("El código de la familia es obligatorio para la modificación.");
            }
            if (string.IsNullOrWhiteSpace(familia.Nombre_460AS))
            {
                throw new ArgumentException("El nombre de la familia es obligatorio.");
            }

            if (_dalFamilia.ObtenerPorCodigo_460AS(familia.Codigo_460AS) == null)
            {
                throw new InvalidOperationException($"La familia con código '{familia.Codigo_460AS}' no existe y no puede ser modificada.");
            }

            foreach (var hijo in familia.ObtenerHijos())
            {
                if (hijo is Permiso_460AS permiso)
                {
                    if (_dalPermiso.ObtenerPorCodigo_460AS(permiso.Codigo_460AS) == null)
                    {
                        throw new InvalidOperationException($"El permiso con código '{permiso.Codigo_460AS}' no existe y no puede ser asignado a la familia.");
                    }
                }
                else if (hijo is Familia_460AS familiaHija)
                {
                    if (familiaHija.Codigo_460AS == familia.Codigo_460AS)
                    {
                        throw new InvalidOperationException("Una familia no puede contenerse a sí misma");
                    }
                    if (_dalFamilia.ObtenerPorCodigo_460AS(familiaHija.Codigo_460AS) == null)
                    {
                        throw new InvalidOperationException($"La familia hija con código '{familiaHija.Codigo_460AS}' no existe y no puede ser asignada.");
                    }
                }
            }

            try
            {
                _dalFamilia.Actualizar_460AS(familia);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la familia.", ex);
            }
        }

        public void EliminarFamilia(string codigoFamilia)
        {
            if (string.IsNullOrWhiteSpace(codigoFamilia))
            {
                throw new ArgumentException("El código de la familia es obligatorio para la eliminación.");
            }

            if (_dalFamilia.ObtenerPorCodigo_460AS(codigoFamilia) == null)
            {
                throw new InvalidOperationException($"La familia con código '{codigoFamilia}' no existe y no puede ser eliminada.");
            }

            // Reglas de negocio adicionales para eliminación de familias:
            // - ¿Puede una familia ser eliminada si es "padre" de otras familias?
            //   La DAL eliminará las relaciones FAMILIA_FAMILIA_460AS, pero la familia hija seguirá existiendo.
            // - ¿Puede una familia ser eliminada si es "hija" de otra familia?
            //   La DAL eliminará la referencia de la familia padre.
            // - ¿Puede una familia ser eliminada si está asignada a un perfil (PERFIL_FAMILIA_460AS)?
            //   Si un perfil tiene esta familia, la eliminación aquí fallaría si la FK está activa y no se gestiona con ON DELETE CASCADE.
            //   Sería prudente verificar si la familia está asociada a algún perfil antes de eliminarla.
            //   Esto requeriría una instancia de DAL460AS_Perfil y un método en ella para verificar la relación.
            //   Ej: if (new DAL460AS_Perfil().FamiliaAsignada(codigoFamilia)) { throw ... }

            try
            {
                _dalFamilia.EliminarPorCodigo_460AS(codigoFamilia);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la familia.", ex);
            }
        }

        public Familia_460AS ObtenerFamiliaPorCodigo(string codigoFamilia)
        {
            if (string.IsNullOrWhiteSpace(codigoFamilia))
            {
                throw new ArgumentException("El código de la familia no puede ser nulo o vacío.");
            }

            return _dalFamilia.ObtenerPorCodigo_460AS(codigoFamilia);
        }

        public IList<Familia_460AS> ObtenerTodasLasFamilias()
        {
            return _dalFamilia.ObtenerTodos_460AS();
        }
    }
}
