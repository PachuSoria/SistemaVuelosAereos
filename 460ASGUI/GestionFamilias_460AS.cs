using _460ASBLL;
using _460ASServicios.Composite;
using _460ASServicios.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _460ASGUI
{
    public partial class GestionFamilias_460AS : Form, IIdiomaObserver_460AS
    {
        private BLL460AS_Familia bllFamilia;
        private BLL460AS_Permiso bllPermiso;
        private Familia_460AS familiaSeleccionada;
        private TreeNode ultimoNodoSeleccionado;
        public GestionFamilias_460AS()
        {
            InitializeComponent();
            bllFamilia = new BLL460AS_Familia();
            bllPermiso = new BLL460AS_Permiso();
            CargarFormulario();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        private void CargarFormulario()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = bllPermiso.ObtenerTodos_460AS();
            listBox1.DisplayMember = "Nombre_460AS";
            CargarTreeView();
        }

        private void CargarTreeView()
        {
            treeView1.Nodes.Clear();

            var familias = bllFamilia.ObtenerTodas_460AS();

            foreach (var fam in familias)
            {
                TreeNode nodo = CrearNodoFamilia(fam);
                treeView1.Nodes.Add(nodo);
            }

            treeView1.ExpandAll();
        }

        private TreeNode CrearNodoFamilia(Familia_460AS familia)
        {
            TreeNode nodo = new TreeNode($"{familia.Codigo_460AS} - {familia.Nombre_460AS}");
            nodo.Tag = familia;

            var permisos = bllFamilia.ObtenerPermisosDeFamilia_460AS(familia.Codigo_460AS);
            foreach (var permiso in permisos)
            {
                TreeNode hijoPermiso = new TreeNode($"{permiso.Nombre_460AS}");
                hijoPermiso.Tag = permiso;
                nodo.Nodes.Add(hijoPermiso);
            }
            var hijas = bllFamilia.ObtenerFamiliasHijas_460AS(familia);
            foreach (var hija in hijas)
            {
                nodo.Nodes.Add(CrearNodoFamilia(hija));
            }

            return nodo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = textBox1.Text.Trim();
                if (!Regex.IsMatch(codigo, @"^[A-Z]{3}[0-9]{2}")) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_codigo_familia_invalido"));
                string nombre = textBox2.Text.Trim();

                var familia = new Familia_460AS { Codigo_460AS = codigo, Nombre_460AS = nombre };
                bllFamilia.GuardarFamilia_460AS(familia);

                CargarFormulario();
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
                if (treeView1.SelectedNode == null || !(treeView1.SelectedNode.Tag is Familia_460AS familia))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_seleccionar_para_asignar_permiso"));

                var permiso = (Permiso_460AS)listBox1.SelectedItem;
                if (permiso == null) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_permiso_seleccionar_valido"));
                var permisosHeredados = bllFamilia.ObtenerPermisosHeredados_460AS(familia.Codigo_460AS);
                if (permisosHeredados.Any(p => p.Codigo_460AS == permiso.Codigo_460AS))
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_permiso_ya_asignado"));
                    return;
                }
                bllFamilia.AgregarPermisoAFamilia_460AS(familia.Codigo_460AS, permiso.Codigo_460AS);
                CargarFormulario();
                RestaurarSeleccionAnterior();
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
                if (treeView1.SelectedNode == null || !(treeView1.SelectedNode.Tag is Familia_460AS padre))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_seleccionar_padre_valido"));

                if (familiaSeleccionada == null)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_seleccionar_hija_previa"));

                bllFamilia.AsignarFamiliaHija_460AS(padre, familiaSeleccionada);

                CargarFormulario();
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
                if (treeView1.SelectedNode == null)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_seleccionar_eliminar"));

                if (!(treeView1.SelectedNode.Tag is Familia_460AS fam))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_seleccionar_valida"));

                bllFamilia.EliminarFamilia_460AS(fam);
                CargarFormulario();
                if (familiaSeleccionada != null && familiaSeleccionada.Codigo_460AS == fam.Codigo_460AS)
                {
                    familiaSeleccionada = null;
                    label6.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode == null || treeView1.SelectedNode.Parent == null)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_nodo_hijo_seleccionar"));

                var padre = treeView1.SelectedNode.Parent.Tag as Familia_460AS;
                var hijo = treeView1.SelectedNode.Tag;

                bllFamilia.EliminarHijo_460AS(padre, hijo);
                CargarFormulario();
                RestaurarSeleccionAnterior();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode == null || !(treeView1.SelectedNode.Tag is Familia_460AS familia))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_familia_valida_seleccionar"));

                familiaSeleccionada = familia;
                label6.Text = $"{familia.Codigo_460AS} - {familia.Nombre_460AS}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RestaurarSeleccionAnterior()
        {
            if (ultimoNodoSeleccionado != null)
            {
                foreach (TreeNode nodo in treeView1.Nodes)
                {
                    TreeNode encontrado = BuscarNodoPorTag(nodo, ultimoNodoSeleccionado.Tag);
                    if (encontrado != null)
                    {
                        treeView1.SelectedNode = encontrado;
                        encontrado.EnsureVisible();
                        break;
                    }
                }
            }
        }

        private TreeNode BuscarNodoPorTag(TreeNode nodoActual, object tagBuscado)
        {
            if (nodoActual.Tag != null && nodoActual.Tag.Equals(tagBuscado))
                return nodoActual;

            foreach (TreeNode hijo in nodoActual.Nodes)
            {
                TreeNode resultado = BuscarNodoPorTag(hijo, tagBuscado);
                if (resultado != null)
                    return resultado;
            }

            return null;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ultimoNodoSeleccionado = e.Node;
        }

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_cod_familia");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_nombre_familia");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_familias");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_permisos");
            label5.Text = IdiomaManager_460AS.Instancia.Traducir("label_familia_seleccionada");
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_crear_familia");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_asignar_permiso");
            button3.Text = IdiomaManager_460AS.Instancia.Traducir("boton_asignar_familia");
            button4.Text = IdiomaManager_460AS.Instancia.Traducir("boton_eliminar_familia");
            button5.Text = IdiomaManager_460AS.Instancia.Traducir("boton_eliminar_hijo");
            button6.Text = IdiomaManager_460AS.Instancia.Traducir("boton_seleccionar");
            button7.Text = IdiomaManager_460AS.Instancia.Traducir("boton_salir");
        }
    }
}
