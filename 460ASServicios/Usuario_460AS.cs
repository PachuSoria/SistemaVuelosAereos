using _460ASServicios.Composite;

namespace _460ASServicios
{
    public class Usuario_460AS
    {
        public string DNI_460AS { get; set; }
        public string Nombre_460AS { get; set; }
        public string Apellido_460AS { get; set; }
        public string Login_460AS { get; set; }
        public string Password_460AS { get; set; }
        public Perfil_460AS Rol_460AS { get; set; }
        public int Telefono_460AS { get; set; }
        public bool Bloqueado_460AS { get; set; }
        public bool Activo_460AS { get; set; }
        public int Contador_460AS { get; set; }
        public DateTime UltimoIntento_460AS { get; set; }
        public string Idioma_460AS { get; set; }
        public Usuario_460AS(string dni_460AS, string nombre_460AS, string apellido_460AS, string login_460AS, string password_460AS, Perfil_460AS rol_460AS, int telefono_460AS, bool bloqueado_460AS, bool activo_460AS, int Contador_460AS, DateTime ultimoIntento_460AS, string idioma_460AS)
        {
            DNI_460AS = dni_460AS;
            this.Nombre_460AS = nombre_460AS;
            this.Apellido_460AS = apellido_460AS;
            this.Login_460AS = login_460AS;
            this.Password_460AS = password_460AS;
            this.Rol_460AS = rol_460AS;
            this.Telefono_460AS = telefono_460AS;
            this.Bloqueado_460AS = bloqueado_460AS;
            this.Activo_460AS = activo_460AS;
            this.Contador_460AS = Contador_460AS;
            this.UltimoIntento_460AS = ultimoIntento_460AS;
            this.Idioma_460AS = idioma_460AS;
        }
    }
}