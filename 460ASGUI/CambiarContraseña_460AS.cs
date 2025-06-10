using _460ASBLL;
using _460ASServicios;
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
    public partial class CambiarContraseña_460AS : Form
    {
        BLL460AS_Usuario bllUsuario_460AS;
        private string _loginUsuarioActual;
        private MenuPrincipal_460AS _menuPrincipal;
        public CambiarContraseña_460AS(MenuPrincipal_460AS menuPrincipal)
        {
            InitializeComponent();
            bllUsuario_460AS = new BLL460AS_Usuario();
            _loginUsuarioActual = SessionManager_460AS.Instancia.Usuario.Login_460AS;
            _menuPrincipal = menuPrincipal;
            textBox1.PasswordChar = '*';
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
            label4.Text = $"Username: {_loginUsuarioActual}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string passwordActual = textBox1.Text;
                string passwordNueva = textBox2.Text;
                string confirmar = textBox3.Text;

                if (string.IsNullOrWhiteSpace(passwordActual) || string.IsNullOrEmpty(passwordNueva) || string.IsNullOrEmpty(confirmar))
                {
                    MessageBox.Show("Debe completar todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (passwordNueva != confirmar)
                {
                    MessageBox.Show("No se pudo confirmar la contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Usuario_460AS usuarioActual = SessionManager_460AS.Instancia.Usuario;
                if (usuarioActual == null || Hashing_460AS.HashearPasswordSHA256_460AS(passwordActual) != usuarioActual.Password_460AS)
                {
                    throw new Exception("Contraseña actual incorrecta");
                }
                string passwordHasheada = Hashing_460AS.HashearPasswordSHA256_460AS(passwordNueva);
                if (Hashing_460AS.HashearPasswordSHA256_460AS(passwordNueva) == usuarioActual.Password_460AS)
                {
                    MessageBox.Show("No puede ingresar la contraseña actual", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (bllUsuario_460AS.CambiarPassword_460AS(_loginUsuarioActual, passwordNueva))
                {
                    MessageBox.Show("Se cambió la contraseña correctamente");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    bllUsuario_460AS.Logout_460AS();
                    _menuPrincipal.ValidarMenuPrincipal_460AS();
                }
                else
                {
                    MessageBox.Show("Error al cambiar la contraseña. Intente nuevamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.PasswordChar = '\0';
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox1.PasswordChar = '*';
                textBox2.PasswordChar = '*';
                textBox3.PasswordChar = '*';
            }
        }
    }
}
