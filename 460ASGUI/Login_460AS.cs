using _460ASBLL;
using _460ASServicios.Observer;
using _460ASServicios.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _460ASGUI
{
    public partial class Login_460AS : Form, IIdiomaObserver_460AS
    {
        BLL460AS_Usuario bllUsuario_460AS;

        public Login_460AS()
        {
            InitializeComponent();
            bllUsuario_460AS = new BLL460AS_Usuario();
            textBox2.PasswordChar = '*';
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var respuesta = bllUsuario_460AS.Login_460AS(this.textBox1.Text, this.textBox2.Text);
                IdiomaManager_460AS.Instancia.CargarIdioma(SessionManager_460AS.Instancia.Usuario.Idioma_460AS);
                MenuPrincipal_460AS menu = (MenuPrincipal_460AS)this.MdiParent;
                menu.ValidarMenuPrincipal_460AS();
                this.Close();
            }
            catch (LoginException_460AS ex)
            {
                switch (ex.Result)
                {
                    case LoginResult_460AS.InvalidUsername:
                        MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_usuario_incorrecto"));
                        break;
                    case LoginResult_460AS.InvalidPassword:
                        MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_contraseña_incorrecta"));
                        break;
                    case LoginResult_460AS.UserInactive:
                        MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_usuario_inactivo"));
                        break;
                    case LoginResult_460AS.UserBlocked:
                        MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_usuario_bloqueado"));
                        break;
                    case LoginResult_460AS.UserAlreadyLoggedIn:
                        MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_usuario_logueado"));
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) textBox2.PasswordChar = '\0';
            else textBox2.PasswordChar = '*';
        }

        private void Login_460AS_LocationChanged(object sender, EventArgs e)
        {
            if (this.Owner == null) return;

            var parentBounds = this.Owner.Bounds;
            var currentBounds = this.Bounds;

            int newX = currentBounds.X;
            int newY = currentBounds.Y;

            if (currentBounds.Left < parentBounds.Left)
                newX = parentBounds.Left;
            if (currentBounds.Right > parentBounds.Right)
                newX = parentBounds.Right - this.Width;

            if (currentBounds.Top < parentBounds.Top)
                newY = parentBounds.Top;
            if (currentBounds.Bottom > parentBounds.Bottom)
                newY = parentBounds.Bottom - this.Height;

            this.Location = new Point(newX, newY);
        }

        private void Login_460AS_Load(object sender, EventArgs e)
        {
            this.LocationChanged += Login_460AS_LocationChanged;
        }

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_usuario");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_contraseña");
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_ingresar");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_cancelar");
            checkBox1.Text = IdiomaManager_460AS.Instancia.Traducir("checkbox_mostrar");
        }
    }
}
