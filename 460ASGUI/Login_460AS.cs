using _460ASBLL;
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
    public partial class Login_460AS : Form
    {
        BLL460AS_Usuario bllUsuario_460AS;

        public Login_460AS()
        {
            InitializeComponent();
            bllUsuario_460AS = new BLL460AS_Usuario();
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var respuesta = bllUsuario_460AS.Login_460AS(this.textBox1.Text, this.textBox2.Text);
                MenuPrincipal_460AS menu = (MenuPrincipal_460AS)this.MdiParent;
                menu.ValidarMenuPrincipal_460AS();
                this.Close();
            }
            catch (LoginException_460AS ex)
            {
                switch (ex.Result)
                {
                    case LoginResult_460AS.InvalidUsername:
                        MessageBox.Show("Usuario incorrecto");
                        break;
                    case LoginResult_460AS.InvalidPassword:
                        MessageBox.Show("Contraseña incorrecta");
                        break;
                    case LoginResult_460AS.UserInactive:
                        MessageBox.Show("El usuario está inactivo");
                        break;
                    case LoginResult_460AS.UserBlocked:
                        MessageBox.Show("El usuario está bloqueado");
                        break;
                    case LoginResult_460AS.UserAlreadyLoggedIn:
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

            // Limitar horizontalmente
            if (currentBounds.Left < parentBounds.Left)
                newX = parentBounds.Left;
            if (currentBounds.Right > parentBounds.Right)
                newX = parentBounds.Right - this.Width;

            // Limitar verticalmente
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
    }
}
