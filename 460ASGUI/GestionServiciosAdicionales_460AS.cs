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
    public partial class GestionServiciosAdicionales_460AS : Form, IIdiomaObserver_460AS
    {
        private Cliente_460AS clienteActual;
        private Reserva_460AS reservaSeleccionada;
        private BLL460AS_Cliente bllCliente;
        private BLL460AS_Reserva bllReserva;
        private List<string> serviciosAgregados = new List<string>();


        public GestionServiciosAdicionales_460AS()
        {
            InitializeComponent();
            bllCliente = new BLL460AS_Cliente();
            bllReserva = new BLL460AS_Reserva();
            dataGridView1.Enabled = false;
            comboBox1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        public void ActualizarIdioma()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = textBox1.Text.Trim();
                if (dni == "") throw new Exception("Debe ingresar el DNI del cliente.");
                clienteActual = bllCliente.ObtenerClientes_460AS().FirstOrDefault(c => c.DNI_460AS == dni);
                if (clienteActual == null) throw new Exception("Cliente no encontrado. Regístrelo antes de continuar.");

                textBox2.Text = clienteActual.Nombre_460AS;
                textBox3.Text = clienteActual.Apellido_460AS;

                CargarReservasCliente(dni);
                dataGridView1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarReservasCliente(string dni)
        {
            var reservas = bllReserva.ObtenerReservasCliente_460AS(dni);
            dataGridView1.DataSource = reservas.Select(r => new
            {
                Codigo = r.CodReserva_460AS,
                Vuelo = r.Vuelo_460AS.CodVuelo_460AS,
                Fecha = r.FechaReserva_460AS.ToShortDateString(),
                Total = r.PrecioTotal_460AS
            }).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                    throw new Exception("Debe seleccionar un servicio.");

                string servicio = comboBox1.SelectedItem.ToString();

                if (servicio == "Cambio de asiento")
                {
                    string codVuelo = reservaSeleccionada.Vuelo_460AS.CodVuelo_460AS;
                    CambioAsiento_460AS form = new CambioAsiento_460AS(codVuelo, reservaSeleccionada.CodReserva_460AS);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        serviciosAgregados.Add(servicio);
                        ActualizarResumen();
                    }
                }
                else if (servicio == "Valija extra")
                {
                    RegistroValijaExtra_460AS form = new RegistroValijaExtra_460AS();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        serviciosAgregados.Add($"Valija extra – Total {form.TotalValijas:C2}");
                        ActualizarResumen();
                    }
                }
                else if (servicio == "Comida especial")
                {
                    MessageBox.Show("Este servicio se implementará próximamente.",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (servicio == "Seguro de viaje")
                {
                    MessageBox.Show("Este servicio se implementará próximamente.",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarResumen()
        {
            if (serviciosAgregados.Count > 0)
            {
                panel1.Visible = true;
                button3.Enabled = true;

                //labelResumen.Text = "Servicios agregados:\n" +
                //    string.Join("\n", serviciosAgregados.Select(s => $"• {s}"));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) return;

                string codReserva = dataGridView1.SelectedRows[0].Cells["Codigo"].Value.ToString();

                reservaSeleccionada = bllReserva.ObtenerReservasCliente_460AS(clienteActual.DNI_460AS) .FirstOrDefault(r => r.CodReserva_460AS == codReserva);

                if (reservaSeleccionada == null) throw new Exception("No se pudo obtener la reserva seleccionada.");

                comboBox1.Enabled = true;
                button2.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
