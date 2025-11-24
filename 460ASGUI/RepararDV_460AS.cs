using _460ASBE;
using _460ASBLL;
using _460ASServicios.Observer;
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
    public partial class RepararDV_460AS : Form, IIdiomaObserver_460AS
    {
        BLL460AS_DV dvBLL;
        BLL460AS_BackUpRestore backupBLL;
        BLL460AS_Usuario usuarioBLL;
        public RepararDV_460AS()
        {
            InitializeComponent();
            dvBLL = new BLL460AS_DV();
            backupBLL = new BLL460AS_BackUpRestore();
            usuarioBLL = new BLL460AS_Usuario();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_inconsistencia");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_tablas");
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_recalcular");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_restaurar_bd");
            button3.Text = IdiomaManager_460AS.Instancia.Traducir("boton_salir");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in dvBLL.CompararDV_460AS())
                {
                    DV_460AS dv = dvBLL.ObtenerTodos_327LG().First(x => x.NombreTabla_460AS == item);
                    dvBLL.GuardarDV_460AS(dv);
                }
                MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_dv_recalculados"));
                this.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog fd = new OpenFileDialog())
                {
                    fd.Filter = "SQL Backup Files (*.bak)|*.bak";
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        if (fd.FileName == string.Empty) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_seleccionar_archivo"));
                        backupBLL.RealizarRestore_460AS(fd.FileName);
                        MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_restaurado"));
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void RepararDV_460AS_Load(object sender, EventArgs e)
        {
            if (dvBLL.CompararDV_460AS().Count > 0)
            {
                string l = string.Empty;
                foreach (string item in dvBLL.CompararDV_460AS())
                {
                    l += item + ", ";
                }
                MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_inconsistencias"));
                ActualizarIdioma();
                label2.Text += l;
            }
        }
    }
}
