using _460ASDAL;
using _460ASServicios;
using _460ASServicios.Composite;
using _460ASServicios.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASBLL
{
    public class BLL460AS_Perfil : IIdiomaObserver_460AS
    {
        private DAL460AS_Perfil dalPerfil_460AS;
        private DAL460AS_Familia dalFamilia_460AS;
        private BLL460AS_Evento _eventoBLL;

        public BLL460AS_Perfil()
        {
            dalPerfil_460AS = new DAL460AS_Perfil();
            dalFamilia_460AS = new DAL460AS_Familia();
            _eventoBLL = new BLL460AS_Evento();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        public bool FamiliaAsignadaAPerfil_460AS(string codFamilia)
        {
            var perfiles = dalPerfil_460AS.ObtenerTodos_460AS();

            foreach (var perfil in perfiles)
            {
                var familiasPerfil = dalPerfil_460AS.ObtenerFamiliasDePerfil_460AS(perfil.Codigo_460AS);
                if (familiasPerfil.Any(f => f.Codigo_460AS == codFamilia))
                    return true;
            }
            return false;
        }

        public bool ExisteFamilia_460AS(string codFamilia_460AS)
        {
            List<Familia_460AS> familias = dalFamilia_460AS.ObtenerTodas_460AS();
            return familias.Any(f => f.Codigo_460AS == codFamilia_460AS);
        }

        public bool PerfilEnUso_460AS(string codPerfil_460AS)
        {
            var familias = dalPerfil_460AS.ObtenerFamiliasDePerfil_460AS(codPerfil_460AS);
            if (familias.Count > 0)
                return true;

            var permisos = dalPerfil_460AS.ObtenerPermisosDePerfil_460AS(codPerfil_460AS);
            if (permisos.Count > 0)
                return true;

            return false;
        }

        public void GuardarPerfil_460AS(Perfil_460AS perfil)
        {
            ValidarPerfilUnico_460AS(perfil.Codigo_460AS);
            var bllFamilia = new BLL460AS_Familia();
            var familias = bllFamilia.ObtenerTodas_460AS();

            if (familias.Any(f => f.Codigo_460AS == perfil.Codigo_460AS && f.Nombre_460AS == perfil.Nombre_460AS))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_perfil_igual_codigo_nombre"));

            dalPerfil_460AS.GuardarPerfil_460AS(perfil);
            Evento_460AS ultimo = _eventoBLL.ObtenerUltimo_460AS();
            var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 2, "Perfiles/Familias", $"Perfil creado - Código: {perfil.Codigo_460AS}");
            _eventoBLL.GuardarEvento_460AS(ev);
        }

        public void EliminarPerfil_460AS(Perfil_460AS perfil)
        {
            if (PerfilEnUso_460AS(perfil.Codigo_460AS))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_perfil_en_uso"));

            var familias = ObtenerFamiliasDePerfil_460AS(perfil.Codigo_460AS);
            var permisos = ObtenerPermisosDePerfil_460AS(perfil.Codigo_460AS);

            if (familias.Count > 0 || permisos.Count > 0)
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_perfil_tiene_familias_o_permisos"));

            dalPerfil_460AS.EliminarPerfil_460AS(perfil);
            Evento_460AS ultimo = _eventoBLL.ObtenerUltimo_460AS();
            var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 2, "Perfiles/Familias", $"Perfil eliminado - Código: {perfil.Codigo_460AS}");
            _eventoBLL.GuardarEvento_460AS(ev);
        }

        private void ValidarPerfilUnico_460AS(string codigo)
        {
            var perfiles = dalPerfil_460AS.ObtenerTodos_460AS();
            if (perfiles.Any(p => p.Codigo_460AS == codigo))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_perfil_codigo_repetido").Replace("{codigo}", codigo));
        }

        public void AgregarFamiliaAPerfil_460AS(string codPerfil_460AS, string codFamilia_460AS)
        {
            var perfil = ObtenerTodas_460AS().FirstOrDefault(p => p.Codigo_460AS == codPerfil_460AS);
            if (perfil == null)
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_perfil_no_existe").Replace("{codigo}", codPerfil_460AS));

            var familiaNueva = new BLL460AS_Familia().ObtenerFamiliaPorCodigo_460AS(codFamilia_460AS);
            if (familiaNueva == null)
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_no_existe").Replace("{codigo}", codFamilia_460AS));

            var familiasAsignadas = ObtenerFamiliasDePerfil_460AS(codPerfil_460AS);

            if (familiasAsignadas.Any(f => f.Codigo_460AS == codFamilia_460AS))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_ya_asignada_a_perfil"));

            var bllFamilia = new BLL460AS_Familia();
            foreach (var fam in familiasAsignadas)
            {
                var familiasHijas = bllFamilia.ObtenerFamiliasHijasRecursivas_460AS(fam);
                if (familiasHijas.Any(h => h.Codigo_460AS == codFamilia_460AS))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_incluida_otro_asignacion"));
            }

            var permisosPerfil = ObtenerPermisosDePerfil_460AS(codPerfil_460AS);
            var permisosHeredadosFamilias = new List<Permiso_460AS>();
            foreach (var fam in familiasAsignadas)
            {
                permisosHeredadosFamilias.AddRange(bllFamilia.ObtenerPermisosHeredados_460AS(fam.Codigo_460AS));
            }
            permisosHeredadosFamilias = permisosHeredadosFamilias
                .GroupBy(p => p.Codigo_460AS)
                .Select(g => g.First())
                .ToList();

            var permisosFamiliaNueva = bllFamilia.ObtenerPermisosHeredados_460AS(codFamilia_460AS);
            if (permisosFamiliaNueva == null || permisosFamiliaNueva.Count == 0)
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_sin_permisos"));

            foreach (var permFam in permisosFamiliaNueva)
            {
                if (permisosPerfil.Any(p => p.Codigo_460AS == permFam.Codigo_460AS))
                    throw new Exception(
                        IdiomaManager_460AS.Instancia.Traducir("msg_permiso_directo_asignado_perfil")
                        .Replace("{permiso}", permFam.Nombre_460AS)
                    );
            }

            foreach (var permFam in permisosFamiliaNueva)
            {
                if (permisosHeredadosFamilias.Any(p => p.Codigo_460AS == permFam.Codigo_460AS))
                    throw new Exception(
                        IdiomaManager_460AS.Instancia.Traducir("msg_permiso_heredado_perfil")
                        .Replace("{permiso}", permFam.Nombre_460AS)
                    );
            }

            dalPerfil_460AS.AgregarFamiliaAPerfil_460AS(codPerfil_460AS, codFamilia_460AS);
        }

        public void EliminarFamiliaDePerfil_460AS(string codPerfil_460AS, string codFamilia_460AS)
        {
            var familias = dalPerfil_460AS.ObtenerFamiliasDePerfil_460AS(codPerfil_460AS);
            if (!familias.Any(f => f.Codigo_460AS == codFamilia_460AS))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_no_asignada_a_perfil"));

            dalPerfil_460AS.EliminarFamiliaDePerfil_460AS(codPerfil_460AS, codFamilia_460AS);
        }

        public void EliminarFamiliasDePerfil_460AS(string codPerfil_460AS)
        {
            dalPerfil_460AS.EliminarFamiliasDePerfil_460AS(codPerfil_460AS);
        }

        public void AgregarPermisoAPerfil_460AS(string codPerfil_460AS, string codPermiso_460AS)
        {
            var permisos = dalPerfil_460AS.ObtenerPermisosDePerfil_460AS(codPerfil_460AS);
            if (permisos.Any(p => p.Codigo_460AS == codPermiso_460AS))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_permiso_ya_asignado_perfil"));

            var familias = ObtenerFamiliasDePerfil_460AS(codPerfil_460AS);
            foreach (var fam in familias)
            {
                var permisosDeFam = new BLL460AS_Familia().ObtenerPermisosHeredados_460AS(fam.Codigo_460AS);
                if (permisosDeFam.Any(p => p.Codigo_460AS == codPermiso_460AS))
                    throw new Exception(
                        IdiomaManager_460AS.Instancia.Traducir("msg_permiso_heredado_desde_familia")
                        .Replace("{familia}", fam.Nombre_460AS)
                    );
            }

            dalPerfil_460AS.AgregarPermisoAPerfil_460AS(codPerfil_460AS, codPermiso_460AS);
        }

        public void EliminarPermisoDePerfil_460AS(string codPerfil_460AS, string codPermiso_460AS)
        {
            var permisos = dalPerfil_460AS.ObtenerPermisosDePerfil_460AS(codPerfil_460AS);
            if (!permisos.Any(p => p.Codigo_460AS == codPermiso_460AS))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_permiso_no_asignado_perfil"));

            dalPerfil_460AS.EliminarPermisoDePerfil_460AS(codPerfil_460AS, codPermiso_460AS);
        }

        public void EliminarPermisosDePerfil_460AS(string codPerfil_460AS)
        {
            dalPerfil_460AS.EliminarPermisosDePerfil_460AS(codPerfil_460AS);
        }

        public List<Perfil_460AS> ObtenerTodas_460AS()
        {
            return dalPerfil_460AS.ObtenerTodos_460AS();
        }

        public List<Familia_460AS> ObtenerFamiliasDePerfil_460AS(string codPerfil_460AS)
        {
            return dalPerfil_460AS.ObtenerFamiliasDePerfil_460AS(codPerfil_460AS);
        }

        public List<Permiso_460AS> ObtenerPermisosDePerfil_460AS(string codPerfil_460AS)
        {
            return dalPerfil_460AS.ObtenerPermisosDePerfil_460AS(codPerfil_460AS);
        }

        public List<Permiso_460AS> ObtenerTodosLosPermisosDelPerfil_460AS(string codPerfil)
        {
            var permisosDirectos = ObtenerPermisosDePerfil_460AS(codPerfil);

            var permisosHeredados = new List<Permiso_460AS>();
            var familias = ObtenerFamiliasDePerfil_460AS(codPerfil);
            var bllFamilia = new BLL460AS_Familia();

            foreach (var familia in familias)
            {
                permisosHeredados.AddRange(bllFamilia.ObtenerPermisosHeredados_460AS(familia.Codigo_460AS));
            }

            return permisosDirectos
                .Concat(permisosHeredados)
                .GroupBy(p => p.Codigo_460AS)
                .Select(g => g.First())
                .ToList();
        }

        public void ActualizarIdioma()
        {
            
        }
    }
}
