using _460ASDAL;
using _460ASServicios;
using _460ASServicios.Singleton;
using Microsoft.VisualBasic.ApplicationServices;
using _460ASServicios.Composite;

namespace _460ASBLL
{
    public class BLL460AS_Usuario
    {
        private DAL460AS_Usuario _usuarioDAL;
        private DAL460AS_Perfil _perfilDAL;
        private BLL460AS_Evento _eventoBLL;
        public BLL460AS_Usuario()
        {
            _usuarioDAL = new DAL460AS_Usuario();
            _perfilDAL = new DAL460AS_Perfil();
            _eventoBLL = new BLL460AS_Evento();
        }

        public void GuardarUsuario_460AS(Usuario_460AS usuario)
        {
            usuario.Password_460AS = Hashing_460AS.HashearPasswordSHA256_460AS(usuario.Password_460AS);
            _usuarioDAL.GuardarUsuario_460AS(usuario);
        }

        public LoginResult_460AS Login_460AS(string login, string password)
        {
            Evento_460AS ultimo = _eventoBLL.ObtenerUltimo_460AS();
            if (SessionManager_460AS.Instancia.IsLogged_460AS()) throw new LoginException_460AS(LoginResult_460AS.UserAlreadyLoggedIn);
            var user = _usuarioDAL.ObtenerUsuarios_460AS().Where(u => u.Login_460AS.Equals(login)).FirstOrDefault();
            if (user == null)
            {
                var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 2, "Usuarios", "Intento de login fallido");
                _eventoBLL.GuardarEvento_460AS(ev);
                throw new LoginException_460AS(LoginResult_460AS.InvalidUsername);
            }          
            if (user.Bloqueado_460AS == true)
            {
                TimeSpan tiempoTranscurrido = DateTime.Now - user.UltimoIntento_460AS;
                double horasTranscurridas = tiempoTranscurrido.TotalHours;
                if (horasTranscurridas >= 4)
                {
                    user.Bloqueado_460AS = false;
                    user.Contador_460AS = 0;
                    _usuarioDAL.ActualizarUsuario_460AS(user);
                }
                else
                {
                    string mensaje = "";
                    double horasRestantes = 4 - horasTranscurridas;
                    if (horasRestantes < 1) mensaje = $"Intente de nuevo en {(int)(horasRestantes * 60)} minutos";
                    else mensaje = $"Intente de nuevo en {horasRestantes:F1} horas";
                    var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 3, "Usuarios", "Intento de login fallido: usuario bloqueado");
                    _eventoBLL.GuardarEvento_460AS(ev);
                    throw new LoginException_460AS(LoginResult_460AS.UserBlocked, mensaje);
                }
            }
            if (user.Activo_460AS == false)
            {
                var ev = Evento_460AS.GenerarEvento_460AS(ultimo, 2, "Usuarios", "Intento de login fallido: usuario inactivo");
                _eventoBLL.GuardarEvento_460AS(ev);
                throw new LoginException_460AS(LoginResult_460AS.UserInactive);
            }
            

