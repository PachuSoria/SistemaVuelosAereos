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
    public class BLL460AS_Familia : IIdiomaObserver_460AS
    {
        private DAL460AS_Familia dalFamilia_460AS;
        private DAL460AS_Permiso dalPermiso_460AS;
        private BLL460AS_Evento _eventoBLL;

        public BLL460AS_Familia()
        {
            dalFamilia_460AS = new DAL460AS_Familia();
            dalPermiso_460AS = new DAL460AS_Permiso();
            _eventoBLL = new BLL460AS_Evento();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }


        public bool FamiliaEnUso_460AS(string codFamilia)
        {
            var permisos = dalFamilia_460AS.ObtenerPermisosDeFamilia_460AS(codFamilia);
            if (permisos.Count > 0)
                return true;

            var familiasHijas = dalFamilia_460AS.ObtenerFamiliasHijas_460AS(new Familia_460AS { Codigo_460AS = codFamilia });
            if (familiasHijas.Count > 0)
                return true;

            return false; 
        }

        public void ValidarNoCicloInvertido_460AS(Familia_460AS padre, Familia_460AS hija)
        {
            var descendientesDeHija = ObtenerFamiliasHijasRecursivas_460AS(hija);
            if (descendientesDeHija.Any(f => f.Codigo_460AS == padre.Codigo_460AS))
            {
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ciclo_invertido")
                .Replace("{hija}", hija.Codigo_460AS)
                .Replace("{padre}", padre.Codigo_460AS));
            }
        }

        public void EliminarFamilia_460AS(Familia_460AS familia)
        {
            if (FamiliaEnUso_460AS(familia.Codigo_460AS))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_eliminar_familia_en_uso"));
            var bllPerfil = new BLL460AS_Perfil();
            if (bllPerfil.FamiliaAsignadaAPerfil_460AS(familia.Codigo_460AS))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_eliminar_familia_asignada_perfil"));

            var familiasPadres = dalFamilia_460AS.ObtenerFamiliasPadres_460AS(familia);
            if (familiasPadres.Count > 0)
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_eliminar_familia_asignada_hija"));

            dalFamilia_460AS.EliminarTodasRelacionesFamilia_460AS(familia);

            dalFamilia_460AS.Eliminar_460AS(familia);
            Evento_460AS ultimo = _eventoBLL.ObtenerUltimo_460AS();
            var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 2, "Familias", $"Familia eliminada - Código: {familia.Codigo_460AS}");
            _eventoBLL.GuardarEvento_460AS(ev);
        }

        public void EliminarRelacionFamilia_460AS(Familia_460AS familiaPadre, Familia_460AS familiaHijo)
        {
            if (familiaPadre == null || familiaHijo == null)
                throw new ArgumentNullException(IdiomaManager_460AS.Instancia.Traducir("msg_argumento_nulo_familia_padre_hija"));

            if (familiaPadre.Codigo_460AS == familiaHijo.Codigo_460AS)
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_relacion_con_sigo_misma"));

            dalFamilia_460AS.EliminarRelacionFamilia_460AS(familiaPadre, familiaHijo);
        }

        private void ValidarFamiliaUnica_460AS(string codigo)
        {
            var familias = dalFamilia_460AS.ObtenerTodas_460AS();
            if (familias.Any(f => f.Codigo_460AS == codigo))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_codigo_repetido").Replace("{codigo}", codigo));
        }

        public void GuardarFamilia_460AS(Familia_460AS familia)
        {
            ValidarFamiliaUnica_460AS(familia.Codigo_460AS);

            if (string.IsNullOrWhiteSpace(familia.Nombre_460AS))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_nombre_familia_vacio"));
            if (string.IsNullOrWhiteSpace(familia.Codigo_460AS))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_codigo_familia_vacio"));

            dalFamilia_460AS.GuardarFamilia_460AS(familia);
            Evento_460AS ultimo = _eventoBLL.ObtenerUltimo_460AS();
            var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 2, "Perfiles/Familias", $"Familia creada - Código: {familia.Codigo_460AS}");
            _eventoBLL.GuardarEvento_460AS(ev);
        }

        public void AgregarPermisoAFamilia_460AS(string codFamilia, string codPermiso)
        {
            var familias = dalFamilia_460AS.ObtenerTodas_460AS();
            if (!familias.Any(f => f.Codigo_460AS == codFamilia))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_no_existe").Replace("{codigo}", codFamilia));

            var permisos = dalPermiso_460AS.ObtenerTodos_460AS();
            if (!permisos.Any(p => p.Codigo_460AS == codPermiso))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_permiso_no_existe").Replace("{codigo}", codPermiso));

            var permisosFamilia = dalFamilia_460AS.ObtenerPermisosDeFamilia_460AS(codFamilia);
            if (permisosFamilia.Any(p => p.Codigo_460AS == codPermiso))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_permiso_ya_asignado_a_familia"));

            var padres = ObtenerFamiliasPadre_460AS(new Familia_460AS { Codigo_460AS = codFamilia });
            foreach (var padre in padres)
            {
                var permisosPadre = ObtenerPermisosHeredados_460AS(padre.Codigo_460AS);
                if (permisosPadre.Any(p => p.Codigo_460AS == codPermiso))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_permiso_heredado_no_asignar"));
            }

            dalFamilia_460AS.AgregarPermisoAFamilia_460AS(codFamilia, codPermiso);
        }

        public List<Familia_460AS> ObtenerFamiliasPadre_460AS(Familia_460AS familiaHija)
        {
            if (familiaHija == null || string.IsNullOrWhiteSpace(familiaHija.Codigo_460AS))
                throw new ArgumentException(IdiomaManager_460AS.Instancia.Traducir("msg_familia_hija_invalida"));

            return dalFamilia_460AS.ObtenerFamiliasPadres_460AS(familiaHija);
        }

        public void EliminarPermisoDeFamilia_460AS(string codFamilia, string codPermiso)
        {
            var familias = dalFamilia_460AS.ObtenerTodas_460AS();
            if (!familias.Any(f => f.Codigo_460AS == codFamilia))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_no_existe").Replace("{codigo}", codFamilia));

            var permisos = dalPermiso_460AS.ObtenerTodos_460AS();
            if (!permisos.Any(p => p.Codigo_460AS == codPermiso))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_permiso_no_existe").Replace("{codigo}", codPermiso));

            dalFamilia_460AS.EliminarPermisoDeFamilia_460AS(codFamilia, codPermiso);
        }

        public List<Permiso_460AS> ObtenerPermisosDeFamilia_460AS(string codFamilia)
        {
            return dalFamilia_460AS.ObtenerPermisosDeFamilia_460AS(codFamilia);
        }


        public List<Familia_460AS> ObtenerTodas_460AS()
        {
            return dalFamilia_460AS.ObtenerTodas_460AS();
        }

        public List<Familia_460AS> ObtenerFamiliasHijas_460AS(Familia_460AS familia)
        {
            if (familia == null || string.IsNullOrWhiteSpace(familia.Codigo_460AS))
                throw new ArgumentException(IdiomaManager_460AS.Instancia.Traducir("msg_familia_hija_invalida"));

            return dalFamilia_460AS.ObtenerFamiliasHijas_460AS(familia);
        }

        public Familia_460AS ObtenerFamiliaPorCodigo_460AS(string codigo)
        {
            return dalFamilia_460AS.ObtenerTodas_460AS().FirstOrDefault(f => f.Codigo_460AS == codigo);
        }

        public void EliminarHijo_460AS(Familia_460AS familiaPadre, object hijo)
        {
            if (familiaPadre == null)
                throw new ArgumentNullException(IdiomaManager_460AS.Instancia.Traducir("msg_familia_padre_nulo"));

            if (hijo == null)
                throw new ArgumentNullException(IdiomaManager_460AS.Instancia.Traducir("msg_hijo_nulo"));

            var permisosDirectos = ObtenerPermisosDeFamilia_460AS(familiaPadre.Codigo_460AS);
            var familiasHijasDirectas = ObtenerFamiliasHijas_460AS(familiaPadre);

            if (hijo is Permiso_460AS permiso)
            {
                bool perteneceDirectamente = permisosDirectos.Any(p => p.Codigo_460AS == permiso.Codigo_460AS);
                if (!perteneceDirectamente)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_permiso_no_hijo_directo"));

                EliminarPermisoDeFamilia_460AS(familiaPadre.Codigo_460AS, permiso.Codigo_460AS);
            }
            else if (hijo is Familia_460AS hija)
            {
                bool perteneceDirectamente = familiasHijasDirectas.Any(f => f.Codigo_460AS == hija.Codigo_460AS);
                if (!perteneceDirectamente)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_no_hija_directa"));

                EliminarRelacionFamilia_460AS(familiaPadre, hija);
            }
            else
            {
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_tipo_hijo_no_reconocido"));

            }
        }

        public void AsignarFamiliaHija_460AS(Familia_460AS padre, Familia_460AS hija)
        {
            if (padre.Codigo_460AS == hija.Codigo_460AS)
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_hija_igual_a_su_misma"));

            if (ExisteCicloFamilia_460AS(hija, padre))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_jerarquia_rota"));

            var descendientesDePadre = ObtenerFamiliasHijasRecursivas_460AS(padre);
            if (descendientesDePadre.Any(f => f.Codigo_460AS == hija.Codigo_460AS))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_hija_ya_asignada_indirectamente"));

            var familiasHijasDirectas = ObtenerFamiliasHijas_460AS(padre);
            if (familiasHijasDirectas.Any(f => f.Codigo_460AS == hija.Codigo_460AS))
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_hija_ya_asignada_directamente")
                    .Replace("{hija}", hija.Codigo_460AS).Replace("{padre}", padre.Codigo_460AS));

            var permisosHija = ObtenerPermisosDeFamilia_460AS(hija.Codigo_460AS);
            if (permisosHija == null || permisosHija.Count == 0)
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_hija_sin_permisos"));

            ValidarNoCicloInvertido_460AS(padre, hija);
            ValidarPermisosDuplicados_460AS(padre, hija);
            dalFamilia_460AS.GuardarFamiliaFamilia_460AS(padre, hija);
        }

        private bool ExisteCicloFamilia_460AS(Familia_460AS padre, Familia_460AS hija)
        {
            if (padre.Codigo_460AS == hija.Codigo_460AS)
                return true;

            var familiasHijas = ObtenerFamiliasHijas_460AS(hija);

            foreach (var famHija in familiasHijas)
            {
                if (ExisteCicloFamilia_460AS(padre, famHija))
                    return true;
            }

            return false;
        }

        public List<Permiso_460AS> ObtenerPermisosHeredados_460AS(string codFamilia)
        {
            var resultado = new List<Permiso_460AS>();

            var permisosDirectos = ObtenerPermisosDeFamilia_460AS(codFamilia);
            resultado.AddRange(permisosDirectos);

            var familiaPadre = ObtenerFamiliaPorCodigo_460AS(codFamilia);
            var hijas = ObtenerFamiliasHijas_460AS(familiaPadre);

            foreach (var hija in hijas)
            {
                var permisosHijas = ObtenerPermisosHeredados_460AS(hija.Codigo_460AS);

                foreach (var permiso in permisosHijas)
                {
                    if (!resultado.Any(p => p.Codigo_460AS == permiso.Codigo_460AS))
                        resultado.Add(permiso);
                }
            }

            return resultado;
        }

        public List<Familia_460AS> ObtenerFamiliasHijasRecursivas_460AS(Familia_460AS familia)
        {
            var resultado = new List<Familia_460AS>();
            var hijas = dalFamilia_460AS.ObtenerFamiliasHijas_460AS(familia);
            foreach (var hija in hijas)
            {
                resultado.Add(hija);
                resultado.AddRange(ObtenerFamiliasHijasRecursivas_460AS(hija));
            }
            return resultado;
        }

        public void ValidarPermisosDuplicados_460AS(Familia_460AS padre, Familia_460AS hija)
        {
            var permisosPadre = ObtenerPermisosHeredados_460AS(padre.Codigo_460AS);
            var permisosHija = ObtenerPermisosHeredados_460AS(hija.Codigo_460AS);

            var permisosDuplicados = permisosPadre.Select(p => p.Codigo_460AS)
                                                 .Intersect(permisosHija.Select(p => p.Codigo_460AS))
                                                 .ToList();

            if (permisosDuplicados.Any())
            {
                throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_permiso_duplicado_al_asignar_hija")
                    .Replace("{hija}", hija.Codigo_460AS).Replace("{padre}", padre.Codigo_460AS));
            }
        }

        public void ActualizarIdioma()
        {
            
        }
    }
}
