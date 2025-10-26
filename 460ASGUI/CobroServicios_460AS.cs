using _460ASBE;
using _460ASBLL;
using _460ASServicios.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _460ASGUI
{
    public partial class CobroServicios_460AS : Form, IIdiomaObserver_460AS
    {
        private readonly decimal _montoTotal;
        public string TipoPagoSeleccionado { get; private set; }
        public CobroServicios_460AS(decimal montoTotal)
        {
            InitializeComponent();
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";

            var metodosPago = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("TarjetaCredito", "Tarjeta de crédito"),
                new KeyValuePair<string, string>("TarjetaDebito", "Tarjeta de débito")
            };

            comboBox1.DataSource = metodosPago;
            comboBox1.SelectedIndex = 0;
            _montoTotal = montoTotal; 
            textBox5.Text = $"{_montoTotal:0.00} USD";

            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();

            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.MinDate = DateTime.Today;
            dateTimePicker1.Value = DateTime.Today.AddMonths(1);
        }

        public void ActualizarIdioma()
        {
            //label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_pago");
            //label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_tarjeta");
            //label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_nombre_titular");
            //label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_apellido_titular");
            //label6.Text = IdiomaManager_460AS.Instancia.Traducir("label_monto");
            //label7.Text = IdiomaManager_460AS.Instancia.Traducir("label_fecha_vencimiento");
            //button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_confirmar_pago");
            //comboBox1.Items.Clear();
            //comboBox1.DisplayMember = "Value";
            //comboBox1.ValueMember = "Key";
            //comboBox1.Items.Add(new KeyValuePair<string, string>("TarjetaCredito", IdiomaManager_460AS.Instancia.Traducir("Credito")));
            //comboBox1.Items.Add(new KeyValuePair<string, string>("TarjetaDebito", IdiomaManager_460AS.Instancia.Traducir("Debito")));
            //if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(comboBox1.Text))
                    throw new Exception("Debe seleccionar un método de pago.");

                if (string.IsNullOrWhiteSpace(textBox1.Text))
                    throw new Exception("Debe ingresar el número de tarjeta.");

                if (!Regex.IsMatch(textBox1.Text, @"^[0-9]{10}$"))
                    throw new Exception("El número de tarjeta debe tener 10 dígitos.");

                if (string.IsNullOrWhiteSpace(textBox2.Text))
                    throw new Exception("Debe ingresar el nombre del titular.");

                if (string.IsNullOrWhiteSpace(textBox3.Text))
                    throw new Exception("Debe ingresar el apellido del titular.");

                if (textBox4.Text.Length != 3)
                    throw new Exception("El CVV debe tener 3 dígitos.");

                var kv = (KeyValuePair<string, string>)comboBox1.SelectedItem;
                TipoPagoSeleccionado = kv.Key; // ⚙️ solo la clave, ej: "TarjetaCredito"

                MessageBox.Show(
                    $"Pago realizado correctamente.\n" +
                    $"Monto: {_montoTotal:0.00} USD\n" +
                    $"Tipo de pago: {kv.Value}",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