            if (!Hashing_460AS.HashearPasswordSHA256_460AS(password).Equals(user.Password_460AS))
            {
                user.Contador_460AS++;
                user.UltimoIntento_460AS = DateTime.Now;
                _usuarioDAL.ActualizarUsuario_460AS(user);
                if (user.Contador_460AS == 3)
                {
                    user.Bloqueado_460AS = true;
                    _usuarioDAL.ActualizarUsuario_460AS(user);
                    var evBloq = Evento_460AS.GenerarEvento_460AS(ultimo, 3, "Usuarios", "Usuario bloqueado por intentos fallidos");
                    _eventoBLL.GuardarEvento_460AS(evBloq);
                    throw new LoginException_460AS(LoginResult_460AS.UserBlocked);
                }
                var evFallido = Evento_460AS.GenerarEvento_460AS(ultimo, 2, "Usuarios", "Intento de login fallido: contraseña incorrecta");
                _eventoBLL.GuardarEvento_460AS(evFallido);
                throw new LoginException_460AS(LoginResult_460AS.InvalidPassword);
            }
            else
            {
                user.Contador_460AS = 0;
                _usuarioDAL.ActualizarUsuario_460AS(user);
                user.Rol_460AS = _perfilDAL.ObtenerPorCodigo_460AS(user.Rol_460AS.Codigo_460AS);
                SessionManager_460AS.Instancia.Login_460AS(user);
                var evOk = Evento_460AS.GenerarEvento_460AS(ultimo, 1, "Usuarios", "Login exitoso");
                _eventoBLL.GuardarEvento_460AS(evOk);
                return LoginResult_460AS.ValidUser;
            }
        }

        public void Logout_460AS()
        {
            if (!SessionManager_460AS.Instancia.IsLogged_460AS())
            {
                throw new Exception("No hay sesion iniciada");
            }
            string usuarioActual = SessionManager_460AS.Instancia.Usuario.Login_460AS;
            SessionManager_460AS.Instancia.Logout_460AS();;
            var ev = Evento_460AS.GenerarEvento_460AS(_eventoBLL.ObtenerUltimo_460AS(), 1, "Usuarios", "Logout");
            ev.Usuario_460AS = usuarioActual;
            _eventoBLL.GuardarEvento_460AS(ev);
        }

        public void Actualizar_460AS(Usuario_460AS usuario)
        {
            _usuarioDAL.ActualizarUsuario_460AS(usuario);
        }

        public void Activar_460AS(Usuario_460AS usuario)
        {
            _usuarioDAL.ActivarUsuario_460AS(usuario);
            var ev = Evento_460AS.GenerarEvento_460AS(_eventoBLL.ObtenerUltimo_460AS(), 2, "Usuarios", "Usuario activado");
            _eventoBLL.GuardarEvento_460AS(ev);
        }

        public void Desactivar_460AS(Usuario_460AS usuario)
        {
            _usuarioDAL.DesactivarUsuario_460AS(usuario);
            var ev = Evento_460AS.GenerarEvento_460AS(_eventoBLL.ObtenerUltimo_460AS(), 2, "Usuarios", "Usuario desactivado");
            _eventoBLL.GuardarEvento_460AS(ev);

        }

        public void Desbloquear_460AS(Usuario_460AS usuario)
        {
            _usuarioDAL.DesbloquearUsuario_460AS(usuario);
            usuario.Bloqueado_460AS = false;
            string dni = usuario.DNI_460AS.ToString();
            string apellido = usuario.Apellido_460AS.ToString();
            string passwordOriginal = dni.Substring(0, 3) + apellido;
            usuario.Password_460AS = Hashing_460AS.HashearPasswordSHA256_460AS(passwordOriginal);
            usuario.Contador_460AS = 0;
            _usuarioDAL.ActualizarUsuario_460AS(usuario);
            var ev = Evento_460AS.GenerarEvento_460AS(_eventoBLL.ObtenerUltimo_460AS(), 3, "Usuarios", "Usuario desbloqueado");
            _eventoBLL.GuardarEvento_460AS(ev);
        }

        public List<Usuario_460AS> ObtenerUsuarios460AS()
        {
            List<Usuario_460AS> usuarios = new List<Usuario_460AS>();
            foreach (var item in _usuarioDAL.ObtenerUsuarios_460AS())
            {
                foreach (var i in _perfilDAL.ObtenerTodos_460AS())
                {
                    if (i.Codigo_460AS == item.Rol_460AS.Codigo_460AS)
                    {
                        item.Rol_460AS = i;
                        usuarios.Add(item);
                    }
                }
            }
            return usuarios;
        }

        public bool CambiarPassword_460AS(string loginUsuario, string nuevaPassword)
        {
            string nuevaPasswordHasheada = Hashing_460AS.HashearPasswordSHA256_460AS(nuevaPassword);
            _usuarioDAL.ActualizarPasswordUsuario_460AS(loginUsuario, nuevaPasswordHasheada);
            var ev = Evento_460AS.GenerarEvento_460AS(_eventoBLL.ObtenerUltimo_460AS(), 2, "Usuarios", "Cambio de contraseña");
            _eventoBLL.GuardarEvento_460AS(ev);
            return true;
        }
    }
}
