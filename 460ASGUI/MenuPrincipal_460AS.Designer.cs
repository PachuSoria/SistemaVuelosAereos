namespace _460ASGUI
{
    partial class MenuPrincipal_460AS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            usuarioToolStripMenuItem = new ToolStripMenuItem();
            iniciarSesiónToolStripMenuItem = new ToolStripMenuItem();
            cambiarContraseñaToolStripMenuItem = new ToolStripMenuItem();
            cerrarSesiónToolStripMenuItem = new ToolStripMenuItem();
            administradorToolStripMenuItem = new ToolStripMenuItem();
            gestionarUsuariosToolStripMenuItem = new ToolStripMenuItem();
            maestroToolStripMenuItem = new ToolStripMenuItem();
            gestionarVuelosToolStripMenuItem = new ToolStripMenuItem();
            gestionarAsientosToolStripMenuItem = new ToolStripMenuItem();
            gestionarClientesToolStripMenuItem = new ToolStripMenuItem();
            reservasToolStripMenuItem = new ToolStripMenuItem();
            registrarReservaToolStripMenuItem = new ToolStripMenuItem();
            reportesToolStripMenuItem = new ToolStripMenuItem();
            idiomaToolStripMenuItem = new ToolStripMenuItem();
            españolToolStripMenuItem = new ToolStripMenuItem();
            inglesToolStripMenuItem = new ToolStripMenuItem();
            ayudaToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            gestionarPerfilesToolStripMenuItem = new ToolStripMenuItem();
            gestionarFamiliasToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Menu;
            menuStrip1.Items.AddRange(new ToolStripItem[] { usuarioToolStripMenuItem, administradorToolStripMenuItem, maestroToolStripMenuItem, reservasToolStripMenuItem, reportesToolStripMenuItem, idiomaToolStripMenuItem, ayudaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1184, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // usuarioToolStripMenuItem
            // 
            usuarioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { iniciarSesiónToolStripMenuItem, cambiarContraseñaToolStripMenuItem, cerrarSesiónToolStripMenuItem });
            usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            usuarioToolStripMenuItem.Size = new Size(59, 20);
            usuarioToolStripMenuItem.Text = "Usuario";
            // 
            // iniciarSesiónToolStripMenuItem
            // 
            iniciarSesiónToolStripMenuItem.Name = "iniciarSesiónToolStripMenuItem";
            iniciarSesiónToolStripMenuItem.Size = new Size(180, 22);
            iniciarSesiónToolStripMenuItem.Text = "Iniciar sesión";
            iniciarSesiónToolStripMenuItem.Click += iniciarSesiónToolStripMenuItem_Click;
            // 
            // cambiarContraseñaToolStripMenuItem
            // 
            cambiarContraseñaToolStripMenuItem.Name = "cambiarContraseñaToolStripMenuItem";
            cambiarContraseñaToolStripMenuItem.Size = new Size(180, 22);
            cambiarContraseñaToolStripMenuItem.Text = "Cambiar contraseña";
            cambiarContraseñaToolStripMenuItem.Click += cambiarContraseñaToolStripMenuItem_Click;
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            cerrarSesiónToolStripMenuItem.Size = new Size(180, 22);
            cerrarSesiónToolStripMenuItem.Text = "Cerrar sesión";
            cerrarSesiónToolStripMenuItem.Click += cerrarSesiónToolStripMenuItem_Click;
            // 
            // administradorToolStripMenuItem
            // 
            administradorToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gestionarUsuariosToolStripMenuItem, gestionarPerfilesToolStripMenuItem, gestionarFamiliasToolStripMenuItem });
            administradorToolStripMenuItem.Name = "administradorToolStripMenuItem";
            administradorToolStripMenuItem.Size = new Size(95, 20);
            administradorToolStripMenuItem.Text = "Administrador";
            // 
            // gestionarUsuariosToolStripMenuItem
            // 
            gestionarUsuariosToolStripMenuItem.Name = "gestionarUsuariosToolStripMenuItem";
            gestionarUsuariosToolStripMenuItem.Size = new Size(180, 22);
            gestionarUsuariosToolStripMenuItem.Text = "Gestionar usuarios";
            gestionarUsuariosToolStripMenuItem.Click += gestionarUsuariosToolStripMenuItem_Click;
            // 
            // maestroToolStripMenuItem
            // 
            maestroToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gestionarVuelosToolStripMenuItem, gestionarAsientosToolStripMenuItem, gestionarClientesToolStripMenuItem });
            maestroToolStripMenuItem.Name = "maestroToolStripMenuItem";
            maestroToolStripMenuItem.Size = new Size(62, 20);
            maestroToolStripMenuItem.Text = "Maestro";
            // 
            // gestionarVuelosToolStripMenuItem
            // 
            gestionarVuelosToolStripMenuItem.Name = "gestionarVuelosToolStripMenuItem";
            gestionarVuelosToolStripMenuItem.Size = new Size(170, 22);
            gestionarVuelosToolStripMenuItem.Text = "Gestionar vuelos";
            gestionarVuelosToolStripMenuItem.Click += gestionarVuelosToolStripMenuItem_Click;
            // 
            // gestionarAsientosToolStripMenuItem
            // 
            gestionarAsientosToolStripMenuItem.Name = "gestionarAsientosToolStripMenuItem";
            gestionarAsientosToolStripMenuItem.Size = new Size(170, 22);
            gestionarAsientosToolStripMenuItem.Text = "Gestionar asientos";
            gestionarAsientosToolStripMenuItem.Click += gestionarAsientosToolStripMenuItem_Click;
            // 
            // gestionarClientesToolStripMenuItem
            // 
            gestionarClientesToolStripMenuItem.Name = "gestionarClientesToolStripMenuItem";
            gestionarClientesToolStripMenuItem.Size = new Size(170, 22);
            gestionarClientesToolStripMenuItem.Text = "Gestionar clientes";
            gestionarClientesToolStripMenuItem.Click += gestionarClientesToolStripMenuItem_Click;
            // 
            // reservasToolStripMenuItem
            // 
            reservasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { registrarReservaToolStripMenuItem });
            reservasToolStripMenuItem.Name = "reservasToolStripMenuItem";
            reservasToolStripMenuItem.Size = new Size(64, 20);
            reservasToolStripMenuItem.Text = "Reservas";
            // 
            // registrarReservaToolStripMenuItem
            // 
            registrarReservaToolStripMenuItem.Name = "registrarReservaToolStripMenuItem";
            registrarReservaToolStripMenuItem.Size = new Size(160, 22);
            registrarReservaToolStripMenuItem.Text = "Registrar reserva";
            registrarReservaToolStripMenuItem.Click += registrarReservaToolStripMenuItem_Click;
            // 
            // reportesToolStripMenuItem
            // 
            reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            reportesToolStripMenuItem.Size = new Size(65, 20);
            reportesToolStripMenuItem.Text = "Reportes";
            // 
            // idiomaToolStripMenuItem
            // 
            idiomaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { españolToolStripMenuItem, inglesToolStripMenuItem });
            idiomaToolStripMenuItem.Name = "idiomaToolStripMenuItem";
            idiomaToolStripMenuItem.Size = new Size(56, 20);
            idiomaToolStripMenuItem.Text = "Idioma";
            // 
            // españolToolStripMenuItem
            // 
            españolToolStripMenuItem.Name = "españolToolStripMenuItem";
            españolToolStripMenuItem.Size = new Size(115, 22);
            españolToolStripMenuItem.Text = "Español";
            españolToolStripMenuItem.Click += españolToolStripMenuItem_Click;
            // 
            // inglesToolStripMenuItem
            // 
            inglesToolStripMenuItem.Name = "inglesToolStripMenuItem";
            inglesToolStripMenuItem.Size = new Size(115, 22);
            inglesToolStripMenuItem.Text = "Ingles";
            inglesToolStripMenuItem.Click += inglesToolStripMenuItem_Click;
            // 
            // ayudaToolStripMenuItem
            // 
            ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            ayudaToolStripMenuItem.Size = new Size(53, 20);
            ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 559);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1184, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(0, 17);
            // 
            // gestionarPerfilesToolStripMenuItem
            // 
            gestionarPerfilesToolStripMenuItem.Name = "gestionarPerfilesToolStripMenuItem";
            gestionarPerfilesToolStripMenuItem.Size = new Size(180, 22);
            gestionarPerfilesToolStripMenuItem.Text = "Gestionar perfiles";
            gestionarPerfilesToolStripMenuItem.Click += gestionarPerfilesToolStripMenuItem_Click;
            // 
            // gestionarFamiliasToolStripMenuItem
            // 
            gestionarFamiliasToolStripMenuItem.Name = "gestionarFamiliasToolStripMenuItem";
            gestionarFamiliasToolStripMenuItem.Size = new Size(180, 22);
            gestionarFamiliasToolStripMenuItem.Text = "Gestionar familias";
            gestionarFamiliasToolStripMenuItem.Click += gestionarFamiliasToolStripMenuItem_Click;
            // 
            // MenuPrincipal_460AS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1184, 581);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "MenuPrincipal_460AS";
            Text = "MenuPrincipal_460AS";
            Load += MenuPrincipal_460AS_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem usuarioToolStripMenuItem;
        private ToolStripMenuItem iniciarSesiónToolStripMenuItem;
        private ToolStripMenuItem cambiarContraseñaToolStripMenuItem;
        private ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private ToolStripMenuItem administradorToolStripMenuItem;
        private ToolStripMenuItem gestionarUsuariosToolStripMenuItem;
        private ToolStripMenuItem maestroToolStripMenuItem;
        private ToolStripMenuItem reportesToolStripMenuItem;
        private ToolStripMenuItem ayudaToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripMenuItem reservasToolStripMenuItem;
        private ToolStripMenuItem registrarReservaToolStripMenuItem;
        private ToolStripMenuItem idiomaToolStripMenuItem;
        private ToolStripMenuItem españolToolStripMenuItem;
        private ToolStripMenuItem inglesToolStripMenuItem;
        private ToolStripMenuItem gestionarVuelosToolStripMenuItem;
        private ToolStripMenuItem gestionarAsientosToolStripMenuItem;
        private ToolStripMenuItem gestionarClientesToolStripMenuItem;
        private ToolStripMenuItem gestionarPerfilesToolStripMenuItem;
        private ToolStripMenuItem gestionarFamiliasToolStripMenuItem;
    }
}