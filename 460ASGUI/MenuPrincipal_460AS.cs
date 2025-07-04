using _460ASBLL;
using _460ASDAL;
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
    public partial class MenuPrincipal_460AS : Form, IIdiomaObserver_460AS
    {
        BLL460AS_Usuario bllUsuario_460AS;
        private MenuPrincipal_460AS _menu;
        public MenuPrincipal_460AS()
        {
            InitializeComponent();
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
            //menuStrip1.ForeColor = Color.White;
            statusStrip1.BackColor = Color.DimGray;
            statusStrip1.ForeColor = Color.WhiteSmoke;

            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            IdiomaManager_460AS.Instancia.CargarIdioma("español");
            ActualizarIdioma();
            ValidarMenuPrincipal_460AS();

        }

        public void ValidarMenuPrincipal_460AS()
        {
            if (SessionManager_460AS.Instancia.IsLogged_460AS())
            {
                ActualizarEstadoSesion();
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
            string mensaje = IdiomaManager_460AS.Instancia.Traducir("confirmar_cierre_sesion");
            string titulo = IdiomaManager_460AS.Instancia.Traducir("titulo_confirmacion");
            if (MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bllUsuario_460AS.Logout_460AS();
                ValidarMenuPrincipal_460AS();
                ActualizarIdioma();
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

        public void ActualizarIdioma()
        {
            iniciarSesiónToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_usuario_iniciar");
            cambiarContraseñaToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_usuario_cambiar");
            cerrarSesiónToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_usuario_cerrar");
            usuarioToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_usuario");
            administradorToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_administrador");
            gestionarUsuariosToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_gestionar");
            maestroToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_maestro");
            reservasToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_reservas");
            reportesToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_reportes");
            idiomaToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_idiomas");
            ayudaToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_ayuda");
            if (!SessionManager_460AS.Instancia.IsLogged_460AS())
            {
                toolStripStatusLabel1.Text = IdiomaManager_460AS.Instancia.Traducir("sesion_no_iniciada");
                toolStripStatusLabel2.Text = "";
            }
            else
            {
                ActualizarEstadoSesion();
            }
        }

        private void ActualizarEstadoSesion()
        {
            if (SessionManager_460AS.Instancia.IsLogged_460AS())
            {
                string rol = SessionManager_460AS.Instancia.Usuario.Rol_460AS;
                string nombreCompleto = $"{SessionManager_460AS.Instancia.Usuario.Nombre_460AS} {SessionManager_460AS.Instancia.Usuario.Apellido_460AS}";
                string rolTraducido = IdiomaManager_460AS.Instancia.Traducir(rol);
                toolStripStatusLabel1.Text = rolTraducido;
                toolStripStatusLabel2.Text = nombreCompleto;
            }
            else
            {
                toolStripStatusLabel1.Text = IdiomaManager_460AS.Instancia.Traducir("sesion_no_iniciada");
                toolStripStatusLabel2.Text = "";
            }
        }

        private void españolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IdiomaManager_460AS.Instancia.CargarIdioma("español");
        }

        private void inglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IdiomaManager_460AS.Instancia.CargarIdioma("ingles");
        }

        private void registrarReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReservaVuelos_460AS formReservaVuelos = new ReservaVuelos_460AS();
            formReservaVuelos.MdiParent = this;
            formReservaVuelos.Show();
        }

        private void gestionarVuelosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionVuelos_460AS formGestionVuelos = new GestionVuelos_460AS();
            formGestionVuelos.MdiParent = this;
            formGestionVuelos.Show();
        }

        private void gestionarAsientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionAsientos_460AS formGestionAsientos = new GestionAsientos_460AS();
            formGestionAsientos.MdiParent = this;
            formGestionAsientos.Show();
        }

        private void gestionarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionClientes_460AS formGestionClientes = new GestionClientes_460AS();
            formGestionClientes.MdiParent = this;
            formGestionClientes.Show();
        }
    }
}
