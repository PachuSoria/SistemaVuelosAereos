using _460ASBLL;
using _460ASDAL;
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
    public partial class MenuPrincipal_460AS : Form
    {
        BLL460AS_Usuario bllUsuario_460AS;
        private MenuPrincipal_460AS _menu;
        public MenuPrincipal_460AS()
        {
            InitializeComponent();
            ValidarMenuPrincipal_460AS();
            bllUsuario_460AS = new BLL460AS_Usuario();
            //bllUsuario_460AS.GuardarUsuario_460AS(new Usuario_460AS("12345678", "Agustin", "Soria", "123Agustin",
            //    "123Soria", "Admin", 42419672, false, true, 0, DateTime.Now));
            foreach (Control ctl in this.Controls)
            {
                if (ctl is MdiClient)
                {
                    ctl.BackColor = Color.LightSteelBlue;
                    break;
                }
            }
            menuStrip1.BackColor = Color.SlateGray;
            menuStrip1.ForeColor = Color.White;
            statusStrip1.BackColor = Color.DimGray;
            statusStrip1.ForeColor = Color.WhiteSmoke;
        }

        public void ValidarMenuPrincipal_460AS()
        {
            if (SessionManager_460AS.Instancia.IsLogged_460AS())
            {
                this.toolStripStatusLabel1.Text = SessionManager_460AS.Instancia.Usuario.Rol_460AS;
                this.toolStripStatusLabel2.Text = $"{SessionManager_460AS.Instancia.Usuario.Nombre_460AS} {SessionManager_460AS.Instancia.Usuario.Apellido_460AS}";
                menuStrip1.Items[2].Visible = true;
                menuStrip1.Items[3].Visible = true;
                menuStrip1.Items[4].Visible = true;
                menuStrip1.Items[5].Visible = true;
                cambiarContraseñaToolStripMenuItem.Enabled = true;
                cerrarSesiónToolStripMenuItem.Enabled = true;
                if (SessionManager_460AS.Instancia.Usuario.Rol_460AS == "Admin")
                {
                    menuStrip1.Items[1].Visible = true;
                }
            }
            else
            {
                this.toolStripStatusLabel1.Text = "[Sesión no iniciada]";
                this.toolStripStatusLabel2.Text = "";
                menuStrip1.Items[1].Visible = false;
                menuStrip1.Items[2].Visible = false;
                menuStrip1.Items[3].Visible = false;
                menuStrip1.Items[4].Visible = false;
                menuStrip1.Items[5].Visible = false;
                cambiarContraseñaToolStripMenuItem.Enabled = false;
                cerrarSesiónToolStripMenuItem.Enabled = false;
            }
        }

        private void AbrirForm<T>() where T : Form, new()
        {
            Form form = Application.OpenForms.OfType<T>().FirstOrDefault();
            if (form != null) form.BringToFront();
            else
            {
                form = new T();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void CerrarForm()
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form != _menu)
                    form.Close();
            }
        }

        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm<Login_460AS>();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarContraseña_460AS form = new CambiarContraseña_460AS(this);
            form.MdiParent = this;
            form.Show();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar la sesión?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bllUsuario_460AS.Logout_460AS();
                ValidarMenuPrincipal_460AS();
                CerrarForm();
            }
        }

        private void MenuPrincipal_460AS_Load(object sender, EventArgs e)
        {
            ValidarMenuPrincipal_460AS();
        }

        private void gestionarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionUsuarios_460AS gestionUsuariosForm = new GestionUsuarios_460AS();
            gestionUsuariosForm.MdiParent = this;
            gestionUsuariosForm.WindowState = FormWindowState.Maximized;
            menuStrip1.Enabled = false;
            gestionUsuariosForm.Show();

            gestionUsuariosForm.FormClosed += (s, ev) =>
            {
                menuStrip1.Enabled = true;
            };

            gestionUsuariosForm.Deactivate += (s, ev) =>
            {
                if (this.ActiveMdiChild == null)
                {
                    menuStrip1.Enabled = true;
                }
            };
        }

        private void vuelosDisponiblesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
