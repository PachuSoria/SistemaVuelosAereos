using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460ASServicios.Singleton
{
    public class LoginException_460AS : Exception
    {
        public LoginResult_460AS Result;
        public LoginException_460AS(LoginResult_460AS result, string mensaje = "") : base(mensaje)
        {
            Result = result;
        }
    }
}
