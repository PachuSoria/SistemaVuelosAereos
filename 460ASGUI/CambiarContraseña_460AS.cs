using _460ASBLL;
using _460ASServicios;
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
    public partial class CambiarContraseña_460AS : Form, IIdiomaObserver_460AS
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
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_contraseña_actual");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_nueva_contraseña");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_confirmar_contraseña");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_usuario") + $": {_loginUsuarioActual}";
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("btn_confirmar");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_cancelar");
            checkBox1.Text = IdiomaManager_460AS.Instancia.Traducir("checkbox_mostrar");
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
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_completar_campos"),
                                    IdiomaManager_460AS.Instancia.Traducir("titulo_confirmacion"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (passwordNueva != confirmar)
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_no_confirmar_contraseña"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Usuario_460AS usuarioActual = SessionManager_460AS.Instancia.Usuario;
                if (usuarioActual == null || Hashing_460AS.HashearPasswordSHA256_460AS(passwordActual) != usuarioActual.Password_460AS)
                {
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_contraseña_actual_incorrecta"));
                }
                string passwordHasheada = Hashing_460AS.HashearPasswordSHA256_460AS(passwordNueva);
                if (Hashing_460AS.HashearPasswordSHA256_460AS(passwordNueva) == usuarioActual.Password_460AS)
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_misma_contraseña"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (bllUsuario_460AS.CambiarPassword_460AS(_loginUsuarioActual, passwordNueva))
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_contraseña_cambiada"));
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    bllUsuario_460AS.Logout_460AS();
                    _menuPrincipal.ValidarMenuPrincipal_460AS();
                }
                else
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_error_cambiar_contraseña"));
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
