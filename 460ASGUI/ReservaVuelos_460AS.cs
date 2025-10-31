using _460ASBE;
using _460ASBLL;
using _460ASDAL;
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
    public partial class ReservaVuelos_460AS : Form, IIdiomaObserver_460AS
    {
        private bool formInicializado = false;
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
            dataGridView1.DataSource = null;
            panel1.Enabled = false;
            groupBox1.Enabled = false;
            button3.Enabled = false;
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
            formInicializado = true;
        }

        private decimal precioTotal;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_vuelo_vacio"));
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
                    label10.Text = $"{IdiomaManager_460AS.Instancia.Traducir("Cliente")}: {clienteActual.Nombre_460AS} {clienteActual.Apellido_460AS}\n" +
                               $"{IdiomaManager_460AS.Instancia.Traducir("Vuelo")}: {vueloSeleccionado.CodVuelo_460AS} - {vueloSeleccionado.Destino_460AS}\n" +
                               $"{IdiomaManager_460AS.Instancia.Traducir("Fecha")}: {vueloSeleccionado.FechaSalida_460AS}\n" +
                               $"{IdiomaManager_460AS.Instancia.Traducir("Asientos")}: {string.Join(", ", asientosSeleccionados.Select(a => a.NumAsiento_460AS))}\n" +
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
                string codReserva = bllReserva_460AS.GenerarCodigoReservaUnico_460AS();
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
                if (textBox1.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_dni_vacio"));
                string dni = textBox1.Text;
                if (textBox2.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_nombre_vacio"));
                string nombre = textBox2.Text;
                if (textBox3.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_apellido_vacio"));
                string apellido = textBox3.Text;
                var clienteExistentePorDNI = bllCliente_460AS.ObtenerClientes_460AS().FirstOrDefault(c => c.DNI_460AS == dni);
                if (clienteExistentePorDNI != null &&
                    (!clienteExistentePorDNI.Nombre_460AS.Equals(nombre, StringComparison.OrdinalIgnoreCase) ||
                     !clienteExistentePorDNI.Apellido_460AS.Equals(apellido, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show(
                        IdiomaManager_460AS.Instancia.Traducir("msg_datos_cliente_incorrectos"),
                        IdiomaManager_460AS.Instancia.Traducir("msg_operacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }
                var cliente = bllCliente_460AS.ObtenerClientes_460AS().FirstOrDefault(c => c.DNI_460AS == dni &&
                         c.Nombre_460AS.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                         c.Apellido_460AS.Equals(apellido, StringComparison.OrdinalIgnoreCase));
                if (cliente == null)
                {
                    var resultado = MessageBox.Show(
                    IdiomaManager_460AS.Instancia.Traducir("msg_cliente_no_registrado"),
                    IdiomaManager_460AS.Instancia.Traducir("msg_cliente_no_encontrado"),
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
                            if (cliente == null) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_no_registrado_cliente"));
                            clienteActual = cliente;
                            panel2.Enabled = false;
                            panel1.Enabled = true;
                            comboBox1.SelectedIndex = 0;
                            CargarVuelos();
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
            if (!formInicializado || !panel1.Enabled) return; 
            CargarVuelos();
        }

        private string TraducirPais(string paisEsp)
        {
            string traducido = IdiomaManager_460AS.Instancia.Traducir(paisEsp);
            return traducido == paisEsp ? paisEsp : traducido;
        }

        private void CargarVuelos()
        {
            string filtroPais = comboBox1.Text;
            List<Vuelo_460AS> vuelosFiltrados;

            if (filtroPais == IdiomaManager_460AS.Instancia.Traducir("Todos"))
                vuelosFiltrados = bllVuelo_460AS.ObtenerVuelos_460AS();
            else
                vuelosFiltrados = bllVuelo_460AS.ObtenerVuelos_460AS()
                    .Where(v => v.Destino_460AS == TraducirAlEspanol(filtroPais))
                    .ToList();

            dataGridView1.DataSource = vuelosFiltrados.Select(v => new
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

        private string TraducirAlEspanol(string textoActual)
        {
            switch (textoActual)
            {
                case "Brazil": return "Brasil";
                case "Spain": return "España";
                case "United States": return "Estados Unidos";
                case "Uruguay": return "Uruguay";
                case "Todos": return "Todos";
                default: return textoActual;
            }
        }

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_vuelos");
            label9.Text = IdiomaManager_460AS.Instancia.Traducir("label_dni");
            label8.Text = IdiomaManager_460AS.Instancia.Traducir("label_nombre");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_cliente");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_destino");
            label7.Text = IdiomaManager_460AS.Instancia.Traducir("label_apellido");

            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_seleccion_asiento");
            button3.Text = IdiomaManager_460AS.Instancia.Traducir("boton_pagar");
            button4.Text = IdiomaManager_460AS.Instancia.Traducir("boton_ingresar");

            comboBox1.Items.Clear();
            comboBox1.Items.Add(IdiomaManager_460AS.Instancia.Traducir("Brasil"));
            comboBox1.Items.Add(IdiomaManager_460AS.Instancia.Traducir("España"));
            comboBox1.Items.Add(IdiomaManager_460AS.Instancia.Traducir("Estados Unidos"));
            comboBox1.Items.Add(IdiomaManager_460AS.Instancia.Traducir("Uruguay"));
            comboBox1.Items.Add(IdiomaManager_460AS.Instancia.Traducir("Todos"));

            groupBox1.Text = IdiomaManager_460AS.Instancia.Traducir("group_resumen");
        }
    }
}
