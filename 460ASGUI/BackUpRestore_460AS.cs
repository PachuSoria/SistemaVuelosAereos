using _460ASBLL;
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
    public partial class BackUpRestore_460AS : Form
    {
        BLL460AS_BackUpRestore _bll;
        public BackUpRestore_460AS()
        {
            InitializeComponent();
            _bll = new BLL460AS_BackUpRestore();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length > 0)
                {
                    _bll.RealizarBackUp_460AS(textBox1.Text);
                    MessageBox.Show("Se realizo la copia correctamente");
                    label1.Text = $"Último backup realizado: {DateTime.Now:dd/MM/yyyy HH:mm} en {textBox1.Text}";
                    textBox1.Clear();
                }
                else MessageBox.Show("Debe seleccionar la ubicacion para guardar la copia");
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
                    _bll.RealizarRestore_460AS(textBox1.Text);
                    MessageBox.Show("Se realizo la restauracion correctamente");
                    textBox1.Clear();
                }
                else MessageBox.Show("Debe seleccionar la ubicacion para la restauracion");
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
