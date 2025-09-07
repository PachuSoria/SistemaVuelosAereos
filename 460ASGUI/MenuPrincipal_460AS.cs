using _460ASBLL;
using _460ASDAL;
using _460ASServicios;
using _460ASServicios.Composite;
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
using System.Media;

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
            //var rol = new Perfil_460AS("SUPERADMIN", "SuperAdmin");
            //bllUsuario_460AS.GuardarUsuario_460AS(new Usuario_460AS("12345678", "Agustin", "Soria", "123Agustin",
            //    "123Soria", rol, 42419672, false, true, 0, DateTime.Now, "Español"));
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
            usuarioToolStripMenuItem.Visible = true;
            idiomaToolStripMenuItem.Visible = true;
            ayudaToolStripMenuItem.Visible = true;

            if (SessionManager_460AS.Instancia.IsLogged_460AS())
            {
                ActualizarEstadoSesion();
                cambiarContraseñaToolStripMenuItem.Enabled = true;
                cerrarSesiónToolStripMenuItem.Enabled = true;

                var perfilActual = SessionManager_460AS.Instancia.Usuario.Rol_460AS;

                if (perfilActual.Codigo_460AS == "SUPERADMIN")
                {
                    administradorToolStripMenuItem.Visible = true;
                    maestroToolStripMenuItem.Visible = true;
                    reservasToolStripMenuItem.Visible = true;
                    reportesToolStripMenuItem.Visible = true;
                }
                else
                {
                    administradorToolStripMenuItem.Visible = false;
                    maestroToolStripMenuItem.Visible = false;
                    reservasToolStripMenuItem.Visible = false;
                    reportesToolStripMenuItem.Visible = false;

                    ValidarMenuPorPerfil_460AS();
                }
            }
            else
            {
                cambiarContraseñaToolStripMenuItem.Enabled = false;
                cerrarSesiónToolStripMenuItem.Enabled = false;

                administradorToolStripMenuItem.Visible = false;
                maestroToolStripMenuItem.Visible = false;
                reservasToolStripMenuItem.Visible = false;
                reportesToolStripMenuItem.Visible = false;

                toolStripStatusLabel2.Text = "";
            }
        }

        private void ValidarMenuPorPerfil_460AS()
        {
            if (!SessionManager_460AS.Instancia.IsLogged_460AS())
                return;

            var perfilActual = SessionManager_460AS.Instancia.Usuario.Rol_460AS;
            var bllPerfil = new BLL460AS_Perfil();

            var permisos = bllPerfil.ObtenerTodosLosPermisosDelPerfil_460AS(perfilActual.Codigo_460AS);

            if (permisos.Any(p => p.Nombre_460AS == "Gestionar Perfiles"))
            {
                administradorToolStripMenuItem.Visible = true;
                gestionarUsuariosToolStripMenuItem.Visible = true;
                gestionarPerfilesToolStripMenuItem.Visible = true;
                gestionarFamiliasToolStripMenuItem.Visible = true;
            }
            else
            {
                administradorToolStripMenuItem.Visible = false;
            }

            bool tieneGestionarVuelos = permisos.Any(p => p.Nombre_460AS == "Gestionar Vuelos");
            bool tieneGestionarClientes = permisos.Any(p => p.Nombre_460AS == "Gestionar Clientes");

            if (tieneGestionarVuelos || tieneGestionarClientes)
            {
                maestroToolStripMenuItem.Visible = true;
                gestionarVuelosToolStripMenuItem.Visible = tieneGestionarVuelos;
                gestionarAsientosToolStripMenuItem.Visible = tieneGestionarVuelos;
                gestionarClientesToolStripMenuItem.Visible = tieneGestionarClientes;
            }
            else
            {
                maestroToolStripMenuItem.Visible = false;
            }

            if (permisos.Any(p => p.Nombre_460AS == "Registrar Reserva"))
            {
                reservasToolStripMenuItem.Visible = true;
                registrarReservaToolStripMenuItem.Visible = true;
            }
            else
            {
                reservasToolStripMenuItem.Visible = false;
            }

            if (permisos.Any(p => p.Nombre_460AS == "Gestionar Reportes"))
            {
                reportesToolStripMenuItem.Visible = true;
                verComprobantesToolStripMenuItem.Visible = true;
            }
            else
            {
                reportesToolStripMenuItem.Visible = false;
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
            SoundPlayer player = new SoundPlayer(Properties.Resources.clash_royale_prince_charge);
            player.Play();
            string mensaje = IdiomaManager_460AS.Instancia.Traducir("msg_confirmar_cierre_sesion");
            string titulo = IdiomaManager_460AS.Instancia.Traducir("msg_titulo_confirmacion");
            if (MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bllUsuario_460AS.Logout_460AS();

                IdiomaManager_460AS.Instancia.CargarIdioma("español");

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
            gestionarVuelosToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_abm_vuelos");
            gestionarAsientosToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_abm_asientos");
            gestionarClientesToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_abm_clientes");
            registrarReservaToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_registrar_reserva");
            españolToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_español");
            inglesToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_ingles");
            gestionarPerfilesToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_perfiles");
            gestionarFamiliasToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_familias");
            verComprobantesToolStripMenuItem.Text = IdiomaManager_460AS.Instancia.Traducir("menu_comprobantes");
            if (!SessionManager_460AS.Instancia.IsLogged_460AS())
            {
                toolStripStatusLabel1.Text = IdiomaManager_460AS.Instancia.Traducir("Sesion_no_iniciada");
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
                string rol = SessionManager_460AS.Instancia.Usuario.Rol_460AS?.Nombre_460AS;

                if (!string.IsNullOrEmpty(rol))
                {
                    string rolTraducido = IdiomaManager_460AS.Instancia.Traducir(rol);
                    toolStripStatusLabel1.Text = rolTraducido;
                }
                else
                {
                    toolStripStatusLabel1.Text = "[Rol no definido]";
                }

                string nombreCompleto = $"{SessionManager_460AS.Instancia.Usuario.Nombre_460AS} {SessionManager_460AS.Instancia.Usuario.Apellido_460AS}";
                toolStripStatusLabel2.Text = nombreCompleto;
            }
            else
            {
                toolStripStatusLabel1.Text = IdiomaManager_460AS.Instancia.Traducir("Sesion_no_iniciada");
                toolStripStatusLabel2.Text = "";
            }
        }

        private void españolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IdiomaManager_460AS.Instancia.CargarIdioma("español");
            if (SessionManager_460AS.Instancia.IsLogged_460AS())
            {
                var usuario = SessionManager_460AS.Instancia.Usuario;
                usuario.Idioma_460AS = "Español";
                bllUsuario_460AS.Actualizar_460AS(usuario);
            }
        }

        private void inglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IdiomaManager_460AS.Instancia.CargarIdioma("ingles");
            if (SessionManager_460AS.Instancia.IsLogged_460AS())
            {
                var usuario = SessionManager_460AS.Instancia.Usuario;
                usuario.Idioma_460AS = "Ingles";
                bllUsuario_460AS.Actualizar_460AS(usuario);
            }
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

        private void gestionarPerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionPerfiles_460AS formGestionPerfiles = new GestionPerfiles_460AS();
            formGestionPerfiles.MdiParent = this;
            formGestionPerfiles.Show();
        }

        private void gestionarFamiliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionFamilias_460AS formGestionFamilias = new GestionFamilias_460AS();
            formGestionFamilias.MdiParent = this;
            formGestionFamilias.Show();
        }

        private void verComprobantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionReportes_460AS formGestionReportes = new GestionReportes_460AS();
            formGestionReportes.MdiParent = this;
            formGestionReportes.Show();
        }

        private void backupYRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackUpRestore_460AS formBackUpRestore = new BackUpRestore_460AS();
            formBackUpRestore.MdiParent = this;
            formBackUpRestore.Show();
        }
    }
}
