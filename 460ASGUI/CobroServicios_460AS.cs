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
            comboBox1.DataSource = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("TarjetaCredito", IdiomaManager_460AS.Instancia.Traducir("combo_credito")),
                new KeyValuePair<string, string>("TarjetaDebito", IdiomaManager_460AS.Instancia.Traducir("combo_debito"))
            };
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
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_metodo_pago");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_num_tarjeta");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_nombre_titular");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_apellido_titular");
            label5.Text = IdiomaManager_460AS.Instancia.Traducir("label_cvv");
            label6.Text = IdiomaManager_460AS.Instancia.Traducir("label_vencimiento");
            label7.Text = IdiomaManager_460AS.Instancia.Traducir("label_total");
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_confirmar_pago");
            comboBox1.DataSource = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("TarjetaCredito", IdiomaManager_460AS.Instancia.Traducir("combo_credito")),
                new KeyValuePair<string, string>("TarjetaDebito", IdiomaManager_460AS.Instancia.Traducir("combo_debito"))
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(comboBox1.Text))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_selec_metodo_pago"));

                if (string.IsNullOrWhiteSpace(textBox1.Text))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ingrese_num_tarjeta"));

                if (!Regex.IsMatch(textBox1.Text, @"^[0-9]{10}$"))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_tarjeta_diez_digitos"));

                if (string.IsNullOrWhiteSpace(textBox2.Text))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ingrese_nombre_titular"));

                if (string.IsNullOrWhiteSpace(textBox3.Text))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ingrese_apellido_titular"));

                if (textBox4.Text.Length != 3)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_cvv_tres_digitos"));

                var kv = (KeyValuePair<string, string>)comboBox1.SelectedItem;
                TipoPagoSeleccionado = kv.Key;

                MessageBox.Show(
                string.Format(
                    "{0}\n{1}: {2:0.00} USD\n{3}: {4}",
                    IdiomaManager_460AS.Instancia.Traducir("msg_pago_exito"),
                    IdiomaManager_460AS.Instancia.Traducir("label_total"),
                    _montoTotal,
                    IdiomaManager_460AS.Instancia.Traducir("label_metodo_pago"),
                    kv.Value
                ),
                IdiomaManager_460AS.Instancia.Traducir("msg_titulo_exito"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

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
