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
    public partial class RegistrarSeguroViaje_460AS : Form
    {
        public string SeguroSeleccionado => seguroSeleccionado;
        private DateTime fechaSalidaVuelo;
        private Dictionary<string, decimal> preciosSeguros = new()
        {
            { "Premium", 80m },
            { "Intermedio", 55m },
            { "Basico", 40m }
        };
        private string seguroSeleccionado = string.Empty;
        public decimal PrecioSeleccionado { get; private set; } = 0m;
        public DateTime FechaVencimiento { get; private set; }
        public RegistrarSeguroViaje_460AS(DateTime fechaSalida)
        {
            InitializeComponent();
            fechaSalidaVuelo = fechaSalida;
            var fechaMinima = fechaSalidaVuelo.AddDays(7);
            dateTimePicker1.Value = DateTime.Today > fechaMinima ? DateTime.Today : fechaMinima;
            dateTimePicker1.MinDate = fechaMinima;
            dateTimePicker1.Value = fechaMinima;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            radioButton2.CheckedChanged += radioButton1_CheckedChanged;
            radioButton3.CheckedChanged += radioButton1_CheckedChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(seguroSeleccionado)) throw new Exception("Debe seleccionar un tipo de seguro de viaje.");

                FechaVencimiento = dateTimePicker1.Value;
                if (FechaVencimiento < fechaSalidaVuelo.AddDays(7))
                    throw new Exception($"La fecha de vencimiento debe ser al menos 7 días posterior a la fecha de salida del vuelo ({fechaSalidaVuelo:dd/MM/yyyy}).");

                PrecioSeleccionado = preciosSeguros[seguroSeleccionado];

                MessageBox.Show($"Seguro {seguroSeleccionado} registrado correctamente.\n" +
                                $"Vencimiento: {FechaVencimiento:dd/MM/yyyy}\n" +
                                $"Precio: {PrecioSeleccionado:0.00} USD",
                                "Servicio agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                seguroSeleccionado = "Premium";
            else if (radioButton2.Checked)
                seguroSeleccionado = "Intermedio";
            else if (radioButton3.Checked)
                seguroSeleccionado = "Basico";
            else
                seguroSeleccionado = string.Empty;

            if (!string.IsNullOrEmpty(seguroSeleccionado))
                textBox1.Text = $"{preciosSeguros[seguroSeleccionado]:0.00} USD";
            else
                textBox1.Clear();
        }
    }
}
