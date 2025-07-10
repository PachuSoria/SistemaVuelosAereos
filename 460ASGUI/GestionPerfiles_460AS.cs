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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _460ASGUI
{
    public partial class GestionPerfiles_460AS : Form, IIdiomaObserver_460AS
    {
        BLL460AS_Perfil bllPerfil;
        BLL460AS_Permiso bllPermiso;
        BLL460AS_Familia bllFamilia;
        private Familia_460AS familiaSeleccionada;
        public GestionPerfiles_460AS()
        {
            InitializeComponent();
            bllPerfil = new BLL460AS_Perfil();
            bllPermiso = new BLL460AS_Permiso();
            bllFamilia = new BLL460AS_Familia();
            CargarPerfiles();
            CargarFamilias();
            CargarPermisos();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        private void CargarPerfiles()
        {
            treeView1.Nodes.Clear();
            var perfiles = bllPerfil.ObtenerTodas_460AS();
            foreach (var perfil in perfiles)
            {
                TreeNode nodoPerfil = new TreeNode(perfil.Nombre_460AS) { Tag = perfil.Codigo_460AS };

                var familias = bllPerfil.ObtenerFamiliasDePerfil_460AS(perfil.Codigo_460AS);
                foreach (var fam in familias)
                    nodoPerfil.Nodes.Add(new TreeNode("[Familia] " + fam.Nombre_460AS) { Tag = fam });

                var permisos = bllPerfil.ObtenerPermisosDePerfil_460AS(perfil.Codigo_460AS);
                foreach (var perm in permisos)
                    nodoPerfil.Nodes.Add(new TreeNode("[Permiso] " + perm.Nombre_460AS) { Tag = perm });

                treeView1.Nodes.Add(nodoPerfil);
            }
        }

        private void CargarFamilias()
        {
            treeView2.Nodes.Clear();

            var todas = bllFamilia.ObtenerTodas_460AS();

            var codigosHijas = todas
                .SelectMany(f => bllFamilia.ObtenerFamiliasHijas_460AS(f))
                .Select(h => h.Codigo_460AS)
                .Distinct()
                .ToList();

            var familiasRaiz = todas.Where(f => !codigosHijas.Contains(f.Codigo_460AS)).ToList();

            foreach (var raiz in familiasRaiz)
            {
                TreeNode nodoCompleto = ConstruirNodoFamiliaRecursivo(raiz);
                treeView2.Nodes.Add(nodoCompleto);
            }
        }
        

        private void AgregarHijasRecursivas(TreeNode nodoPadre, Familia_460AS familiaPadre)
        {
            var hijas = bllFamilia.ObtenerFamiliasHijas_460AS(familiaPadre);
            foreach (var hija in hijas)
            {
                TreeNode nodoHija = new TreeNode(hija.Nombre_460AS) { Tag = hija };
                nodoPadre.Nodes.Add(nodoHija);
                AgregarHijasRecursivas(nodoHija, hija);
            }
        }

        private TreeNode ConstruirNodoFamiliaRecursivo(Familia_460AS familia)
        {
            TreeNode nodoFamilia = new TreeNode("[FAMILIA] " + familia.Nombre_460AS) { Tag = familia };

            var familiasHijas = bllFamilia.ObtenerFamiliasHijas_460AS(familia);
            foreach (var famHija in familiasHijas)
            {
                TreeNode nodoHija = ConstruirNodoFamiliaRecursivo(famHija);
                nodoFamilia.Nodes.Add(nodoHija);
            }

            var permisos = bllFamilia.ObtenerPermisosDeFamilia_460AS(familia.Codigo_460AS);
            foreach (var permiso in permisos)
            {
                TreeNode nodoPermiso = new TreeNode("[PERMISO] " + permiso.Nombre_460AS) { Tag = permiso };
                nodoFamilia.Nodes.Add(nodoPermiso);
            }

            return nodoFamilia;
        }

        private void CargarPermisos()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = bllPermiso.ObtenerTodos_460AS();
            listBox1.DisplayMember = "Nombre_460AS";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Perfil_460AS nuevo = new Perfil_460AS(textBox1.Text.Trim(), textBox2.Text.Trim());
                bllPerfil.GuardarPerfil_460AS(nuevo);
                CargarPerfiles();
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
                if (treeView1.SelectedNode == null || !(treeView1.SelectedNode.Tag is string codPerfil))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_seleccione_perfil_treeview"));

                if (listBox1.SelectedItem == null || !(listBox1.SelectedItem is Permiso_460AS permiso))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_seleccione_permiso"));

                var permisosAsignados = bllPerfil.ObtenerPermisosDePerfil_460AS(codPerfil);
                if (permisosAsignados.Any(p => p.Codigo_460AS == permiso.Codigo_460AS))
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_permiso_ya_asignado_perfil"));
                    return;
                }

                bllPerfil.AgregarPermisoAPerfil_460AS(codPerfil, permiso.Codigo_460AS);
                CargarPerfiles();
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
                if (familiaSeleccionada == null)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_seleccione_familia"));

                if (treeView1.SelectedNode == null || treeView1.SelectedNode.Level != 0)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_seleccione_perfil_arbol"));

                string codPerfil = treeView1.SelectedNode.Tag.ToString();

                bllPerfil.AgregarFamiliaAPerfil_460AS(codPerfil, familiaSeleccionada.Codigo_460AS);

                familiaSeleccionada = null; 
                CargarPerfiles();
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
                if (treeView1.SelectedNode == null || treeView1.SelectedNode.Level != 0)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_seleccione_perfil"));

                string codPerfil = treeView1.SelectedNode.Tag.ToString();
                Perfil_460AS perfil = new Perfil_460AS(codPerfil, textBox2.Text.Trim());
                bllPerfil.EliminarPerfil_460AS(perfil);
                CargarPerfiles();
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
                if (treeView1.SelectedNode == null || treeView1.SelectedNode.Level != 1)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_seleccione_familia_o_permiso"));

                string codPerfil = treeView1.SelectedNode.Parent.Tag.ToString();
                var tag = treeView1.SelectedNode.Tag;

                if (tag is Familia_460AS fam)
                    bllPerfil.EliminarFamiliaDePerfil_460AS(codPerfil, fam.Codigo_460AS);
                else if (tag is Permiso_460AS perm)
                    bllPerfil.EliminarPermisoDePerfil_460AS(codPerfil, perm.Codigo_460AS);
                else
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_tipo_nodo_no_reconocido"));

                CargarPerfiles();
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
                if (treeView2.SelectedNode == null)
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_seleccione_familia_arbol"));
                    return;
                }

                if (treeView2.SelectedNode.Tag is Familia_460AS fam)
                {
                    familiaSeleccionada = fam;
                    label7.Text = $"{fam.Codigo_460AS} - {fam.Nombre_460AS}";
                }
                else
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_nodo_invalido_seleccionado"));
                }
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

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_perfiles");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_permisos");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_familias");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_cod_perfil");
            label5.Text = IdiomaManager_460AS.Instancia.Traducir("label_nombre_perfil");
            label6.Text = IdiomaManager_460AS.Instancia.Traducir("label_familia_seleccionada");
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_crear_perfil");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_asignar_permiso");
            button3.Text = IdiomaManager_460AS.Instancia.Traducir("boton_asignar_familia");
            button4.Text = IdiomaManager_460AS.Instancia.Traducir("boton_eliminar_perfil");
            button5.Text = IdiomaManager_460AS.Instancia.Traducir("boton_eliminar_hijo");
            button6.Text = IdiomaManager_460AS.Instancia.Traducir("boton_seleccionar");
            button7.Text = IdiomaManager_460AS.Instancia.Traducir("boton_salir");
        }
    }
}
