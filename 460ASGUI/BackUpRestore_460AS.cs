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
    public partial class BackUpRestore_460AS : Form, IIdiomaObserver_460AS
    {
        BLL460AS_BackUpRestore _bll;
        public BackUpRestore_460AS()
        {
            InitializeComponent();
            _bll = new BLL460AS_BackUpRestore();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        public void ActualizarIdioma()
        {
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_copia");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_restaurar");
            button3.Text = IdiomaManager_460AS.Instancia.Traducir("boton_ubicacion");
            button4.Text = IdiomaManager_460AS.Instancia.Traducir("boton_ubicacion");

            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_estado_backup");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length > 0)
                {
                    if (!Directory.Exists(textBox1.Text))
                        MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_carpeta_inexistente"));

                    try
                    {
                        string testFile = Path.Combine(textBox1.Text, "permiso_prueba.tmp");
                        using (FileStream fs = File.Create(testFile, 1, FileOptions.DeleteOnClose)) { }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_sin_permisos"));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_error_permisos"));
                    }
                    _bll.RealizarBackUp_460AS(textBox1.Text);
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_copia_realizada"));
                    label1.Text = string.Format(IdiomaManager_460AS.Instancia.Traducir("label_ultimo_estado"),
                                                DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                                                textBox1.Text);
                    textBox1.Clear();
                }
                else MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_seleccionar_ubi_copia"));
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
                if (textBox2.Text.Length > 0)
                {
                    if (!File.Exists(textBox2.Text))
                        MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_archivo_inexistente"));

                    try
                    {
                        using (FileStream fs = File.OpenRead(textBox2.Text)) { }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_no_permisos"));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_error_acceso"));
                    }
                    _bll.RealizarRestore_460AS(textBox2.Text);
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_restauracion_realizada"));
                    textBox1.Clear();
                }
                else MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_seleccionar_ubi_restauracion"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        textBox1.Text = folderDialog.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "SQL Backup Files (*.bak)|*.bak";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        textBox2.Text = openFileDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
