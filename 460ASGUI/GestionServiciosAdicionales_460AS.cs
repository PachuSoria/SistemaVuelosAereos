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
using System.Text.RegularExpressions;
using CambioAsientoForm = _460ASGUI.CambioAsiento_460AS;
using CambioAsientoServicio = _460ASBE.CambioAsiento_460AS;

namespace _460ASGUI
{
    public partial class GestionServiciosAdicionales_460AS : Form, IIdiomaObserver_460AS
    {
        private BLL460AS_Servicios bllServicios = new BLL460AS_Servicios();
        private BLL460AS_Pago bllPago = new BLL460AS_Pago();
        private Cliente_460AS clienteActual;
        private Reserva_460AS reservaSeleccionada;
        private BLL460AS_Cliente bllCliente;
        private BLL460AS_Reserva bllReserva;
        private List<(string Nombre, decimal Monto, string Extra)> serviciosAgregados = new();
        private List<ServiciosDecorator_460AS> serviciosParaGuardar = new();

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
            CargarServiciosCombo();
        }

        private string TraducirServicioDisplay(string key)
        {
            return key switch
            {
                "Cambio de asiento" => IdiomaManager_460AS.Instancia.Traducir("combo_asiento"),
                "Comida especial" => IdiomaManager_460AS.Instancia.Traducir("combo_comida"),
                "Seguro de viaje" => IdiomaManager_460AS.Instancia.Traducir("combo_seguro"),
                "Valija extra" => IdiomaManager_460AS.Instancia.Traducir("combo_valija"),
                _ => key
            };
        }

