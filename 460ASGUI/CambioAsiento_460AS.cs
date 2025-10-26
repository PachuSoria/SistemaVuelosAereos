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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _460ASGUI
{
    public partial class CambioAsiento_460AS : Form, IIdiomaObserver_460AS
    {
        public List<(Asiento_460AS AsientoViejo_460AS, Asiento_460AS AsientoNuevo_460AS)> ListaCambios { get; set; } = new List<(Asiento_460AS, Asiento_460AS)>();
        public decimal PrecioTotal { get; private set; } = 0;
        BLL460AS_Asiento bllAsiento_460AS;
        private string codVueloSeleccionado;
        private string codReservaSeleccionada;
        private List<Asiento_460AS> asientosDisponibles;
        private List<Asiento_460AS> asientosSeleccionados;
        private List<Asiento_460AS> asientosActuales;
        private Asiento_460AS asientoSeleccionadoActual;
        private Dictionary<string, Asiento_460AS> cambiosPendientes = new Dictionary<string, Asiento_460AS>();
        private Reserva_460AS reservaSeleccionada;

        public Dictionary<string, Asiento_460AS> CambiosPendientes => cambiosPendientes;
        public List<Asiento_460AS> AsientosSeleccionados => asientosSeleccionados;
        public CambioAsiento_460AS(string codVuelo, string codReserva)
        {
            InitializeComponent();
            bllAsiento_460AS = new BLL460AS_Asiento();
            codVueloSeleccionado = codVuelo;
            codReservaSeleccionada = codReserva;
            radioButton1.Checked = true;
            asientosSeleccionados = new List<Asiento_460AS>();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            CargarAsientosActuales();
            ActualizarIdioma();
        }

        public CambioAsiento_460AS(Reserva_460AS reservaSeleccionada)
        {
            this.reservaSeleccionada = reservaSeleccionada;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null) throw new Exception("Debe seleccionar cuál asiento desea cambiar.");

                if (dataGridView1.SelectedRows.Count == 0)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_asiento_vacio"));

                string numAsientoNuevo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var asientoNuevo = asientosDisponibles.FirstOrDefault(a => a.NumAsiento_460AS == numAsientoNuevo);
                if (asientoNuevo == null)
                    throw new Exception("Asiento inválido.");

                var asientoViejo = (Asiento_460AS)comboBox1.SelectedItem;

                if (!asientoNuevo.Disponible_460AS)
                    throw new Exception(string.Format(IdiomaManager_460AS.Instancia.Traducir("msg_asiento_ocupado"), numAsientoNuevo));
                if (numAsientoNuevo == asientoViejo.NumAsiento_460AS)
                    throw new Exception("No puede elegir el mismo asiento que ya posee.");

                if (cambiosPendientes.ContainsKey(asientoViejo.NumAsiento_460AS))
                {
                    cambiosPendientes[asientoViejo.NumAsiento_460AS] = asientoNuevo;
                }
                else
                {
                    bool nuevoYaUsado = cambiosPendientes.Values.Any(v =>
                        v.NumAsiento_460AS == asientoNuevo.NumAsiento_460AS
                        && v.CodVuelo_460AS == asientoNuevo.CodVuelo_460AS); 
                    if (nuevoYaUsado)
                        throw new Exception($"El asiento {asientoNuevo.NumAsiento_460AS} ya fue elegido para otro cambio.");

                    cambiosPendientes.Add(asientoViejo.NumAsiento_460AS, asientoNuevo);
                }

                listBox1.Items.Clear();
                foreach (var kvp in cambiosPendientes)
                    listBox1.Items.Add($"Asiento {kvp.Key} → {kvp.Value.NumAsiento_460AS}");

                button4.Enabled = cambiosPendientes.Count > 0;
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
                Estado = a.Disponible_460AS
                ? IdiomaManager_460AS.Instancia.Traducir("Disponible")
                : IdiomaManager_460AS.Instancia.Traducir("Ocupado")
            }).ToList();
        }

        private void CargarAsientosActuales()
        {
            asientosActuales = bllAsiento_460AS.ObtenerAsientosDeReserva_460AS(codReservaSeleccionada);
            comboBox1.DataSource = null;
            comboBox1.DataSource = asientosActuales;
            comboBox1.DisplayMember = "NumAsiento_460AS";
            comboBox1.ValueMember = "NumAsiento_460AS";
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
                if (cambiosPendientes.Count == 0)
                    throw new Exception("Debe seleccionar al menos un cambio de asiento.");

                decimal precioPorCambio = radioButton1.Checked ? 50 : 100;
                PrecioTotal = cambiosPendientes.Count * precioPorCambio;

                ListaCambios.Clear();
                foreach (var kvp in cambiosPendientes)
                {
                    var asientoViejo = asientosActuales.First(a => a.NumAsiento_460AS == kvp.Key);
                    var asientoNuevo = kvp.Value;
                    ListaCambios.Add((asientoViejo, asientoNuevo));
                }

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

        public void ActualizarIdioma()
        {
            //label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_asientos");
            //label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_tipo");
            //label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_asientos_asignados");
            //button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_agregar_asiento");
            //button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_confirmar_asiento");
            //button3.Text = IdiomaManager_460AS.Instancia.Traducir("boton_salir");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cambiosPendientes.Clear();
                asientosSeleccionados.Clear();
                listBox1.Items.Clear();
                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedIndex = 0;
                else
                    comboBox1.SelectedIndex = -1;;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar los datos: " + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
