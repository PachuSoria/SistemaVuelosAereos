using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _460ASServicios.Singleton
{
    public class Session_460AS
    {
        private Usuario_460AS _usuario {  get; set; }
        public Usuario_460AS Usuario
        {
            get
            {
                return _usuario;
            }
        }
        public void Login_460AS(Usuario_460AS usuario)
        {
            _usuario = usuario;
        }
        public void Logout_460AS()
        {
            _usuario = null;
        }
        public bool IsLogged_460AS()
        {
            return _usuario != null;
        }
    }
}
