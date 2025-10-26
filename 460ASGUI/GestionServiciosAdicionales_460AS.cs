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
                    var formCambio = new CambioAsientoForm(codVuelo, reservaSeleccionada.CodReserva_460AS);

                    if (formCambio.ShowDialog() == DialogResult.OK && formCambio.CambiosPendientes.Any())
                    {
                        serviciosAgregados.RemoveAll(s => s.Nombre == "Cambio de asiento");
                        serviciosParaGuardar.RemoveAll(s => s.TipoServicio_460AS == "Cambio de asiento");

                        serviciosAgregados.Add(("Cambio de asiento", formCambio.PrecioTotal, string.Empty));

                        string codServicioComun = Guid.NewGuid().ToString();

                        foreach (var cambio in formCambio.CambiosPendientes.Values)
                        {
                            serviciosParaGuardar.Add(new CambioAsientoServicio(reservaSeleccionada)
                            {
                                CodServicio_460AS = codServicioComun,
                                NumAsiento_460AS = cambio.NumAsiento_460AS,
                                TipoServicio_460AS = "Cambio de asiento",
                                Descripcion_460AS = $"Cambio a asientos {string.Join(", ", formCambio.CambiosPendientes.Values.Select(a => a.NumAsiento_460AS))}",
                                Precio_460AS = formCambio.PrecioTotal / formCambio.CambiosPendientes.Count,
                                ListaCambios = formCambio.ListaCambios,
                                Reserva_460AS = reservaSeleccionada
                            });
                        }

                        ActualizarResumen();
                    }
                }

                else if (servicio == "Valija extra")
                {
                    RegistroValijaExtra_460AS form = new RegistroValijaExtra_460AS();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        serviciosAgregados.RemoveAll(s => s.Nombre == "Valija extra");
                        serviciosParaGuardar.RemoveAll(s => s.TipoServicio_460AS == "Valija extra");
                        serviciosAgregados.Add(("Valija extra", form.TotalValijas, ""));
                        serviciosParaGuardar.Add(new ValijaExtra_460AS(reservaSeleccionada)
                        {
                            CodServicio_460AS = Guid.NewGuid().ToString(),
                            Precio_460AS = form.TotalValijas,
                            Cantidad_460AS = form.CantidadTotal,    
                            PesoTotal_460AS = form.PesoTotal,
                            TipoServicio_460AS = "Valija extra",
                            Descripcion_460AS = $"Valijas: {form.CantidadTotal} - Peso total: {form.PesoTotal} kg",
                            Reserva_460AS = reservaSeleccionada
                        });
                        ActualizarResumen();
                    }
                }
                else if (servicio == "Comida especial")
                {
                    RegistrarComidaEspecial_460AS form = new RegistrarComidaEspecial_460AS();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        serviciosAgregados.RemoveAll(s => s.Nombre == "Comida especial");
                        serviciosParaGuardar.RemoveAll(s => s.TipoServicio_460AS == "Comida especial");
                        serviciosAgregados.Add(("Comida especial", form.TotalComidas, ""));
                        serviciosParaGuardar.Add(new ComidaEspecial_460AS(reservaSeleccionada)
                        {
                            CodServicio_460AS = Guid.NewGuid().ToString(),
                            Precio_460AS = form.TotalComidas,
                            TipoComida_460AS = form.TipoSeleccionado,
                            TipoServicio_460AS = "Comida especial",
                            Descripcion_460AS = $"Comida: {form.TipoSeleccionado}",
                            Reserva_460AS = reservaSeleccionada
                        });
                        ActualizarResumen();
                    }
                }
                else if (servicio == "Seguro de viaje")
                {
                    DateTime fechaSalida = reservaSeleccionada.Vuelo_460AS.FechaSalida_460AS;
                    using (var form = new RegistrarSeguroViaje_460AS(fechaSalida))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            serviciosAgregados.RemoveAll(s => s.Nombre == "Seguro de viaje");
                            serviciosParaGuardar.RemoveAll(s => s.TipoServicio_460AS == "Seguro de viaje");
                            serviciosAgregados.Add(("Seguro de viaje", form.PrecioSeleccionado, $"vence {form.FechaVencimiento:dd/MM/yyyy}"));
                            serviciosParaGuardar.Add(new SeguroViaje_460AS(reservaSeleccionada)
                            {
                                CodServicio_460AS = Guid.NewGuid().ToString(),
                                Precio_460AS = form.PrecioSeleccionado,
                                Cobertura_460AS = form.SeguroSeleccionado,   
                                Vencimiento_460AS = form.FechaVencimiento,
                                TipoServicio_460AS = "Seguro de viaje",
                                Descripcion_460AS = $"Seguro: {form.SeguroSeleccionado} - Vence {form.FechaVencimiento:dd/MM/yyyy}",
                                Reserva_460AS = reservaSeleccionada
                            });
                            ActualizarResumen();
                        }
                    }
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
                    $"Cliente: {clienteActual?.Nombre_460AS} {clienteActual?.Apellido_460AS}\n" +
                    $"Reserva: {reservaSeleccionada?.CodReserva_460AS}\n" +
                    $"Vuelo: {reservaSeleccionada?.Vuelo_460AS?.CodVuelo_460AS} – {reservaSeleccionada?.Vuelo_460AS?.Destino_460AS}\n\n" +
                    "Servicios agregados:\n" +
                    string.Join("\n", serviciosAgregados.Select(s =>
                        $"• {s.Nombre} – {s.Monto:0.00} USD" + (string.IsNullOrEmpty(s.Extra) ? "" : $" ({s.Extra})")
                    )) + "\n\n" +
                    $"Total a pagar: {total:0.00} USD";
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
                    throw new Exception("No hay servicios con monto válido para cobrar.");

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

                    MessageBox.Show(
                        $"Pago de servicios adicionales registrado correctamente.\n" +
                        $"Monto: {totalServicios:0.00} USD\n" +
                        $"Tipo de pago: {formCobro.TipoPagoSeleccionado}",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
