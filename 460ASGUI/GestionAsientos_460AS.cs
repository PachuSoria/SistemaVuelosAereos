using _460ASBE;
using _460ASBLL;
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
    public partial class GestionAsientos_460AS : Form, IIdiomaObserver_460AS
    {
        private BLL460AS_Asiento bllAsiento_460AS;
        private BLL460AS_Vuelo bllVuelo_460AS;
        private FormEstado estadoActual = FormEstado.Consulta;
        public GestionAsientos_460AS()
        {
            InitializeComponent();
            bllAsiento_460AS = new BLL460AS_Asiento();
            bllVuelo_460AS = new BLL460AS_Vuelo();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.MultiSelect = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView2.MultiSelect = false;
            label5.Text = "Modo Consulta";
            ActualizarIdioma();
            CargarVuelos();
        }

        private enum FormEstado
        {
            Consulta,
            Agregar,
            Eliminar
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label5.Text = "Modo Agregar";
                estadoActual = FormEstado.Agregar;
                textBox1.Enabled = true;
                comboBox1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = true;
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
                label5.Text = "Modo Eliminar";
                estadoActual = FormEstado.Eliminar;
                button1.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = true;
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
                if (estadoActual == FormEstado.Agregar)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_vuelo_vacio"));
                    var vuelo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    var asientos = bllAsiento_460AS.ObtenerAsientos_460AS(vuelo);
                    if (textBox1.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_asiento_vacio"));
                    string numAsiento = textBox1.Text;
                    if (asientos.Any(a => a.NumAsiento_460AS == numAsiento)) throw new Exception(string.Format(IdiomaManager_460AS.Instancia.Traducir("msg_nro_asiento_existe"), numAsiento));
                    if (!Regex.IsMatch(numAsiento, @"^[A-Za-z]{1}[0-9]{3}$")) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_nro_asiento_invalido"));
                    if (string.IsNullOrWhiteSpace(comboBox1.Text)) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_tipo_asiento_vacio"));
                    TipoAsiento_460AS tipo = (TipoAsiento_460AS)Enum.Parse(typeof(TipoAsiento_460AS), comboBox1.Text);
                    var nuevoAsiento = new Asiento_460AS(numAsiento, vuelo, true, tipo, null);
                    bllAsiento_460AS.AgregarAsiento_460AS(nuevoAsiento);
                    textBox1.Clear();
                }
                else if (estadoActual == FormEstado.Eliminar)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_vuelo_vacio"));
                    var vuelo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    if (dataGridView2.SelectedRows.Count == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_asiento_vacio"));
                    var asiento = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    bllAsiento_460AS.EliminarAsiento(asiento, vuelo);
                }
                CargarVuelos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string TraducirPais(string paisEsp)
        {
            string traducido = IdiomaManager_460AS.Instancia.Traducir(paisEsp);
            return traducido == paisEsp ? paisEsp : traducido;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                label5.Text = "Modo Consulta";
                estadoActual = FormEstado.Consulta;
                textBox1.Enabled = false;
                comboBox1.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = false;
                button4.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string codVuelo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                CargarAsientos(codVuelo);
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
            dataGridView1.Columns[0].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Codigo");
            dataGridView1.Columns[1].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Aerolinea");
            dataGridView1.Columns[2].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Origen");
            dataGridView1.Columns[3].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Destino");
            dataGridView1.Columns[4].HeaderText = IdiomaManager_460AS.Instancia.Traducir("FechaSalida");
            dataGridView1.Columns[5].HeaderText = IdiomaManager_460AS.Instancia.Traducir("FechaLlegada");
            dataGridView1.Columns[6].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Precio");
        }

        private void CargarAsientos(string codVuelo)
        {
            var asientos = bllAsiento_460AS.ObtenerAsientos_460AS(codVuelo);
            dataGridView2.DataSource = asientos.Select(a => new
            {
                NumeroAsiento = a.NumAsiento_460AS,
                Tipo = a.Tipo_460AS.ToString(),
                Estado = a.Disponible_460AS
                    ? IdiomaManager_460AS.Instancia.Traducir("Disponible")
                    : IdiomaManager_460AS.Instancia.Traducir("Ocupado")
            }).ToList();
            dataGridView2.Columns[0].HeaderText = IdiomaManager_460AS.Instancia.Traducir("NumAsiento");
            dataGridView2.Columns[1].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Tipo");
            dataGridView2.Columns[2].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Estado");
        }

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_vuelos");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_nro_asiento");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_asientos");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_tipo");
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_añadir");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_eliminar");
            button3.Text = IdiomaManager_460AS.Instancia.Traducir("boton_guardar");
            button4.Text = IdiomaManager_460AS.Instancia.Traducir("boton_cancelar");
            switch (estadoActual)
            {
                case FormEstado.Consulta:
                    label5.Text = IdiomaManager_460AS.Instancia.Traducir("modo_consulta");
                    break;
                case FormEstado.Agregar:
                    label5.Text = IdiomaManager_460AS.Instancia.Traducir("modo_añadir");
                    break;
                case FormEstado.Eliminar:
                    label5.Text = IdiomaManager_460AS.Instancia.Traducir("modo_eliminar");
                    break;
            }
            CargarVuelos();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string codVuelo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                CargarAsientos(codVuelo);
            }
        }
    }
}
