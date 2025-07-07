using _460ASBLL;
using _460ASServicios.Composite;
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
    public partial class GestionFamilias_460AS : Form
    {
        private BLL460AS_Familia _bllFamilia = new BLL460AS_Familia();
        private BLL460AS_Permiso _bllPermiso = new BLL460AS_Permiso();
        private List<Familia_460AS> _familias;
        public GestionFamilias_460AS()
        {
            InitializeComponent();
            CargarFamilias();
            CargarComponentes();
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        }

        private void CargarFamilias()
        {
            listBox1.Items.Clear();
            _familias = _bllFamilia.ObtenerTodasLasFamilias().ToList();

            foreach (var familia in _familias)
            {
                listBox1.Items.Add(familia);
            }
        }

        private void CargarComponentes()
        {
            treeView1.Nodes.Clear();

            TreeNode nodoPermisos = new TreeNode("Permisos");
            foreach (var permiso in _bllPermiso.ObtenerTodosLosPermisos())
            {
                TreeNode nodo = new TreeNode(permiso.Nombre_460AS)
                {
                    Tag = permiso,
                    Checked = false
                };
                nodoPermisos.Nodes.Add(nodo);
            }

            TreeNode nodoFamilias = new TreeNode("Familias");
            foreach (var familia in _bllFamilia.ObtenerTodasLasFamilias())
            {
                TreeNode nodo = new TreeNode(familia.Nombre_460AS)
                {
                    Tag = familia,
                    Checked = false
                };
                nodoFamilias.Nodes.Add(nodo);
            }

            treeView1.Nodes.Add(nodoPermisos);
            treeView1.Nodes.Add(nodoFamilias);
            treeView1.ExpandAll();
        }

        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            foreach (TreeNode nodoRaiz in treeView1.Nodes)
            {
                foreach (TreeNode nodo in nodoRaiz.Nodes)
                {
                    nodo.Checked = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = textBox1.Text;
                string nombre = textBox2.Text;
                Familia_460AS familia = new Familia_460AS(codigo, nombre);

                foreach (TreeNode nodoRaiz in treeView1.Nodes)
                {
                    foreach (TreeNode nodo in nodoRaiz.Nodes)
                    {
                        if (nodo.Checked)
                        {
                            if (nodo.Tag is Permiso_460AS permiso)
                                familia.AgregarHijo(permiso);
                            else if (nodo.Tag is Familia_460AS familiaHija)
                                familia.AgregarHijo(familiaHija);
                        }
                    }
                }

                _bllFamilia.RegistrarFamilia(familia);
                MessageBox.Show("Familia guardada correctamente");
                CargarFamilias();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = textBox1.Text;
                string nombre = textBox2.Text;
                Familia_460AS familia = new Familia_460AS(codigo, nombre);

                foreach (TreeNode nodoRaiz in treeView1.Nodes)
                {
                    foreach (TreeNode nodo in nodoRaiz.Nodes)
                    {
                        if (nodo.Checked)
                        {
                            if (nodo.Tag is Permiso_460AS permiso)
                                familia.AgregarHijo(permiso);
                            else if (nodo.Tag is Familia_460AS familiaHija)
                                familia.AgregarHijo(familiaHija);
                        }
                    }
                }

                _bllFamilia.ModificarFamilia(familia);
                MessageBox.Show("Familia modificada correctamente");
                CargarFamilias();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem is Familia_460AS familia)
                {
                    if (MessageBox.Show("¿Está seguro de eliminar esta familia?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            _bllFamilia.EliminarFamilia(familia.Codigo_460AS);
                            MessageBox.Show("Familia eliminada correctamente");
                            CargarFamilias();
                            LimpiarCampos();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Familia_460AS familiaSeleccionada)
            {
                textBox1.Text = familiaSeleccionada.Codigo_460AS;
                textBox2.Text = familiaSeleccionada.Nombre_460AS;

                foreach (TreeNode nodoRaiz in treeView1.Nodes)
                {
                    foreach (TreeNode nodo in nodoRaiz.Nodes)
                    {
                        nodo.Checked = false;
                    }
                }

                var hijos = familiaSeleccionada.ObtenerHijos();

                foreach (var hijo in hijos)
                {
                    foreach (TreeNode nodoRaiz in treeView1.Nodes)
                    {
                        foreach (TreeNode nodo in nodoRaiz.Nodes)
                        {
                            if (nodo.Tag is Permiso_460AS permisoNodo && hijo is Permiso_460AS permiso)
                            {
                                if (permisoNodo.Codigo_460AS == permiso.Codigo_460AS)
                                    nodo.Checked = true;
                            }
                            else if (nodo.Tag is Familia_460AS familiaNodo && hijo is Familia_460AS familia)
                            {
                                if (familiaNodo.Codigo_460AS == familia.Codigo_460AS)
                                    nodo.Checked = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
