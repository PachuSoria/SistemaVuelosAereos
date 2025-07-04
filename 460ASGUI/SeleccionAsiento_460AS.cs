using _460ASBE;
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
    public partial class SeleccionAsiento_460AS : Form
    {
        BLL460AS_Asiento bllAsiento_460AS;
        private string codVueloSeleccionado; 
        private List<Asiento_460AS> asientosDisponibles;
        private List<Asiento_460AS> asientosSeleccionados;
        public List<Asiento_460AS> AsientosSeleccionados => asientosSeleccionados;
        public SeleccionAsiento_460AS(string codVuelo)
        {
            InitializeComponent();
            bllAsiento_460AS = new BLL460AS_Asiento();
            codVueloSeleccionado = codVuelo;
            radioButton1.Checked = true;
            asientosSeleccionados = new List<Asiento_460AS>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar un asiento");
                string numAsiento = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var asiento = asientosDisponibles.FirstOrDefault(a => a.NumAsiento_460AS == numAsiento);
                if (!asiento.Disponible_460AS) throw new Exception($"El asiento {numAsiento} esta ocupado");
                if (asientosSeleccionados.Any(a => a.NumAsiento_460AS == numAsiento)) throw new Exception("Este asiento ya fue agregado");
                asientosSeleccionados.Add(asiento);
                if (!listBox1.Items.Contains(numAsiento)) listBox1.Items.Add(numAsiento);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarAsientos(TipoAsiento_460AS tipo)
        {
            asientosDisponibles = bllAsiento_460AS.ObtenerAsientos_460AS(codVueloSeleccionado, tipo);
            dataGridView1.DataSource = asientosDisponibles.Select(a => new
            {
                Numero = a.NumAsiento_460AS,
                Tipo = a.Tipo_460AS.ToString(),
                Estado = a.Disponible_460AS ? "Disponible" : "Ocupado"
            }).ToList();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) CargarAsientos(TipoAsiento_460AS.Normal);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) CargarAsientos(TipoAsiento_460AS.VIP);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (asientosSeleccionados.Count == 0) throw new Exception("No se seleccionaron asientos");
                MessageBox.Show("Asientos cargados correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
