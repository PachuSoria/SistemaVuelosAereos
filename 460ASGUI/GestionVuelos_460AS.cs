using _460ASBE;
using _460ASBLL;
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
    public partial class GestionVuelos_460AS : Form
    {
        private BLL460AS_Vuelo bllVuelo_460AS;
        private FormEstado estadoActual = FormEstado.Consulta;
        public GestionVuelos_460AS()
        {
            InitializeComponent();
            bllVuelo_460AS = new BLL460AS_Vuelo();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.MultiSelect = false;
            label9.Text = "Modo Consulta";
            dataGridView1.DataSource = bllVuelo_460AS.ObtenerVuelos_460AS();
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
                label9.Text = "Modo Agregar";
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
                label9.Text = "Modo Modificar";
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
                label9.Text = "Modo Eliminar";
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
                    if (textBox1.Text.Length == 0) throw new Exception("Debe ingresar el codigo de vuelo");
                    string codVuelo = textBox1.Text;
                    if (!Regex.IsMatch(codVuelo, @"^[A-Za-z]{3}[0-9]{5}$")) throw new Exception("El codigo de vuelo no es valido");
                    if (bllVuelo_460AS.ObtenerVuelos_460AS().Any(v => v.CodVuelo_460AS == codVuelo)) throw new Exception($"Ya existe un vuelo con el codigo {codVuelo}");
                    if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar la aerolinea");
                    string aerolinea = textBox2.Text;
                    string origen = textBox3.Text;
                    if (textBox4.Text.Length == 0) throw new Exception("Debe ingresar el precio");
                    decimal precio;
                    if (!decimal.TryParse(textBox4.Text, out precio)) throw new Exception("El precio debe ser un valor numérico válido");
                    if (precio <= 0) throw new Exception("El precio no es valido");
                    if (string.IsNullOrWhiteSpace(comboBox1.Text)) throw new Exception("Debe seleccionar el pais");
                    string destino = comboBox1.Text;
                    DateTime fechaSalida = dateTimePicker1.Value;
                    if (fechaSalida < DateTime.Now) throw new Exception("La fecha de salida no es valida");
                    DateTime fechaLlegada = dateTimePicker2.Value;
                    if (fechaLlegada < DateTime.Now) throw new Exception("La fecha de llegada no es valida");
                    if (fechaSalida > fechaLlegada) throw new Exception("Las fechas no son validas");
                    bllVuelo_460AS.AgregarVuelo_460AS(new Vuelo_460AS(codVuelo, aerolinea, origen, destino, fechaSalida, fechaLlegada, precio));
                    textBox1.Clear(); textBox2.Clear(); textBox4.Clear();
                }
                else if (estadoActual == FormEstado.Modificar)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception("Debe seleccionar un vuelo");
                    var vuelo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    Vuelo_460AS v = bllVuelo_460AS.ObtenerVuelos_460AS().FirstOrDefault(v => v.CodVuelo_460AS == vuelo);
                    if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar la aerolinea");
                    v.Aerolinea_460AS = textBox2.Text;
                    if (textBox4.Text.Length == 0) throw new Exception("Debe ingresar el precio");
                    decimal precio;
                    if (!decimal.TryParse(textBox4.Text, out precio)) throw new Exception("El precio debe ser un valor numérico válido");
                    if (precio <= 0) throw new Exception("El precio no es válido");
                    v.FechaSalida_460AS = dateTimePicker1.Value;
                    if (v.FechaSalida_460AS < DateTime.Now) throw new Exception("La fecha de salida no es valida");
                    v.FechaLlegada_460AS = dateTimePicker2.Value;
                    if (v.FechaLlegada_460AS < DateTime.Now) throw new Exception("La fecha de llegada no es valida");
                    if (v.FechaSalida_460AS > v.FechaLlegada_460AS) throw new Exception("Las fechas no son validas");
                    bllVuelo_460AS.ActualizarVuelo_460AS(v);
                    textBox2.Clear(); textBox4.Clear();
                }
                else if (estadoActual == FormEstado.Eliminar)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception("Debe seleccionar un vuelo");
                    var vuelo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    bllVuelo_460AS.EliminarVuelo_460AS(vuelo);
                }
                dataGridView1.DataSource = bllVuelo_460AS.ObtenerVuelos_460AS().Select(v => new
                {
                    Codigo = v.CodVuelo_460AS,
                    Aerolinea = v.Aerolinea_460AS,
                    Origen = v.Origen_460AS,
                    Destino = v.Destino_460AS,
                    FechaSalida = v.FechaSalida_460AS.ToString("g"),
                    FechaLlegada = v.FechaLlegada_460AS.ToString("g"),
                    Precio = $"${v.PrecioVuelo_460AS} USD"
                }).ToList();
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
                label9.Text = "Modo Consulta";
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
    }
}