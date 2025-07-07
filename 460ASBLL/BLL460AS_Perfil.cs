using _460ASDAL;
using _460ASServicios.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_Perfil
    {
        private DAL460AS_Perfil _dalPerfil = new DAL460AS_Perfil();
        private DAL460AS_Permiso _dalPermiso = new DAL460AS_Permiso();
        private DAL460AS_Familia _dalFamilia = new DAL460AS_Familia();

        public void RegistrarPerfil_460AS(Perfil_460AS perfil)
        {
            if (perfil == null)
            {
                throw new ArgumentNullException(nameof(perfil));
            }
            if (string.IsNullOrWhiteSpace(perfil.Codigo_460AS))
            {
                throw new ArgumentException("El código del perfil es obligatorio");
            }
            if (string.IsNullOrWhiteSpace(perfil.Nombre_460AS))
            {
                throw new ArgumentException("El nombre del perfil es obligatorio");
            }

            if (_dalPerfil.ObtenerPorCodigo_460AS(perfil.Codigo_460AS) != null)
            {
                throw new InvalidOperationException($"Ya existe un perfil con el código '{perfil.Codigo_460AS}'");
            }

            foreach (var hijo in perfil.ObtenerHijos())
            {
                if (hijo is Permiso_460AS permiso)
                {
                    if (_dalPermiso.ObtenerPorCodigo_460AS(permiso.Codigo_460AS) == null)
                    {
                        throw new InvalidOperationException($"El permiso con código '{permiso.Codigo_460AS}' no existe y no puede ser asignado");
                    }
                }
                else if (hijo is Familia_460AS familia)
                {
                    if (_dalFamilia.ObtenerPorCodigo_460AS(familia.Codigo_460AS) == null)
                    {
                        throw new InvalidOperationException($"La familia con código '{familia.Codigo_460AS}' no existe y no puede ser asignada");
                    }
                }
            }

            try
            {
                _dalPerfil.Guardar_460AS(perfil);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el perfil", ex);
            }
        }

        public void ModificarPerfil_460AS(Perfil_460AS perfil)
        {
            if (perfil == null)
            {
                throw new ArgumentNullException(nameof(perfil));
            }
            if (string.IsNullOrWhiteSpace(perfil.Codigo_460AS))
            {
                throw new ArgumentException("El código del perfil es obligatorio para la modificación");
            }
            if (string.IsNullOrWhiteSpace(perfil.Nombre_460AS))
            {
                throw new ArgumentException("El nombre del perfil es obligatorio");
            }

            if (_dalPerfil.ObtenerPorCodigo_460AS(perfil.Codigo_460AS) == null)
            {
                throw new InvalidOperationException($"El perfil con código '{perfil.Codigo_460AS}' no existe y no puede ser modificado");
            }

            foreach (var hijo in perfil.ObtenerHijos())
            {
                if (hijo is Permiso_460AS permiso)
                {
                    if (_dalPermiso.ObtenerPorCodigo_460AS(permiso.Codigo_460AS) == null)
                    {
                        throw new InvalidOperationException($"El permiso con código '{permiso.Codigo_460AS}' no existe y no puede ser asignado");
                    }
                }
                else if (hijo is Familia_460AS familia)
                {
                    if (_dalFamilia.ObtenerPorCodigo_460AS(familia.Codigo_460AS) == null)
                    {
                        throw new InvalidOperationException($"La familia con código '{familia.Codigo_460AS}' no existe y no puede ser asignada");
                    }
                }
            }

            try
            {
                _dalPerfil.Actualizar_460AS(perfil);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el perfil", ex);
            }
        }

        public void EliminarPerfil_460AS(string codPerfil)
        {
            if (string.IsNullOrWhiteSpace(codPerfil))
            {
                throw new ArgumentException("El código del perfil es obligatorio para la eliminacion");
            }

            if (_dalPerfil.ObtenerPorCodigo_460AS(codPerfil) == null)
            {
                throw new InvalidOperationException($"El perfil con código '{codPerfil}' no existe y no puede ser eliminado");
            }

            try
            {
                _dalPerfil.EliminarPorCodigo_460AS(codPerfil);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el perfil", ex);
            }
        }

        public Perfil_460AS ObtenerPerfilPorCodigo(string codigoPerfil)
        {
            if (string.IsNullOrWhiteSpace(codigoPerfil))
            {
                throw new ArgumentException("El código del perfil no puede ser nulo o vacío");
            }

            return _dalPerfil.ObtenerPorCodigo_460AS(codigoPerfil);
        }

        public IList<Perfil_460AS> ObtenerTodosLosPerfiles()
        {
            return _dalPerfil.ObtenerTodos_460AS();
        }
    }
}
