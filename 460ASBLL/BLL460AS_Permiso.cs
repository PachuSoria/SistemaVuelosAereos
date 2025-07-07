using _460ASDAL;
using _460ASServicios.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_Permiso
    {
        private DAL460AS_Permiso _dalPermiso;

        public BLL460AS_Permiso()
        {
            _dalPermiso = new DAL460AS_Permiso();
        }

        public void RegistrarPermiso_460AS(Permiso_460AS permiso)
        {
            if (permiso == null)
            {
                throw new ArgumentNullException(nameof(permiso));
            }
            if (string.IsNullOrWhiteSpace(permiso.Codigo_460AS))
            {
                throw new ArgumentException("El código del permiso es obligatorio");
            }
            if (string.IsNullOrWhiteSpace(permiso.Nombre_460AS))
            {
                throw new ArgumentException("El nombre del permiso es obligatorio");
            }

            if (_dalPermiso.ObtenerPorCodigo_460AS(permiso.Codigo_460AS) != null)
            {
                throw new InvalidOperationException($"Ya existe un permiso con el código '{permiso.Codigo_460AS}'");
            }

            try
            {
                _dalPermiso.Guardar_460AS(permiso);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el permiso", ex);
            }
        }

        public void ModificarPermiso_460AS(Permiso_460AS permiso)
        {
            if (permiso == null)
            {
                throw new ArgumentNullException(nameof(permiso));
            }
            if (string.IsNullOrWhiteSpace(permiso.Codigo_460AS))
            {
                throw new ArgumentException("El código del permiso es obligatorio para la modificación");
            }
            if (string.IsNullOrWhiteSpace(permiso.Nombre_460AS))
            {
                throw new ArgumentException("El nombre del permiso es obligatorio");
            }

            if (_dalPermiso.ObtenerPorCodigo_460AS(permiso.Codigo_460AS) == null)
            {
                throw new InvalidOperationException($"El permiso con código '{permiso.Codigo_460AS}' no existe y no puede ser modificado");
            }

            try
            {
                _dalPermiso.Actualizar_460AS(permiso);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el permiso", ex);
            }
        }

        public void EliminarPermiso_460AS(string codigoPermiso)
        {
            if (string.IsNullOrWhiteSpace(codigoPermiso))
            {
                throw new ArgumentException("El código del permiso es obligatorio para la eliminación");
            }

            if (_dalPermiso.ObtenerPorCodigo_460AS(codigoPermiso) == null)
            {
                throw new InvalidOperationException($"El permiso con código '{codigoPermiso}' no existe y no puede ser eliminado");
            }

            // Aquí podrías añadir lógica de negocio adicional antes de eliminar, por ejemplo:
            // - ¿Está este permiso asignado a algún perfil o familia?
            //   Esto requeriría que la BLL de Permiso conociera las DAL de Perfil y Familia,
            //   o que haya un servicio de negocio que coordine esto.
            //   Por simplicidad, en este punto, la DAL de Perfil/Familia se encarga de eliminar las FK.
            try
            {
                _dalPermiso.EliminarPorCodigo_460AS(codigoPermiso);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el permiso", ex);
            }
        }

        public Permiso_460AS ObtenerPermisoPorCodigo(string codigoPermiso)
        {
            if (string.IsNullOrWhiteSpace(codigoPermiso))
            {
                throw new ArgumentException("El código del permiso no puede ser nulo o vacío");
            }

            return _dalPermiso.ObtenerPorCodigo_460AS(codigoPermiso);
        }

        public IList<Permiso_460AS> ObtenerTodosLosPermisos()
        {
            return _dalPermiso.ObtenerTodos_460AS();
        }
    }
}