        private void RebindServiciosDisponibles(List<string> keysDisponibles, string? selectedKeyToKeep = null)
        {
            var items = keysDisponibles
                .Select(k => new KeyValuePair<string, string>(k, TraducirServicioDisplay(k)))
                .ToList();

            var keep = selectedKeyToKeep ?? (comboBox1.SelectedValue as string);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            comboBox1.DataSource = items;

            if (!string.IsNullOrEmpty(keep) && items.Any(i => i.Key == keep))
                comboBox1.SelectedValue = keep;
            else if (items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void CargarServiciosCombo()
        {
            comboBox1.DataSource = new List<KeyValuePair<string, string>>
            {
                new("Cambio de asiento", IdiomaManager_460AS.Instancia.Traducir("combo_asiento")),
                new("Comida especial", IdiomaManager_460AS.Instancia.Traducir("combo_comida")),
                new("Seguro de viaje", IdiomaManager_460AS.Instancia.Traducir("combo_seguro")),
                new("Valija extra",    IdiomaManager_460AS.Instancia.Traducir("combo_valija"))
            };
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        public void ActualizarIdioma()
        {
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_cliente");
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_dni");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_nombre");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_apellido");

            label5.Text = IdiomaManager_460AS.Instancia.Traducir("label_reservas");
            label6.Text = IdiomaManager_460AS.Instancia.Traducir("label_servicios");
            groupBox1.Text = IdiomaManager_460AS.Instancia.Traducir("label_resumen");

            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_ingresar");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_servicio");
            button3.Text = IdiomaManager_460AS.Instancia.Traducir("boton_cobrar");

            if (comboBox1.DataSource is List<KeyValuePair<string, string>> list)
            {
                var keys = list.Select(p => p.Key).ToList();
                var selectedKey = comboBox1.SelectedValue as string;
                RebindServiciosDisponibles(keys, selectedKey);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = textBox1.Text.Trim();
                string nombre = textBox2.Text.Trim();
                string apellido = textBox3.Text.Trim();

                if (string.IsNullOrEmpty(dni))
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_dni_requerido"));

                var clienteExistente = bllCliente.ObtenerClientes_460AS()
                    .FirstOrDefault(c => c.DNI_460AS == dni);

                if (clienteExistente != null &&
                    (!clienteExistente.Nombre_460AS.Equals(nombre, StringComparison.OrdinalIgnoreCase) ||
                     !clienteExistente.Apellido_460AS.Equals(apellido, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show(
                        IdiomaManager_460AS.Instancia.Traducir("msg_datos_cliente_incorrectos"),
                        IdiomaManager_460AS.Instancia.Traducir("msg_operacion"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (clienteExistente == null)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_cliente_no_encontrado"));

                clienteActual = clienteExistente;
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
            dataGridView1.Columns[0].HeaderText = IdiomaManager_460AS.Instancia.Traducir("col_codigo");
            dataGridView1.Columns[1].HeaderText = IdiomaManager_460AS.Instancia.Traducir("col_vuelo");
            dataGridView1.Columns[2].HeaderText = IdiomaManager_460AS.Instancia.Traducir("col_fecha");
            dataGridView1.Columns[3].HeaderText = IdiomaManager_460AS.Instancia.Traducir("col_total");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue == null)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_selec_servicio"));

                string servicio = comboBox1.SelectedValue.ToString();

                switch (servicio)
                {
                    case "Cambio de asiento":
                        string codVuelo = reservaSeleccionada.Vuelo_460AS.CodVuelo_460AS;
                        var formCambio = new CambioAsientoForm(codVuelo, reservaSeleccionada.CodReserva_460AS);

                        if (formCambio.ShowDialog() == DialogResult.OK && formCambio.CambiosPendientes.Any())
                        {
                            serviciosAgregados.RemoveAll(s => s.Nombre == servicio);
                            serviciosParaGuardar.RemoveAll(s => s.TipoServicio_460AS == servicio);

                            serviciosAgregados.Add((servicio, formCambio.PrecioTotal, string.Empty));

                            string codServicioComun = Guid.NewGuid().ToString();

                            foreach (var cambio in formCambio.CambiosPendientes.Values)
                            {
                                serviciosParaGuardar.Add(new CambioAsientoServicio(reservaSeleccionada)
                                {
                                    CodServicio_460AS = codServicioComun,
                                    NumAsiento_460AS = cambio.NumAsiento_460AS,
                                    TipoServicio_460AS = servicio,
                                    Descripcion_460AS = $"Cambio a asientos {string.Join(", ", formCambio.CambiosPendientes.Values.Select(a => a.NumAsiento_460AS))}",
                                    Precio_460AS = formCambio.PrecioTotal / formCambio.CambiosPendientes.Count,
                                    ListaCambios = formCambio.ListaCambios,
                                    Reserva_460AS = reservaSeleccionada
                                });
                            }
                            ActualizarResumen();
                        }
                        break;

                    case "Valija extra":
                        using (var formValija = new RegistroValijaExtra_460AS())
                        {
                            if (formValija.ShowDialog() == DialogResult.OK)
                            {
                                serviciosAgregados.RemoveAll(s => s.Nombre == servicio);
                                serviciosParaGuardar.RemoveAll(s => s.TipoServicio_460AS == servicio);
                                serviciosAgregados.Add((servicio, formValija.TotalValijas, ""));
                                serviciosParaGuardar.Add(new ValijaExtra_460AS(reservaSeleccionada)
                                {
                                    CodServicio_460AS = Guid.NewGuid().ToString(),
                                    Precio_460AS = formValija.TotalValijas,
                                    Cantidad_460AS = formValija.CantidadTotal,
                                    PesoTotal_460AS = formValija.PesoTotal,
                                    TipoServicio_460AS = servicio,
                                    Descripcion_460AS = $"Valijas: {formValija.CantidadTotal} - Peso total: {formValija.PesoTotal} kg",
                                    Reserva_460AS = reservaSeleccionada
                                });
                                ActualizarResumen();
                            }
                        }
                        break;

                    case "Comida especial":
                        using (var formComida = new RegistrarComidaEspecial_460AS())
                        {
                            if (formComida.ShowDialog() == DialogResult.OK)
                            {
                                serviciosAgregados.RemoveAll(s => s.Nombre == servicio);
                                serviciosParaGuardar.RemoveAll(s => s.TipoServicio_460AS == servicio);
                                serviciosAgregados.Add((servicio, formComida.TotalComidas, ""));
                                serviciosParaGuardar.Add(new ComidaEspecial_460AS(reservaSeleccionada)
                                {
                                    CodServicio_460AS = Guid.NewGuid().ToString(),
                                    Precio_460AS = formComida.TotalComidas,
                                    TipoComida_460AS = formComida.TipoSeleccionado,
                                    TipoServicio_460AS = servicio,
                                    Descripcion_460AS = $"Comida: {formComida.TipoSeleccionado}",
                                    Reserva_460AS = reservaSeleccionada
                                });
                                ActualizarResumen();
                            }
                        }
                        break;

                    case "Seguro de viaje":
                        DateTime fechaSalida = reservaSeleccionada.Vuelo_460AS.FechaSalida_460AS;
                        using (var formSeguro = new RegistrarSeguroViaje_460AS(fechaSalida))
                        {
                            if (formSeguro.ShowDialog() == DialogResult.OK)
                            {
                                serviciosAgregados.RemoveAll(s => s.Nombre == servicio);
                                serviciosParaGuardar.RemoveAll(s => s.TipoServicio_460AS == servicio);
                                serviciosAgregados.Add((servicio, formSeguro.PrecioSeleccionado, $"vence {formSeguro.FechaVencimiento:dd/MM/yyyy}"));
                                serviciosParaGuardar.Add(new SeguroViaje_460AS(reservaSeleccionada)
                                {
                                    CodServicio_460AS = Guid.NewGuid().ToString(),
                                    Precio_460AS = formSeguro.PrecioSeleccionado,
                                    Cobertura_460AS = formSeguro.SeguroSeleccionado,
                                    Vencimiento_460AS = formSeguro.FechaVencimiento,
                                    TipoServicio_460AS = servicio,
                                    Descripcion_460AS = $"Seguro: {formSeguro.SeguroSeleccionado} - Vence {formSeguro.FechaVencimiento:dd/MM/yyyy}",
                                    Reserva_460AS = reservaSeleccionada
                                });
                                ActualizarResumen();
                            }
                        }
                        break;
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

                decimal total = serviciosAgregados.Sum(s => s.Monto);

                label7.Text =
                    $"{IdiomaManager_460AS.Instancia.Traducir("label_cliente")}: {clienteActual?.Nombre_460AS} {clienteActual?.Apellido_460AS}\n" +
                    $"{IdiomaManager_460AS.Instancia.Traducir("label_reserva")}: {reservaSeleccionada?.CodReserva_460AS}\n" +
                    $"{IdiomaManager_460AS.Instancia.Traducir("label_vuelo")}: {reservaSeleccionada?.Vuelo_460AS?.CodVuelo_460AS} – {reservaSeleccionada?.Vuelo_460AS?.Destino_460AS}\n\n" +
                    $"{IdiomaManager_460AS.Instancia.Traducir("label_servicios_agregados")}:\n" +
                    string.Join("\n", serviciosAgregados.Select(s =>
                        $"• {TraducirServicioDisplay(s.Nombre)} – {s.Monto:0.00} USD" +
                        (string.IsNullOrEmpty(s.Extra) ? "" : $" ({s.Extra})")
                    )) + "\n\n" + $"{IdiomaManager_460AS.Instancia.Traducir("label_total_pagar")}: {total:0.00} USD";
            }
            else
            {
                panel1.Visible = false;
                button3.Enabled = false;
                label7.Text = string.Empty;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                decimal totalServicios = serviciosAgregados.Sum(s => s.Monto);
                if (totalServicios <= 0)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_servicios_invalidos"));

                using (var formCobro = new CobroServicios_460AS(totalServicios))
                {
                    if (formCobro.ShowDialog() != DialogResult.OK)
                        return;

                    foreach (var servicio in serviciosParaGuardar)
                    {
                        if (string.IsNullOrWhiteSpace(servicio.CodServicio_460AS))
                            servicio.CodServicio_460AS = Guid.NewGuid().ToString();

                        servicio.Reserva_460AS = reservaSeleccionada;
                    }

                    foreach (var servicio in serviciosParaGuardar)
                    {
                        bllServicios.GuardarServicio_460AS(servicio);
                    }

                    reservaSeleccionada.Cliente_460AS = clienteActual;
                    var pago = new Pago_460AS
                    {
                        CodPago_460AS = Guid.NewGuid().ToString(),
                        Reserva_460AS = reservaSeleccionada,
                        Monto_460AS = totalServicios,
                        TipoPago_460AS = formCobro.TipoPagoSeleccionado,
                        FechaPago_460AS = DateTime.Now,
                        ServiciosPagados = serviciosParaGuardar.Select(d => new Servicio_460AS
                        {
                            CodServicio_460AS = d.CodServicio_460AS,
                            CodReserva_460AS = reservaSeleccionada.CodReserva_460AS,
                            TipoServicio_460AS = d.TipoServicio_460AS,
                            Descripcion_460AS = d.Descripcion_460AS,
                            Precio_460AS = d.Precio_460AS
                        }).ToList()
                    };

                    bllPago.GuardarPago_460AS(pago);

                    BLL460AS_Asiento bllAsiento = new BLL460AS_Asiento();
                    foreach (var servicio in serviciosParaGuardar)
                    {
                        if (servicio is _460ASBE.CambioAsiento_460AS cambio &&
                            cambio.ListaCambios != null && cambio.ListaCambios.Any())
                        {
                            foreach (var det in cambio.ListaCambios)
                            {
                                bllAsiento.ActualizarCambioAsiento_460AS(
                                    det.AsientoViejo_460AS,
                                    det.AsientoNuevo_460AS
                                );
                            }
                        }
                    }
                    serviciosAgregados.Clear();
                    serviciosParaGuardar.Clear();
                    label7.Text = string.Empty;
                    panel1.Visible = false;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0) return;

                string codReserva = dataGridView1.SelectedRows[0].Cells["Codigo"].Value.ToString();
                reservaSeleccionada = bllReserva.ObtenerReservasCliente_460AS(clienteActual.DNI_460AS)
                                                .FirstOrDefault(r => r.CodReserva_460AS == codReserva);

                if (reservaSeleccionada == null)
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_reserva_error"));

                var pagos = bllPago.ObtenerPagosPorReserva_460AS(reservaSeleccionada.CodReserva_460AS);

                var serviciosPagados = pagos
                    .SelectMany(p => p.ServiciosPagados)
                    .Select(s => s.TipoServicio_460AS)
                    .Distinct()
                    .ToList();

                var todosLosServicios = new List<string>
                {
                    "Comida especial",
                    "Valija extra",
                    "Seguro de viaje",
                    "Cambio de asiento"
                };
                var serviciosDisponibles = todosLosServicios
                    .Where(s => s == "Cambio de asiento" || !serviciosPagados.Contains(s))
                    .ToList();

                RebindServiciosDisponibles(serviciosDisponibles);
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
