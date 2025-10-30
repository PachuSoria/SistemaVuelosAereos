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
    public partial class RegistrarComidaEspecial_460AS : Form, IIdiomaObserver_460AS
    {
        public string TipoSeleccionado { get; set; }
        public RegistrarComidaEspecial_460AS()
        {
            InitializeComponent();
            checkedListBox1.CheckOnClick = false;
            checkedListBox1.SelectionMode = SelectionMode.One;
            checkedListBox1.MouseUp += checkedListBox1_MouseUp;
            CargarComidas();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        private void CargarComidas()
        {
            checkedListBox1.Items.Clear();
            foreach (var kvp in preciosComida)
                checkedListBox1.Items.Add($"{kvp.Key} – {kvp.Value:0.00} USD");
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
        }

        private Dictionary<string, decimal> preciosComida = new()
        {
            { "Vegetariana", 20m },
            { "Sin gluten", 22m },
            { "Premium", 30m }
        };
        public decimal TotalComidas { get; private set; } = 0m;

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show(
                    IdiomaManager_460AS.Instancia.Traducir("msg_seleccionar_comida"),
                    "⚠️",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            string texto = checkedListBox1.CheckedItems[0].ToString();
            TipoSeleccionado = texto.Split('–')[0].Trim();
            MessageBox.Show(
                string.Format(IdiomaManager_460AS.Instancia.Traducir("msg_registro_comida"),
                              checkedListBox1.CheckedItems.Count, TotalComidas),
                IdiomaManager_460AS.Instancia.Traducir("msg_servicio_agregado"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void RegistrarComidaEspecial_460AS_Load(object sender, EventArgs e)
        {

        }

        private void ActualizarTotal()
        {
            TotalComidas = 0m;

            foreach (var item in checkedListBox1.CheckedItems)
            {
                string texto = item.ToString()!;
                string nombre = texto.Split('–')[0].Trim();
                TotalComidas += preciosComida[nombre];
            }

            textBox1.Text = $"{TotalComidas:0.00} USD";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((Action)ActualizarTotal);
        }

        private void checkedListBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void checkedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int index = checkedListBox1.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches) return;

            Rectangle itemRect = checkedListBox1.GetItemRectangle(index);

            Rectangle checkRect = new Rectangle(itemRect.X + 2, itemRect.Y + 2, 16, 16);

            if (checkRect.Contains(e.Location))
            {
                bool actual = checkedListBox1.GetItemChecked(index);
                checkedListBox1.SetItemChecked(index, !actual);
            }
        }

        public void ActualizarIdioma()
        {
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_vegetariana");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_gluten");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_premium");
            label5.Text = IdiomaManager_460AS.Instancia.Traducir("label_comidas");
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_precio_final");
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_registrar");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_salir");
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.Add($"{IdiomaManager_460AS.Instancia.Traducir("listbox_vegetariana")} – {preciosComida["Vegetariana"]:0.00} USD");
            checkedListBox1.Items.Add($"{IdiomaManager_460AS.Instancia.Traducir("listbox_gluten")} – {preciosComida["Sin gluten"]:0.00} USD");
            checkedListBox1.Items.Add($"{IdiomaManager_460AS.Instancia.Traducir("listbox_premium")} – {preciosComida["Premium"]:0.00} USD");
        }
    }
}
