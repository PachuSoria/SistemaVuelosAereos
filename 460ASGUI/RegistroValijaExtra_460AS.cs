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
    public partial class RegistroValijaExtra_460AS : Form, IIdiomaObserver_460AS
    {
        private List<(int Cantidad, string Peso, decimal Precio)> valijasAgregadas = new();
        private Dictionary<int, decimal> preciosPorPeso = new();
        public decimal TotalValijas => valijasAgregadas.Sum(v => v.Precio);
        public int CantidadTotal {  get; set; }
        public decimal PesoTotal { get; set; }
        public RegistroValijaExtra_460AS()
        {
            InitializeComponent();
            ConfigurarPrecios();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
            comboBox1.SelectedIndexChanged += ActualizarPrecio;
            comboBox2.SelectedIndexChanged += ActualizarPrecio;
        }

        private void ConfigurarPrecios()
        {
            preciosPorPeso[10] = 10m;
            preciosPorPeso[15] = 15m;
            preciosPorPeso[23] = 25m;
            preciosPorPeso[32] = 40m;
        }

        private void ActualizarPrecio(object? sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                textBox1.Text = "";
                return;
            }

            int cantidad = int.Parse(comboBox1.SelectedItem.ToString()!);
            string pesoTxt = comboBox2.SelectedItem.ToString()!;
            int pesoValor = int.Parse(pesoTxt.Split(' ')[0]); 

            decimal precioBase = preciosPorPeso[pesoValor];
            decimal total = cantidad * precioBase;
            textBox1.Text = $"{total:0.00} USD";
        }

        public void ActualizarIdioma()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
                    throw new Exception("Debe seleccionar cantidad y peso.");

                int cantidad = int.Parse(comboBox1.SelectedItem.ToString()!);
                string pesoTxt = comboBox2.SelectedItem.ToString()!;
                int pesoValor = int.Parse(pesoTxt.Split(' ')[0]);

                decimal precioBase = preciosPorPeso[pesoValor];
                decimal precio = cantidad * precioBase;

                int totalValijasActuales = valijasAgregadas.Sum(v => v.Cantidad);

                if (totalValijasActuales + cantidad > 4)
                    throw new Exception("Solo puede agregar hasta 4 valijas en total por reserva.");
                valijasAgregadas.Add((cantidad, pesoTxt, precio));

                listBox1.Items.Add($"{cantidad} x {pesoTxt}  →  {precio:0.00} USD");

                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                textBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (valijasAgregadas.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos una valija.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int totalValijas = valijasAgregadas.Sum(v => v.Cantidad);
            decimal totalPeso = valijasAgregadas.Sum(v =>
            {
                int kilos = int.Parse(v.Peso.Split(' ')[0]);
                return v.Cantidad * kilos;
            });
            CantidadTotal = totalValijas;
            PesoTotal = totalPeso;
            MessageBox.Show($"Se registraron {totalValijas} valija(s) – Total: {TotalValijas:0.00} USD",
                            "Servicio agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
