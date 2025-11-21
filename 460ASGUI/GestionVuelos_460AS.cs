using _460ASBE;
using _460ASBLL;
using _460ASServicios.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _460ASGUI
{
    public partial class GestionVuelos_460AS : Form, IIdiomaObserver_460AS
    {
        private BLL460AS_Vuelo bllVuelo_460AS;
        private FormEstado estadoActual = FormEstado.Consulta;
        public GestionVuelos_460AS()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            bllVuelo_460AS = new BLL460AS_Vuelo();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.MultiSelect = false;
            CargarVuelos();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
            label9.Text = IdiomaManager_460AS.Instancia.Traducir("modo_consulta");
        }

        private enum FormEstado
        {
            Consulta,
            Agregar,
            Modificar,
            Eliminar
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label9.Text = IdiomaManager_460AS.Instancia.Traducir("modo_añadir");
                estadoActual = FormEstado.Agregar;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox4.Enabled = true;
                comboBox1.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
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
                label9.Text = IdiomaManager_460AS.Instancia.Traducir("modo_modificar");
                estadoActual = FormEstado.Modificar;
                textBox2.Enabled = true;
                textBox4.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                button1.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
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
                label9.Text = IdiomaManager_460AS.Instancia.Traducir("modo_eliminar");
                estadoActual = FormEstado.Eliminar;
                button1.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
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
                if (estadoActual == FormEstado.Agregar)
                {
                    if (textBox1.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_codvuelo_vacio"));
                    string codVuelo = textBox1.Text;
                    if (!Regex.IsMatch(codVuelo, @"^[A-Za-z]{3}[0-9]{5}$")) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_codvuelo_invalido"));
                    if (bllVuelo_460AS.ObtenerVuelos_460AS().Any(v => v.CodVuelo_460AS == codVuelo)) throw new Exception(string.Format(IdiomaManager_460AS.Instancia.Traducir("msg_codvuelo_existe"), codVuelo));
                    if (textBox2.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_aerolinea_vacio"));
                    string aerolinea = textBox2.Text;
                    string origen = textBox3.Text;
                    if (textBox4.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_precio_vacio"));
                    decimal precio;
                    if (!decimal.TryParse(textBox4.Text, out precio)) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_precio_invalido1"));
                    if (precio <= 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_precio_invalido2"));
                    if (string.IsNullOrWhiteSpace(comboBox1.Text)) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_pais_vacio"));
                    string destino = TraducirAlEspanol(comboBox1.Text);
                    DateTime fechaSalida = dateTimePicker1.Value;
                    if (fechaSalida < DateTime.Now) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_salida_invalida"));
                    DateTime fechaLlegada = dateTimePicker2.Value;
                    if (fechaLlegada < DateTime.Now) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_llegada_invalida"));
                    if (fechaSalida > fechaLlegada) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_fechas_invalidas"));
                    bllVuelo_460AS.AgregarVuelo_460AS(new Vuelo_460AS(codVuelo, aerolinea, origen, destino, fechaSalida, fechaLlegada, precio));
                    textBox1.Clear(); textBox2.Clear(); textBox4.Clear();
                }
                else if (estadoActual == FormEstado.Modificar)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_vuelo_vacio"));
                    var vuelo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    Vuelo_460AS v = bllVuelo_460AS.ObtenerVuelos_460AS().FirstOrDefault(v => v.CodVuelo_460AS == vuelo);
                    if (textBox2.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_aerolinea_vacio"));
                    v.Aerolinea_460AS = textBox2.Text;
                    if (textBox4.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_precio_vacio"));
                    decimal precio;
                    if (!decimal.TryParse(textBox4.Text, out precio)) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_precio_invalido1"));
                    if (precio <= 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_precio_invalido2"));
                    v.FechaSalida_460AS = dateTimePicker1.Value;
                    if (v.FechaSalida_460AS < DateTime.Now) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_salida_invalida"));
                    v.FechaLlegada_460AS = dateTimePicker2.Value;
                    if (v.FechaLlegada_460AS < DateTime.Now) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_llegada_invalida"));
                    if (v.FechaSalida_460AS > v.FechaLlegada_460AS) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_fechas_invalidas"));
                    bllVuelo_460AS.ActualizarVuelo_460AS(v);
                    textBox2.Clear(); textBox4.Clear();
                }
                else if (estadoActual == FormEstado.Eliminar)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_vuelo_vacio"));
                    var vuelo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    bllVuelo_460AS.EliminarVuelo_460AS(vuelo);
                }
                CargarVuelos();
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
                label9.Text = IdiomaManager_460AS.Instancia.Traducir("modo_consulta");
                estadoActual = FormEstado.Consulta;
                textBox1.Clear(); textBox2.Clear(); textBox4.Clear();
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox4.Enabled = false;
                comboBox1.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = false;
                button5.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string TraducirPais(string paisEsp)
        {
            return IdiomaManager_460AS.Instancia.Traducir(paisEsp);
        }

        private string TraducirAlEspanol(string paisTraducido)
        {
            switch (paisTraducido)
            {
                case "Brazil": return "Brasil";
                case "Spain": return "España";
                case "United States": return "Estados Unidos";
                case "Uruguay": return "Uruguay";
                default: return paisTraducido;
            }
        }

        private void CargarVuelos()
        {
            dataGridView1.DataSource = bllVuelo_460AS.ObtenerVuelos_460AS().Select(v => new
            {
                Codigo = v.CodVuelo_460AS,
                Aerolinea = v.Aerolinea_460AS,
                Origen = v.Origen_460AS,
                Destino = TraducirPais(v.Destino_460AS),
                FechaSalida = v.FechaSalida_460AS.ToString("g"),
                FechaLlegada = v.FechaLlegada_460AS.ToString("g"),
                Precio = $"${v.PrecioVuelo_460AS} USD"
            }).ToList();

            dataGridView1.Columns[0].HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_codigo");
            dataGridView1.Columns[1].HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_aerolinea");
            dataGridView1.Columns[2].HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_origen");
            dataGridView1.Columns[3].HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_destino");
            dataGridView1.Columns[4].HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_salida");
            dataGridView1.Columns[5].HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_llegada");
            dataGridView1.Columns[6].HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_precio");
        }

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_vuelos");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_codigo");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_aerolinea");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_origen");
            label5.Text = IdiomaManager_460AS.Instancia.Traducir("label_destino");
            label6.Text = IdiomaManager_460AS.Instancia.Traducir("label_precio");
            label7.Text = IdiomaManager_460AS.Instancia.Traducir("label_salida");
            label8.Text = IdiomaManager_460AS.Instancia.Traducir("label_llegada");
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_añadir");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_modificar");
            button3.Text = IdiomaManager_460AS.Instancia.Traducir("boton_eliminar");
            button4.Text = IdiomaManager_460AS.Instancia.Traducir("boton_guardar");
            button5.Text = IdiomaManager_460AS.Instancia.Traducir("boton_cancelar");
            switch (estadoActual)
            {
                case FormEstado.Agregar:
                    label9.Text = IdiomaManager_460AS.Instancia.Traducir("modo_añadir");
                    break;
                case FormEstado.Consulta:
                    label9.Text = IdiomaManager_460AS.Instancia.Traducir("modo_consulta");
                    break;
                case FormEstado.Eliminar:
                    label9.Text = IdiomaManager_460AS.Instancia.Traducir("modo_eliminar");
                    break;
                case FormEstado.Modificar:
                    label9.Text = IdiomaManager_460AS.Instancia.Traducir("modo_modificar");
                    break;
            }
            comboBox1.Items.Clear();
            comboBox1.Items.Add(IdiomaManager_460AS.Instancia.Traducir("Brasil"));
            comboBox1.Items.Add(IdiomaManager_460AS.Instancia.Traducir("España"));
            comboBox1.Items.Add(IdiomaManager_460AS.Instancia.Traducir("Estados Unidos"));
            comboBox1.Items.Add(IdiomaManager_460AS.Instancia.Traducir("Uruguay"));
            CargarVuelos();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (estadoActual != FormEstado.Modificar)
                return;

            if (dataGridView1.SelectedRows.Count == 0)
                return;

            var fila = dataGridView1.SelectedRows[0];

            textBox1.Text = fila.Cells[0].Value?.ToString();
            textBox2.Text = fila.Cells[1].Value?.ToString();
            textBox3.Text = fila.Cells[2].Value?.ToString();
            comboBox1.Text = fila.Cells[3].Value?.ToString();

            if (DateTime.TryParse(fila.Cells[4].Value?.ToString(), out DateTime salida))
                dateTimePicker1.Value = salida;

            if (DateTime.TryParse(fila.Cells[5].Value?.ToString(), out DateTime llegada))
                dateTimePicker2.Value = llegada;

            string precioStr = fila.Cells[6].Value?.ToString().Replace("$", "").Replace("USD", "").Trim();
            textBox4.Text = precioStr;
        }
    }
}