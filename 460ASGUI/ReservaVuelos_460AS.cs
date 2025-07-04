using _460ASBE;
using _460ASBLL;
using _460ASDAL;
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
    public partial class ReservaVuelos_460AS : Form
    {
        private Cliente_460AS clienteActual;
        private Vuelo_460AS vueloSeleccionado;
        private List<Asiento_460AS> asientosSeleccionados;

        private BLL460AS_Cliente bllCliente_460AS;
        private BLL460AS_Vuelo bllVuelo_460AS;
        private BLL460AS_Reserva bllReserva_460AS;
        public ReservaVuelos_460AS()
        {
            InitializeComponent();
            bllCliente_460AS = new BLL460AS_Cliente();
            bllVuelo_460AS = new BLL460AS_Vuelo();
            bllReserva_460AS = new BLL460AS_Reserva();
            panel1.Enabled = false;
            groupBox1.Enabled = false;
            button3.Enabled = false;
        }

        private decimal precioTotal;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0) throw new Exception("Debe seleccionar un vuelo");
                string codVuelo = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                vueloSeleccionado = bllVuelo_460AS.ObtenerVuelos_460AS().FirstOrDefault(v => v.CodVuelo_460AS == codVuelo);
                SeleccionAsiento_460AS formAsientos = new SeleccionAsiento_460AS(codVuelo);
                if (formAsientos.ShowDialog() == DialogResult.OK)
                {
                    asientosSeleccionados = formAsientos.AsientosSeleccionados;
                    groupBox1.Enabled = true;
                    button3.Enabled = true;
                    precioTotal = 0;
                    foreach (var asiento in asientosSeleccionados)
                    {
                        decimal adicional = asiento.Tipo_460AS == TipoAsiento_460AS.VIP ? 75 : 0;
                        precioTotal += vueloSeleccionado.PrecioVuelo_460AS + adicional;
                    }
                    label10.Text = $"Cliente: {clienteActual.Nombre_460AS} {clienteActual.Apellido_460AS}\n" +
                                   $"Vuelo: {vueloSeleccionado.CodVuelo_460AS} - {vueloSeleccionado.Destino_460AS}\n" +
                                   $"Fecha: {vueloSeleccionado.FechaSalida_460AS}\n" +
                                   $"Asientos: {string.Join(", ", asientosSeleccionados.Select(a => a.NumAsiento_460AS))}\n" +
                                   $"Total: ${precioTotal} USD";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string codReserva = bllReserva_460AS.GenerarCodigoReservaUnico();
                Reserva_460AS reserva = new Reserva_460AS(
                    codReserva,
                    DateTime.Now,
                    clienteActual,
                    vueloSeleccionado,
                    precioTotal);
                reserva.AsientosReservados_460AS = asientosSeleccionados;
                CobroReserva_460AS formCobro = new CobroReserva_460AS(reserva);
                if (formCobro.ShowDialog() == DialogResult.OK)
                {
                    bllReserva_460AS.AgregarReserva_460AS(reserva);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0) throw new Exception("Debe ingresar el DNI");
                string dni = textBox1.Text;
                if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar el nombre");
                string nombre = textBox2.Text;
                if (textBox3.Text.Length == 0) throw new Exception("Debe ingresar el apellido");
                string apellido = textBox3.Text;
                var cliente = bllCliente_460AS.ObtenerClientes_460AS().FirstOrDefault(c => c.DNI_460AS == dni &&
                         c.Nombre_460AS.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                         c.Apellido_460AS.Equals(apellido, StringComparison.OrdinalIgnoreCase));
                if (cliente == null)
                {
                    var resultado = MessageBox.Show("El cliente no está registrado. ¿Desea registrarlo ahora?",
                                    "Cliente no encontrado",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        RegistroCliente_460AS formRegistrar = new RegistroCliente_460AS();
                        if (formRegistrar.ShowDialog() == DialogResult.OK)
                        {
                            cliente = bllCliente_460AS.ObtenerClientes_460AS()
                                        .FirstOrDefault(c => c.DNI_460AS == dni &&
                                                             c.Nombre_460AS.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                                                             c.Apellido_460AS.Equals(apellido, StringComparison.OrdinalIgnoreCase));
                            if (cliente == null)
                                throw new Exception("No se pudo registrar el cliente correctamente");
                            clienteActual = cliente;
                            panel2.Enabled = false;
                            panel1.Enabled = true;
                        }
                        return;
                    }
                    else return;
                }
                clienteActual = cliente;
                panel2.Enabled = false; panel1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pais = comboBox1.Text;
            List<Vuelo_460AS> vuelosFiltrados;
            if (pais == "Todos") vuelosFiltrados = bllVuelo_460AS.ObtenerVuelos_460AS();
            else vuelosFiltrados = bllVuelo_460AS.ObtenerVuelos_460AS().Where(v => v.Destino_460AS == pais).ToList();
            dataGridView1.DataSource = vuelosFiltrados.Select(v => new
            {
                Codigo = v.CodVuelo_460AS,
                Aerolinea = v.Aerolinea_460AS,
                Origen = v.Origen_460AS,
                Destino = v.Destino_460AS,
                FechaSalida = v.FechaSalida_460AS,
                FechaLlegada = v.FechaLlegada_460AS,
                Precio = $"${v.PrecioVuelo_460AS} USD"
            }).ToList();
        }
    }
}
