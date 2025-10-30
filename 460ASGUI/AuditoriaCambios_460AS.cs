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
    public partial class AuditoriaCambios_460AS : Form, IIdiomaObserver_460AS
    {
        private BLL460AS_Cliente_C bllClienteC = new BLL460AS_Cliente_C();
        private BLL460AS_Cliente bllCliente = new BLL460AS_Cliente();
        public AuditoriaCambios_460AS()
        {
            InitializeComponent();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
            CargarBitacora();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.MultiSelect = false; dataGridView1.ReadOnly = true;
        }

        private void CargarBitacora()
        {
            try
            {
                var lista = bllClienteC.ObtenerTodos_460AS()
                    .Select(c => new
                    {
                        c.DNI_460AS,
                        c.Nombre_460AS,
                        c.Apellido_460AS,
                        c.FechaNacimiento_460AS,
                        c.Telefono_460AS,
                        c.NroPasaporte_460AS,
                        c.FechaCambio_460AS,
                        Activo_460AS = c.Activo_460AS ? 1 : 0 
                    })
                    .ToList();

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = lista;

                AjustarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los registros: " + ex.Message);
            }
        }

        private void AjustarColumnas()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["DNI_460AS"].HeaderText = "DNI";
                dataGridView1.Columns["Nombre_460AS"].HeaderText = "Nombre";
                dataGridView1.Columns["Apellido_460AS"].HeaderText = "Apellido";
                dataGridView1.Columns["FechaNacimiento_460AS"].HeaderText = "Fecha nacimiento";
                dataGridView1.Columns["Telefono_460AS"].HeaderText = "Teléfono";
                dataGridView1.Columns["NroPasaporte_460AS"].HeaderText = "Pasaporte";
                dataGridView1.Columns["FechaCambio_460AS"].HeaderText = "Fecha cambio";
                dataGridView1.Columns["Activo_460AS"].HeaderText = "Activo";

                if (dataGridView1.Columns["Activo_460AS"] is DataGridViewCheckBoxColumn)
                {
                    int index = dataGridView1.Columns["Activo_460AS"].Index;
                    dataGridView1.Columns.RemoveAt(index);

                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn
                    {
                        Name = "Activo_460AS",
                        HeaderText = "Activo",
                        DataPropertyName = "Activo_460AS"
                    };
                    dataGridView1.Columns.Insert(index, col);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = textBox1.Text.Trim();
                string nombre = textBox2.Text.Trim();
                string apellido = textBox3.Text.Trim();

                bool hayFiltroTexto = !string.IsNullOrEmpty(dni) || !string.IsNullOrEmpty(nombre) || !string.IsNullOrEmpty(apellido);
                bool fechaInicioMarcada = dateTimePicker1.Checked;
                bool fechaFinMarcada = dateTimePicker2.Checked;

                if (fechaInicioMarcada && !fechaFinMarcada)
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_selec_fin"));
                    return;
                }
                if (!fechaInicioMarcada && fechaFinMarcada)
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_selec_inicio"));
                    return;
                }
                if (dateTimePicker1.Value.Date > dateTimePicker2.Value.Date)
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_fechas_error"),
                                    IdiomaManager_460AS.Instancia.Traducir("msg_fechas_invalido"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateTimePicker1.Value = DateTime.Today; 
                    dateTimePicker2.Value = DateTime.Today;

                    return;
                }
                if (!hayFiltroTexto && !fechaInicioMarcada && !fechaFinMarcada)
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_criterio"));
                    return;
                }

                DateTime? fechaInicio = fechaInicioMarcada ? dateTimePicker1.Value.Date : (DateTime?)null;
                DateTime? fechaFin = fechaFinMarcada ? dateTimePicker2.Value.Date : (DateTime?)null;

                var lista = bllClienteC.FiltrarClientesC_460AS(
                    dni: dni == "" ? null : dni,
                    nombre: nombre == "" ? null : nombre,
                    apellido: apellido == "" ? null : apellido
                );

                if (fechaInicio.HasValue && fechaFin.HasValue)
                    lista = lista.Where(c => c.FechaCambio_460AS.Date >= fechaInicio && c.FechaCambio_460AS.Date <= fechaFin).ToList();

                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_resultados_error"));
                    return;
                }

                var listaConvertida = lista.Select(c => new
                {
                    c.DNI_460AS,
                    c.Nombre_460AS,
                    c.Apellido_460AS,
                    c.FechaNacimiento_460AS,
                    c.Telefono_460AS,
                    c.NroPasaporte_460AS,
                    c.FechaCambio_460AS,
                    Activo_460AS = c.Activo_460AS ? 1 : 0
                }).ToList();

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = listaConvertida;
                AjustarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Checked = false;
            dateTimePicker2.Checked = false;
            CargarBitacora();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_activar"));
                    return;
                }

                
                var fila = dataGridView1.SelectedRows[0];

                string dni = fila.Cells["DNI_460AS"].Value.ToString();
                DateTime fechaCambio = Convert.ToDateTime(fila.Cells["FechaCambio_460AS"].Value);
                string nombre = fila.Cells["Nombre_460AS"].Value.ToString();
                string apellido = fila.Cells["Apellido_460AS"].Value.ToString();
                int activo = Convert.ToInt32(fila.Cells["Activo_460AS"].Value);
                if (activo == 1)
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_activo"));
                    return;
                }
                var clienteOriginal = bllCliente.ObtenerClientes_460AS().FirstOrDefault(c => c.DNI_460AS == dni);
                if (clienteOriginal == null)
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_cliente_eliminado"),
                                    IdiomaManager_460AS.Instancia.Traducir("msg_operacion"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                DialogResult r = MessageBox.Show(
                    IdiomaManager_460AS.Instancia.Traducir("msg_activar_cliente"),
                    IdiomaManager_460AS.Instancia.Traducir("msg_confirmacion"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (r == DialogResult.Yes)
                {
                    bllClienteC.ActivarCliente_460AS(dni, fechaCambio);
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_version"));
                    CargarBitacora();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al activar versión: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_cambios");

            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_aplicar");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_limpiar");
            button3.Text = IdiomaManager_460AS.Instancia.Traducir("boton_activar2");
            button4.Text = IdiomaManager_460AS.Instancia.Traducir("boton_salir");

            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_dni");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_nombre");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_apellido");
            label7.Text = IdiomaManager_460AS.Instancia.Traducir("label_fechaIni");
            label8.Text = IdiomaManager_460AS.Instancia.Traducir("label_fechaFin");
        }
    }
}
